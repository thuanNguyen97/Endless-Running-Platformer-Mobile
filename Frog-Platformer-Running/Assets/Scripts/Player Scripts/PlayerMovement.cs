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
    private bool _canDoubleJump;

    void Awake()
    {
        _myBody = GetComponent<Rigidbody>();    // get Rigidbody component
    }

    void FixedUpdate()
    {
        PlayerMove();
        PlayerGrounded(); 
        PlayerJump();
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
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isGrounded && _canDoubleJump)
        {
            _canDoubleJump = false;
            _myBody.AddForce(new Vector3(0, secondJumpPower, 0));
            Debug.Log("First jump");
        }
        else if (Input.GetKeyUp(KeyCode.Space) && _isGrounded)
        {
            _myBody.AddForce(new Vector3(0, jumpPower, 0));
            _playerJumped = true;
            _canDoubleJump = true;
            Debug.Log("Second jump");
        }
    }
}   // class 
