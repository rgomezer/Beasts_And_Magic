using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProjectileCollision : NetworkBehaviour
{
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private int blockDurabilityNum = 4;

    //private int tempBlockDurability;
    private AudioSource explosionSound;
    
    // Use this for initialization
    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        Debug.Assert(healthManager != null);

        explosionSound = GetComponent<AudioSource>();

        //tempBlockDurability = blockDurabilityNum;
    }

    void OnCollisionEnter(Collision other)
    {

        explosionSound.Play();

        if (other.gameObject.tag == "Player2" || other.gameObject.tag == "Player1")
        {
            PlayerAnim panim = other.gameObject.GetComponent<PlayerAnim>();
            PlayerData pData = other.gameObject.GetComponent<PlayerData>();

            Debug.Log("Collided with: " + other.gameObject.name);
            //Debug.Log("Player Attack 1: " + pAttack.getAttack1);

            if (!gameObject.GetComponent<SpellFire>().getStrongAttack)
            {
                healthManager.takeDamage(other.gameObject, pData.lightAttackDamage);
                panim.lightHit();
            }
            else
            {
                healthManager.takeDamage(other.gameObject, pData.heavyAttackDamage);
                panim.lightHit();
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}