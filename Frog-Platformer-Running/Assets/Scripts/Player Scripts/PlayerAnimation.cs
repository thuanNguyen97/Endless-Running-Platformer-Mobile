using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    
    private Animation _anim;

    void Awake()
    {
        _anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DidJump()
    {
        _anim.Stop(Tags.ANIMATION_RUN);
        _anim.Play(Tags.ANIMATION_JUMP);
        _anim.PlayQueued(Tags.ANIMATION_JUMP_FALL);
        Debug.Log("Player Did jump");
    }    

    public void DidLand()
    {
        _anim.Stop(Tags.ANIMATION_JUMP_FALL);
        _anim.Stop(Tags.ANIMATION_JUMP_LAND);
        _anim.Blend(Tags.ANIMATION_JUMP_LAND, 0);
        _anim.CrossFade(Tags.ANIMATION_RUN);
    }    

    public void PlayerRun()
    {
        _anim.Play(Tags.ANIMATION_RUN);
    }   
    

} //class
