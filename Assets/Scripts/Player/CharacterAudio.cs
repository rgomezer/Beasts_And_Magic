using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CharacterAudio : MonoBehaviour {

    public AudioClip Hit;
    public AudioSource audioSource;


    // Use this for initialization
    void Start()
    {

    }

    void hitSound()
    {
        audioSource.Play();
    }


}
