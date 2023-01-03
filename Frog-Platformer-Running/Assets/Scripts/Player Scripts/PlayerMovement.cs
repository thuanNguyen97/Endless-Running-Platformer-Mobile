using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpPower = 10f;
    public float secondJumpPower = 10f;
    public Transform groundCheckPosition;
    public float radius = 0.3f;
    public LayerMask layerGround;

    private Rigidbody _myBody;
    private bool _isGrounded;
    private bool _playerJumped;
    private bool _canDoubleJump = true;

    private PlayerAnimation _playerAnim;

    private bool _gameStarted;

    public GameObject smokePosition;

    void Awake()
    {
        _myBody = GetComponent<Rigidbody>();    // get Rigidbody component
        _playerAnim = GetComponent<PlayerAnimation>();  // get PLayerAnimation components
    }

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    void FixedUpdate()
    {
        if (_gameStarted)
        {
            PlayerMove();
            PlayerGrounded();
        }    
        
    }

    private void Update()   // this method is only for PlayerJump()
    {
        if (_gameStarted)
        {
            PlayerJump();
        }
    }    

    void PlayerMove()
    {
        _myBody.velocity = new Vector3(movementSpeed, _myBody.velocity.y, 0);
    }    

    void PlayerGrounded()
    {
        //create a sphere to check if player touching the ground
        _isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, layerGround).Length > 0;

        Debug.Log("Is player grounded " + _isGrounded);

        if (_isGrounded && _playerJumped)
        {
            _playerJumped = false;

            _playerAnim.DidLand();
        }    
    }

    void PlayerJump()
    {
        /*if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _myBody.AddForce(new Vector3(0, jumpPower, 0));
        } */   

        if (Input.GetKeyDown(KeyCode.Space) && !_isGrounded && _canDoubleJump)
        {
            _canDoubleJump = false;
            _myBody.AddForce(new Vector3(0, secondJumpPower, 0));
            Debug.Log("Second Jump");
        }
        else if (Input.GetKeyUp(KeyCode.Space) && _isGrounded)
        {
            _playerAnim.DidJump();  //play the jump animation

            _myBody.AddForce(new Vector3(0, jumpPower, 0));
            _playerJumped = true;
            _canDoubleJump = true;
            Debug.Log("First jump");
        }

        
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        _gameStarted = true;
        smokePosition.SetActive(true);
        _playerAnim.PlayerRun();
    }
}   // class 
