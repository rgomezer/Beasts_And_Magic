using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HealthManager : NetworkBehaviour {

    [SerializeField] private GameObject player;
   
    public PlayerData mplayerData;

    //private PlayerAnimation anim;
    private bool find;

    public Text healthText;
    public Text healthText1;
    public Text healthText2;

    public Text p1healthText;
    [SerializeField] private string player1HealthText;

    public Text p2healthText;
    [SerializeField] private string player2HealthText;

    public void Start()
    {
        if (gameObject.tag == "Player1")
            healthText = GameObject.Find("Player1HealthText").GetComponent<Text>();
        else
        if (gameObject.tag == "Player2")
            healthText = GameObject.Find("Player2HealthText").GetComponent<Text>();

        mplayerData = GetComponent<PlayerData>();
        //Debug.Log("Gameobject:" + gameObject.name + "   HelthText:" + healthText.name);


        //Debug.Log("Plyerdata:" + mplayerData.name);
        player = gameObject;
        if (mplayerData == null)
            return;
        mplayerData.currentHealth = mplayerData.characterHealthPool;

    }

    public void Update()
    {
        if (mplayerData == null)
            return;
       if((mplayerData.currentHealth <= 0) && (mplayerData.isAlive))
        {
            if (player != null)
            {
                player.GetComponent<PlayerAnim>().death();
                mplayerData.isAlive = false;
            }
        }
    }


    public void takeDamage(GameObject player, int damage)
    {
        CmdDoDamage(player, damage);
    }

    [Command]
    void CmdDoDamage(GameObject player, int damage)
    {
        player.GetComponent<PlayerData>().currentHealth -= damage;
        Debug.Log(player.tag + "    Damage:" + player.GetComponent<PlayerData>().currentHealth);

    }

}
