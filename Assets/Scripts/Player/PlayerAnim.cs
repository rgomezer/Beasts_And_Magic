using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAnim : NetworkBehaviour {

    [SerializeField] private Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isIdle", true);
    }

    public void walk(Vector3 InputDirection, float speed)
    {
        //Debug.Log(InputDirection.z);

        if (Equals(InputDirection.x, 0.0f) && Equals(InputDirection.y, 0.0f) && Equals(InputDirection.z, 0.0f))
        {
            stopWalking();
        }
        if (InputDirection.z > 0.0f)
        {
            walkForward();
        }
        if (InputDirection.z < 0.0f)
        {
            walkBackward();
        }

    }
    public void stopWalking()
    {
        animator.SetBool("isIdle", true);
        animator.SetBool("walkingForward", false);
        animator.SetBool("walkingBackward", false);
        animator.SetBool("walkingRight", false);
        animator.SetBool("walkingLeft", false);

    }
    public void walkForward()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("walkingForward", true);
        animator.SetBool("walkingBackward", false);
        animator.SetBool("walkingRight", false);
        animator.SetBool("walkingLeft", false);
        //animator.SetFloat("movementSpeed", speed);
    }
    public void walkBackward()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("walkingForward", false);
        animator.SetBool("walkingBackward", true);
        animator.SetBool("walkingRight", false);
        animator.SetBool("walkingLeft", false);
    }
    public void walkRight()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("walkingForward", false);
        animator.SetBool("walkingBackward", false);
        animator.SetBool("walkingRight", true);
        animator.SetBool("walkingLeft", false);
        animator.SetBool("blocking", false);
    }
    public void walkLeft()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("walkingForward", false);
        animator.SetBool("walkingBackward", false);
        animator.SetBool("walkingRight", false);
        animator.SetBool("walkingLeft", true);
        animator.SetBool("blocking", false);
    }


    public void death()
    {
        animator.Play("death");
    }

    public void attackOne(bool isAttacking)
    {
        animator.Play("lightAttack");
    }

    public void attackTwo(bool isAttacking)
    {
        animator.Play("heavyAttack");
    }

    public void lightHit()
    {
        animator.Play("lightHit");
    }

    public void blocking()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("walkingForward", false);
        animator.SetBool("walkingBackward", false);
        animator.SetBool("walkingRight", false);
        animator.SetBool("walkingLeft", false);
        animator.SetBool("blocking", true);
    }

    public void unBlocking()
    {
        animator.SetBool("isIdle", true);
        animator.SetBool("walkingForward", false);
        animator.SetBool("walkingBackward", false);
        animator.SetBool("walkingRight", false);
        animator.SetBool("walkingLeft", false);
        animator.SetBool("blocking", false);
    }

    public void blockingImpact()
    {
        animator.Play("blockImpact");
    }
    public void blockBreak()
    {
        animator.Play("defenseBroken");
    }
}
