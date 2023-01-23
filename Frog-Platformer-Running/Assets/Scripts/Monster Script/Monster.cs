using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject MonsterDiedEffect;
    public Transform bullet;
    public float distanceFromPlayerToStartMove = 20f;
    public float movementSpeed_Min = 1f;
    public float movementSpeed_Max = 2f;

    private bool _moveRight;
    private float _movementSpeed;
    private bool _isPlayerInRegion; // this variable tell us if player is in the monster attack region or not

    private Transform playerTransform;

    public bool canShoot;

    private string FUNCTION_TO_INVOKE = "StartShooting";

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;

        if (Random.Range(0.0f, 1.0f) > 0.5f)
        {
            _moveRight = true;
        }
        else
        {
            _moveRight = false;
        }

        _movementSpeed =- Random.Range(movementSpeed_Min, movementSpeed_Max);


    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform)
        {

            float distanceFromPlayer = (playerTransform.position - playerTransform.position).magnitude;

            if (distanceFromPlayer < distanceFromPlayerToStartMove)
            {
                if (_moveRight)
                {
                    transform.position = new Vector3(transform.position.x + Time.deltaTime * _movementSpeed,
                        transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x - Time.deltaTime * _movementSpeed,
                        transform.position.y, transform.position.z);
                }

                if (!_isPlayerInRegion)
                {
                    if (canShoot)
                    {
                        InvokeRepeating(FUNCTION_TO_INVOKE, 0.5f, 1.5f);
                    }
                    _isPlayerInRegion = true;  
                }
            }
            else
            {
                CancelInvoke(FUNCTION_TO_INVOKE);
            }
        }
    }

    void StartShooting()
    {
        if (playerTransform)
        {
            Vector3 bulletPos = transform.position; //set position of the bullet
            bulletPos.y += 1.5f;
            bulletPos.x -= 1f;

            Transform newBullet = (Transform) Instantiate(bullet, bulletPos, Quaternion.identity); //instancite bullet

            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
            
            newBullet.parent = transform;

        }
    }

    void MonsterDied()
    {
        Vector3 effectPos = transform.position;
        effectPos.y += 2f;
        Instantiate(MonsterDiedEffect, effectPos, Quaternion.identity);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.PLAYER_BULLET_TAG)
        {
            MonsterDied();
        }    
    }

    

    void OnCollisionEnter(Collision target)
    {
        if (target.collider.tag == Tags.PLAYER_TAG)
        {
            MonsterDied();
        }    
    }

}   // class
