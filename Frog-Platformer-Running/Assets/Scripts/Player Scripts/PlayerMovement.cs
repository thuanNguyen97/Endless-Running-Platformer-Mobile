using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Rigidbody _myBody;

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
