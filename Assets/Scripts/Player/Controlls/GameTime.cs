using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.ComponentModel;
using System;
using UnityEngine.SceneManagement;
using Prototype.NetworkLobby;


public class GameTime : NetworkBehaviour
{
    [SyncVar]
    public int iTime;
    public int timeLimited;
    int s;
    int hight;
    int width;
    Text timeText;
    Text testText;

    bool isStart;
    [SyncVar]
    string WinnerName;

    Text healthText1;
    Text healthText2;
    Text ServerNameText;
    Text ClientNameText;
    DateTime now;
    NetworkManagerHUD hud;

    bool isOpen;
    GameObject go;
    GameObject objPlayer;
    GameObject btno;
    Text winName;
    Button btn;

    // Use this for initialization
    void Start()
    {
 

        iTime = 0;
        healthText1 = GameObject.Find("Player1HealthText").GetComponent<Text>();
        healthText2 = GameObject.Find("Player2HealthText").GetComponent<Text>();
        ServerNameText = GameObject.Find("Player1NameText").GetComponent<Text>();
        ClientNameText = GameObject.Find("Player2NameText").GetComponent<Text>();
        timeText = GameObject.Find("GameTimeText").GetComponent<Text>();
        //hud = NetworkManager.singleton.GetComponent<NetworkManagerHUD>();
        //if (hud == null)
        //{
        //    return;
        //}
        //       go = GameObject.Find("myPanel");
        go = findGameObj("myPanel");
        objPlayer = findGameObj("Player");
        winName = objPlayer.GetComponent<Text>();
        //winName = GameObject.Find("Player").GetComponent<Text>();
        //btn = GameObject.Find("Button").GetComponent<Button>();

        isStart = true;
        timeText.text = "00";
        iTime = 0;
        if (timeLimited == 0)
            timeLimited = 90;

    }
    GameObject findGameObj(string name)
    {
        List<GameObject> objList = GetAllObjectsInScene();
        foreach(GameObject o in objList)
        {
            if (o.name == name) return o;
        }
        return null;
    }
    List<GameObject> GetAllObjectsInScene()
    {
        List<GameObject> objectsInScene = new List<GameObject>();

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave)
                continue;

 //           if (!EditorUtility.IsPersistent(go.transform.root.gameObject))
 //               continue;

            objectsInScene.Add(go);
        }

        return objectsInScene;
    }


    private void OnDisconnectedFromServer(NetworkDisconnection info)
    {


        timeText.text = "00";
    }




    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;

        // hight = Camera.main.pixelHeight;
        //width = Camera.main.pixelWidth;

        //timeText.rectTransform.position = (new Vector3((width-320) / 2, -40));

        if (NetworkManager.singleton.numPlayers > 1)
        {
            if (isStart)
            {
                now = DateTime.Now;
                isStart = false;
            }
            TimeSpan t = DateTime.Now.Subtract(now);

            iTime = timeLimited - (int)t.TotalSeconds;

            if (iTime <= 0)
            {
                int p1 = int.Parse(healthText1.text);
                int p2 = int.Parse(healthText2.text);
                if (p1 > p2)
                {
                    WinnerName = ServerNameText.text;
                }
                else
                {
                    if (p2 > p1)
                    {
                        WinnerName = ClientNameText.text;

                    }
                    else
                    {
                        if (p1 == p2)
                        {
                            WinnerName = "Dogfall";
                        }

                    }


                }
                if (isServer)
                {
                    RpcExit(WinnerName);
                    //StopServer();
                }
                else
                {
                    if (!isServer && isClient)
                    {
                        CmdExit(WinnerName);
                    }

                }
                PlayerPrefs.SetString("Winner", WinnerName);
                //GameObject lobby = GameObject.Find("LobbyManager");
                //LobbyManager lm = lobby.GetComponent<LobbyManager>();
                //lm.GoBackButton();
                //NetworkManager.Shutdown();
                //NetworkManager.singleton.StopHost();
                isStart = true;
                winName.text = WinnerName;
                go.SetActive(true);

                //SceneManager.LoadScene("RecordScene");
            }

        }

    }
    private void StopServer()
    {
        //LobbyManager lm = null;

        if (NetworkManager.singleton != null)
        {

            NetworkManager.singleton.StopMatchMaker();
        }
    }
    [ClientRpc]
    public void RpcExit(string name)
    {
        WinnerName = name;
        PlayerPrefs.SetString("Winner", WinnerName);
        go.SetActive(true);
        //SceneManager.LoadScene("RecordScene");

    }
    [Command]
    public void CmdExit(string name)
    {
        WinnerName = name;
        PlayerPrefs.SetString("Winner", WinnerName);
        go.SetActive(true);
//        SceneManager.LoadScene("RecordScene");

    }

    private void OnGUI()
    {

        //hud.offsetX =(int) ( -100)/2 ;

        string s = iTime.ToString("D2");
        timeText.text = s;

    }
}
