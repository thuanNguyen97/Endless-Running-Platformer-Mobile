using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private float _collisionSoundEffect = 1f;

    public float audioFootVolume = 1f;
    public float soundEffectPitchRandomness = 0.05f;

    private AudioSource _audioSource;
    public AudioClip gerericFootSound;
    public AudioClip metalFootSound;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void FootSound()
    {
        Debug.Log("PLAY SOUND");

        //Random the sound volume and sound pitch
        _audioSource.volume = _collisionSoundEffect * audioFootVolume;
        _audioSource.pitch = Random.Range(1f - soundEffectPitchRandomness, 1f + soundEffectPitchRandomness);

        if (Random.Range(0f, 2f) > 0)
        {
            _audioSource.clip = gerericFootSound;
        }
        else
        {
            _audioSource.clip = metalFootSound;
        }

        _audioSource.Play();
    }


}
