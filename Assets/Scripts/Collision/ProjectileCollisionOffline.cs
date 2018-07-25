using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionOffline : MonoBehaviour {

    [SerializeField] private HealthManagerOffline healthManager;
    [SerializeField] private int blockDurabilityNum = 4;

    //private int tempBlockDurability;

    // Use this for initialization
    void Start()
    {
        healthManager = FindObjectOfType<HealthManagerOffline>();
        Debug.Assert(healthManager != null);

        //tempBlockDurability = blockDurabilityNum;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player2" || other.gameObject.tag == "Player1")
        {
            //PlayerAnimation panim = other.gameObject.GetComponent<PlayerAnimation>();
            PlayerData pData = other.gameObject.GetComponent<PlayerData>();

            Debug.Log("Collided with: " + other.gameObject.name);
            //Debug.Log("Player Attack 1: " + pAttack.getAttack1);

            //panim.lightHit();

            if (!gameObject.GetComponent<ProjectileOffline>().getStrongAttack)
            {
                healthManager.takeDamage(other.gameObject, pData.lightAttackDamage);
                //panim.lightHit();
            }
            else
            {
                healthManager.takeDamage(other.gameObject, pData.heavyAttackDamage);
                //panim.lightHit();
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
