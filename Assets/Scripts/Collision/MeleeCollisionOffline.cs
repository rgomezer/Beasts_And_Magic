using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollisionOffline : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    [SerializeField] private bool offlineMode;
    [SerializeField] private int blockDurabilityNum = 4;

    private PlayerAttack pAttack;
    private PlayerAttackOffline pAttackO;
    private PlayerData pData;
    private HealthManagerOffline healthManager;

    private bool isAttacking;

    private int tempBlockDurability;

    // Use this for initialization
    void Start()
    {
        tempBlockDurability = blockDurabilityNum;
        healthManager = FindObjectOfType<HealthManagerOffline>();
        Debug.Assert(healthManager != null);

        isAttacking = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == null)
        {
            return;
        }

        isAttacking = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == null)
        {
            return;
        }

        //PlayerAnimation panim = other.gameObject.GetComponent<PlayerAnimation>();
        PlayerAttackOffline pAttack = GetComponent<PlayerAttackOffline>();
        PlayerData pData = other.GetComponent<PlayerData>();
        if (pAttack == null) return;
        if (pData == null) return;
        if (pData.currentHealth <= 0) return;

        if (!isAttacking && pAttack.getAttack2 || pAttack.getAttack1)
        {
            isAttacking = true;

            if (other.gameObject.GetComponent<ForceBlock>().getBlock)
            {
                Debug.Log("Collided with: " + other.gameObject.name);
                Debug.Log("Other Player Block: " + other.gameObject.GetComponent<ForceBlock>().getBlock);

                tempBlockDurability--;
                Debug.Log("Block Durability: " + tempBlockDurability);

                //Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                //rb.AddForceAtPosition(-other.gameObject.transform.forward, other.gameObject.transform.position);

                healthManager.takeDamage(other.gameObject, 5);

                if (tempBlockDurability <= 0)
                {
                    other.gameObject.GetComponent<ForceBlock>().setBlock(false);
                    other.gameObject.GetComponent<ForceBlock>().setBreakBlockEffect(true);
                    //panim.blockBreak();
                    tempBlockDurability = blockDurabilityNum;
                    Debug.Log("Block Durability: " + tempBlockDurability);
                }

                return;
            }

            if (pAttack.getAttack1)
            {
                Debug.Log("Collided with: " + other.gameObject.name);
                Debug.Log("Player Attack 1: " + pAttack.getAttack1);

                //panim.lightHit();
                healthManager.takeDamage(other.gameObject, 10);

                return;
            }

            if (pAttack.getAttack2)
            {
                Debug.Log("Collided with: " + other.gameObject.tag + "  name:" + other.gameObject.name);
                Debug.Log("Player Attack 2: " + pAttack.getAttack2);
                //panim.lightHit();
                healthManager.takeDamage(other.gameObject, 20);
                return;
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == null)
        {
            return;
        }

        isAttacking = false;
    }
}
