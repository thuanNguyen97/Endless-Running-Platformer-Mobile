using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDamageShoot : MonoBehaviour
{
    public float distanceBeforeNewPlatform = 120f;

    private LevelGenerator _levelGenerator;

    private void Awake()
    {
        _levelGenerator = GameObject.Find(Tags.LEVEL_GENERATOR_OBJ).GetComponent<LevelGenerator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.MORE_PLATFORM_TAG)
        {
            //reposition the MorePlatform object
            Vector3 temp = target.transform.position;
            temp.x += distanceBeforeNewPlatform;
            target.transform.position = temp;

            _levelGenerator.GenerateLevel(false);
        }
    }

}
