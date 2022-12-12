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

    void Awake()
    {
        _myBody = GetComponent<Rigidbody>();    // get Rigidbody component
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        _myBody.velocity = new Vector3(movementSpeed, _myBody.velocity.y, 0);
    }    


}   // class
