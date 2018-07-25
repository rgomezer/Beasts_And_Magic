using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerData : NetworkBehaviour
{

    //default fields that SHOULD be overridden in the prefab.
    public float movementSpeed = 3.0f;
    [SyncVar] 
    public int currentHealth;
    public int characterHealthPool = 100;

    public float attack1CoolDown = 0.5f;
    public float attack2CoolDown = 1.5f;

    public int strength = 10;  //physical attacks
    public int agility = 10;   //blocking
    public int wisdom = 10;    //ranged attacks

    public int lightAttackDamage = 10;
    public int heavyAttackDamage = 20;

    public bool isAlive = true;
    public bool isBlocking = false;

    private void Start()
    {
        currentHealth = characterHealthPool;
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }
}