using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodControl : MonoBehaviour {
    Text healthText1;
    Text healthText2;
    Slider slid1;
    Slider slid2;
   [SerializeField] private GameObject Player1;
   [SerializeField] private GameObject Player2;
    public int PlayerNumber;

    // Use this for initialization
    void Start () {
        healthText1 = GameObject.Find("Player1HealthText").GetComponent<Text>();
        healthText2 = GameObject.Find("Player2HealthText").GetComponent<Text>();

        slid1 = GameObject.Find("SliderA").GetComponents<Slider>()[0];
        slid2 = GameObject.Find("SliderB").GetComponents<Slider>()[0];
        slid2.maxValue = 150;
        slid1.maxValue = 150;
        slid2.minValue = 0;
        slid1.minValue = 0;

        if(Player1 == null)
            Player1=GameObject.Find("Beast - Player 1 Offline");

        if(Player2 == null)
            Player2 = GameObject.Find("Beast - Player 2 - Offline");
    }

    // Update is called once per frame
    void Update () {
        int BloodNumber;

        if (PlayerNumber == 1)
        {
            BloodNumber = Player2.GetComponent<PlayerData>().currentHealth;
            slid2.value = BloodNumber;
            healthText2.text = BloodNumber.ToString();
        }
        if (PlayerNumber == 2)
        {
            BloodNumber=Player1.GetComponent<PlayerData>().currentHealth;
            slid1.value = BloodNumber;
            healthText1.text = BloodNumber.ToString();
           
        }
    }
}
