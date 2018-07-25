using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class EndWndManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClickexit()
    {
        LobbyManager lm = null;

        if (NetworkManager.singleton != null)
        {
            if (NetworkManager.singleton.IsClientConnected())
            {
                NetworkManager.singleton.StopClient();
            }
            NetworkManager.singleton.StopServer();
        }

        Debug.Log("Networked Clicked");
        GameObject lobby = GameObject.Find("LobbyManager");
        if (lobby != null)
            lm = lobby.GetComponent<LobbyManager>();
        if (lm != null)
            lm.StopAndExit();
        SceneManager.LoadScene("menuScene");
    }
}
