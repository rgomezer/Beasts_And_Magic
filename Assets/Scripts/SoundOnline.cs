using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class SoundOnline : MonoBehaviour{

    private AudioSource source;
    public AudioClip lightAttack;
    public AudioClip heavyAttack;

    public AudioClip lightAttacked;
    public AudioClip heavyAttacked;

    public AudioClip hit;
    public AudioClip blockHit;

    // Use this for initialization
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void lightAttackSound()
    {
        source.PlayOneShot(lightAttack, 1.0f);
    }
    public void heavyAttackSound()
    {
        source.PlayOneShot(heavyAttack, 1.0f);
    }

    public void hitSound()
    {
        source.PlayOneShot(hit, 1.0f);
    }
    public void blockHitSound()
    {
        source.PlayOneShot(blockHit, 1.0f);
    }
    public void HitedSound()
    {
        source.PlayOneShot(blockHit, 1.0f);
    }
    public void lightAttackedSound()
    {
        source.PlayOneShot(lightAttack, 1.0f);
    }
    public void heavyAttackedSound()
    {
        source.PlayOneShot(heavyAttack, 1.0f);
    }
}
