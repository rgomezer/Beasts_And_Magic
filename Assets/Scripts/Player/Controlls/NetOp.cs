using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
//using UnityEngine.Windows;
using Prototype.NetworkLobby;

public class NetOp : NetworkBehaviour
{

    //[SerializeField]
    //private bool player2 = false;
    //private PlayerData playerData;
    public GameObject cam;
    //private UnityStandardAssets.Cameras.AutoCam theCamera;

    //public AndroidJoystick joyStick;
    //private PlayerAnimation anim;
    public GameObject otherPlayer;

    //private float movementDirTemp;

    static GameObject goClient;
    static GameObject goServer;

    [SyncVar]
    string Player1Name;

    [SyncVar]
    string Player2Name;

    Text healthText1;
    Text healthText2;
    Text ServerNameText;
    Text ClientNameText;
    Slider slid1;
    Slider slid2;
    Slider[] slider;

    [SyncVar]
    bool isEnd;
    [SyncVar]
    string WinnerName;
    bool isOpen;
    GameObject go;
    Text winName;
    Button btn;
    GameObject objPlayer;

    private void Awake()
    {
    }
    // Use this for initialization
    void Start()
    {
        isOpen = false;
        go = findGameObj("myPanel");
        objPlayer = findGameObj("Player");
        winName = objPlayer.GetComponent<Text>();
        //btn = GameObject.Find("Button").GetComponent<Button>();
        go.SetActive(isOpen);

        isEnd = false;
        //theCamera = Camera.main.GetComponentInParent<UnityStandardAssets.Cameras.AutoCam>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        //theCamera = cam.GetComponentInParent<UnityStandardAssets.Cameras.AutoCam>();

        //theCamera = Camera.main.GetComponentInParent<UnityStandardAssets.Cameras.AutoCam>();

        //anim = GetComponent<PlayerAnimation>();
        //playerData = GetComponent<PlayerData>();
        //otherPlayer = GetComponent<Movement>().otherPlayer;
        healthText1 = GameObject.Find("Player1HealthText").GetComponent<Text>();
        healthText2 = GameObject.Find("Player2HealthText").GetComponent<Text>();

        ServerNameText = GameObject.Find("Player1NameText").GetComponent<Text>();
        ClientNameText = GameObject.Find("Player2NameText").GetComponent<Text>();

        slid1 = GameObject.Find("SliderA").GetComponents<Slider>()[0];
        slid2 = GameObject.Find("SliderB").GetComponents<Slider>()[0];
        int healthValue1 = GetComponent<PlayerData>().currentHealth;
        int healthValue2 = GetComponent<PlayerData>().currentHealth;
        healthText1.text = healthValue1.ToString();
        healthText2.text = healthValue2.ToString();
        slid2.maxValue = healthValue2;
        slid1.maxValue = healthValue1;
        otherPlayer = null;

        Player1Name = PlayerPrefs.GetString("Player1");
        Player2Name = PlayerPrefs.GetString("Player2");
        ServerNameText.text = Player1Name;
        ClientNameText.text = Player2Name;

    }
    GameObject findGameObj(string name)
    {
        List<GameObject> objList = GetAllObjectsInScene();
        foreach (GameObject o in objList)
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
    public GameObject getOtherPlayer()
    {
        return otherPlayer;
    }
    public GameObject getServerPlayer()
    {
        return goServer;
    }
    public GameObject getClientPlayer()
    {
        return goClient;
    }
    public override void OnStartClient()
    {
        if (gameObject.Equals(goServer)) return;

        goClient = gameObject;
        // goClient.tag = "ClientPlayer";
        goClient.name = "ClientPlayer";


    }

    public override void OnStartServer()
    {
        if (goServer == null)
        {


            goServer = gameObject;
            //goServer.tag = "ServerPlayer";

        }
    }

    [ClientRpc]
    public void RpcSetOther(GameObject gameObject)
    {
        if (goServer == null)
            goServer = gameObject;
    }
    [ClientRpc]
    public void RpcSetPlayer1Name(string iPlayer1Name)
    {
        if (ServerNameText == null) return;
        ServerNameText.text = iPlayer1Name;

    }

    [ClientRpc]
    public void RpcSetPlayer2Name(string iPlayer2Name)
    {
        if (ClientNameText == null) return;
        ClientNameText.text = iPlayer2Name;
    }
    [ClientRpc]
    public void RpcExitToMenu(string Name)
    {
        //if (ServerNameText == null) return;
        WinnerName = Name;
        PlayerPrefs.SetString("Winner", WinnerName);
        winName.text = WinnerName;
        go.SetActive(true);

        //SceneManager.LoadScene("RecordScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (goClient != null)
        {

            healthText2.text = goClient.GetComponent<PlayerData>().currentHealth.ToString();
            //Debug.Log("Health2="+healthText2.text);
            if (slid1 != null)
                slid2.value = goClient.GetComponent<PlayerData>().currentHealth;

        }

        if (goServer != null)
        {

            healthText1.text = goServer.GetComponent<PlayerData>().currentHealth.ToString();
            //Debug.Log("Health1=" + healthText1.text);
            if (slid1 != null)
                slid1.value = goServer.GetComponent<PlayerData>().currentHealth;


        }

        if (gameObject.Equals(goServer))
        {


            if (goClient != null)
            {
                //GetComponent<Movement>().setOther(goClient);
                //Debug.Log("***Other(Client)="+goClient);
                //GetComponent<Movement>().otherPlayer=goClient;
                otherPlayer = goClient;
            }
        }
        if (gameObject.Equals(goClient))
        {

            if (goServer != null)
            {
                //GetComponent<Movement>().setOther(goServer);
                //Debug.Log("***Other(Server)=" + goServer);
                //GetComponent<Movement>().otherPlayer=goServer;
                otherPlayer = goServer;
            }
        }
        if (isServer && goServer != null)
        {
            RpcSetOther(goServer);
            if (Player1Name == "")
                Player1Name = "PlayerA";
            if (Player2Name == "")
                Player2Name = "PlayerA";
            RpcSetPlayer1Name(Player1Name);
            RpcSetPlayer2Name(Player2Name);
            int ServerHealthValue = goServer.GetComponent<PlayerData>().currentHealth;
            if (ServerHealthValue < 1)
            {
                WinnerName = Player2Name;
                isEnd = true;
                RpcExitToMenu(WinnerName);
                PlayerPrefs.SetString("Winner", WinnerName);
                winName.text = WinnerName;
                go.SetActive(true);
                //StopServer();
                //SceneManager.LoadScene("RecordScene");
            }
            
            if (goClient == null) return;
            int ClientHealthValue = goClient.GetComponent<PlayerData>().currentHealth;
            if (ClientHealthValue < 1)
            {
                WinnerName = Player1Name;
                isEnd = true;
                RpcExitToMenu(WinnerName);
                PlayerPrefs.SetString("Winner", WinnerName);
                winName.text = WinnerName;
                go.SetActive(true);
                //SceneManager.LoadScene("RecordScene");
                //StopServer();
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
    public override void OnStartLocalPlayer()
    {
        cam.SetActive(true);

        if (!isServer)
        {
            gameObject.transform.Find("Player1Icon").GetComponent<MeshRenderer>().material.color = Color.blue;
            gameObject.transform.Find("PersonalSpace").GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else
        {
            if (isLocalPlayer && isClient)
            {
                gameObject.transform.Find("Player1Icon").GetComponent<MeshRenderer>().material.color = Color.blue;
                gameObject.transform.Find("PersonalSpace").GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }
    }
}
