using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class SelectPlayer : MonoBehaviour
{


    string strSelectedPlayer;

    // Use this for initialization
    void Start()
    {
        //PlayerPrefs.SetString("SelectedPlayer", "PlayerA");

        strSelectedPlayer = GenSelectPlayer();

    }
    private string GenSelectPlayer()
    {
        string dir = Directory.GetCurrentDirectory();
        UInt32 hash = (UInt32)dir.GetHashCode();
        string strHash = "SelectedPlayer" + hash.ToString();

        return strHash;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SelectPlayerA()
    {
        Debug.Log("A slected.");
        PlayerPrefs.SetString(strSelectedPlayer, "Beast");
        //        PlayerPrefs.SetString("SelectedPlayer", "Beast");

        SceneManager.LoadScene(GameConstants.LEVELS.MenuScene.ToString());
    }
    public void SelectPlayerB()
    {
        Debug.Log("B slected.");
        PlayerPrefs.SetString(strSelectedPlayer, "Mage");
        //       PlayerPrefs.SetString("SelectedPlayer", "Mage");

        SceneManager.LoadScene(GameConstants.LEVELS.MenuScene.ToString());
    }
    public void SelectPlayerC()
    {
        Debug.Log("C slected.");
        PlayerPrefs.SetString(strSelectedPlayer, "paladin");
        

        SceneManager.LoadScene(GameConstants.LEVELS.MenuScene.ToString());
    }
    public void SelectPlayerAToLobby()
        {
            Debug.Log("A slected.");
            PlayerPrefs.SetString(strSelectedPlayer, "Beast");
            //        PlayerPrefs.SetString("SelectedPlayer", "Beast");

            SceneManager.LoadScene(GameConstants.LEVELS.LobbyScene.ToString());
        }

        public void SelectPlayerBToLobby()
        {
            Debug.Log("B slected.");
            PlayerPrefs.SetString(strSelectedPlayer, "Mage");
            //       PlayerPrefs.SetString("SelectedPlayer", "Mage");

            SceneManager.LoadScene(GameConstants.LEVELS.LobbyScene.ToString());

        }
    public void SelectPlayerCToLobby()
    {
        Debug.Log("C slected.");
        PlayerPrefs.SetString(strSelectedPlayer, "Paladin");
        //       PlayerPrefs.SetString("SelectedPlayer", "Mage");

        SceneManager.LoadScene(GameConstants.LEVELS.LobbyScene.ToString());

    }

}