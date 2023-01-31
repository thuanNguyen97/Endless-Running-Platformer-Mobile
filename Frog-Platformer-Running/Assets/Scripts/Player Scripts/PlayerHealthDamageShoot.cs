using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDamageShoot : MonoBehaviour
{
    [SerializeField]
    private Transform playerBullet;

    public float distanceBeforeNewPlatform = 120f;

    private LevelGenerator _levelGenerator;

    private void Awake()
    {
        _levelGenerator = GameObject.Find(Tags.LEVEL_GENERATOR_OBJ).GetComponent<LevelGenerator>();
    }

    private void Update()
    {
        Fire();
    }

    void FixedUpdate()
    {
        //Fire();
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Vector3 bulletPos = transform.position;
            bulletPos.y += 1.5f;
            bulletPos.x += 1f;
            Transform newBullet = Instantiate(playerBullet, bulletPos, Quaternion.identity);

            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
            newBullet.parent = transform;
        }    
    }    

    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.MONSTER_BULLET_TAG)
        {
            //inform game controller that player has died
            Destroy(gameObject);
        }

        if (target.tag == Tags.HEALTH_TAG || target.tag == Tags.BOUNDS_TAG)
        {
            //inform gameplayer controller that we have collect the health
            target.gameObject.SetActive(false);
        }    

        if (target.tag == Tags.MORE_PLATFORM_TAG)
        {
            //reposition the MorePlatform object
            Vector3 temp = target.transform.position;
            temp.x += distanceBeforeNewPlatform;
            target.transform.position = temp;

            _levelGenerator.GenerateLevel(false);
        }
    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == Tags.MONSTER_BULLET_TAG)
        {
            //inform game controller that player has died
            Destroy(gameObject);
        }
    }

}
