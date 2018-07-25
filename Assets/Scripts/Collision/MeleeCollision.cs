using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MeleeCollision : NetworkBehaviour {

    [SerializeField] private GameObject playerObj;
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private bool offlineMode;

    private PlayerAttack pAttack;
    private PlayerAttackOffline pAttackO;
    private PlayerData pData;
    private bool hasCollided;

    // Use this for initialization
    void Start () {

        hasCollided = false;
        healthManager = FindObjectOfType<HealthManager>();
        Debug.Assert(healthManager != null);

        if (!offlineMode)
        {
            
        }
        else
        {
         //   pAttackO = playerObj.GetComponent<PlayerAttackOffline>();
         //   Debug.Assert(pAttack != null);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (!offlineMode)
        {
            if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2" && !hasCollided)
            {
                if (hasCollided)
                {
                    return;
                }

                //if (other.gameObject == null) return;
                PlayerAnim panim = other.gameObject.GetComponent<PlayerAnim>();
                PlayerAttack pAttack = GetComponent<PlayerAttack>();
                PlayerData pData = other.GetComponent<PlayerData>();
                if (pAttack == null)
                {
                    pAttack = playerObj.GetComponent<PlayerAttack>();
                }
                if (pData == null)
                {
                    return;
                }
                if (pData.currentHealth <= 0) return;
                if (pAttack.getAttack1)
                {
                    //sound.lightAttackedSound();
                    Debug.Log("Collided with: " + other.gameObject.name);
                    Debug.Log("Player Attack 1: " + pAttack.getAttack1);

                    // if (other.gameObject == null) return;
                    //                PlayerAnimation pa = other.gameObject.GetComponent<PlayerAnimation>();
                    //                if (pa == null) return;
                    panim.lightHit();
                    healthManager.takeDamage(other.gameObject, 10);
                }

                if (pAttack.getAttack2)
                {
                    Debug.Log("Collided with: " + other.gameObject.tag + "  name:" + other.gameObject.name);
                    Debug.Log("Player Attack 2: " + pAttack.getAttack2);

                    //if (other.gameObject == null) return;
                    //                PlayerAnimation pa = other.gameObject.GetComponent<PlayerAnimation>();
                    //                if (pa == null) return;
                    panim.lightHit();
                    healthManager.takeDamage(other.gameObject, 20);
                }

                hasCollided = true;
                StartCoroutine(EnableCollision());
            }
        }
    }

    private IEnumerator EnableCollision()
    {
       yield return new WaitForSeconds(1.5f);
       hasCollided = false;
    }
}
