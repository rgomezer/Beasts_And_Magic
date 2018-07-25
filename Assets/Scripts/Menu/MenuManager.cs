using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class MenuManager:MonoBehaviour {

    Text PlayerNameText;
    string PlayerName;

	public void OnClickPracticeButton() {
		Debug.Log ("Practice button clicked");
		SceneManager.LoadScene(GameConstants.LEVELS.MainWorldPractice.ToString());
	}

	public void OnClickNetworkButton() {
		Debug.Log ("Networked Clicked");
         SceneManager.LoadScene(GameConstants.LEVELS.CharacterMainWorldScene.ToString());
       LobbyManager lm = null;
        //NetworkManager.singleton.StopServer();
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
            lm.GoBackButton();
	}
    
	public void OnClickCharacterButton() {
        Debug.Log ("CharacterClicked");
        SceneManager.LoadScene(GameConstants.LEVELS.CharacterScene.ToString());
	}

	public void OnClickQuitButton() {
		Debug.Log ("Quit selected");
		Application.Quit();
	}

	public void LoadScene(string scene) {
		SceneManager.LoadScene (scene);
	}
    void Start()
    {
        //PlayerNameText = GameObject.Find("MenuPlayerText").GetComponent<Text>();
        //PlayerName=PlayerPrefs.GetString("SelectedPlayer");
        //if(PlayerName!="")
        //    PlayerNameText.text = "You selected game player is "+PlayerName+" now.";

        //Ensure full restart of multiplayer session
        GameObject lobbyInstance = GameObject.Find("LobbyManager");

        if(lobbyInstance != null)
        {
            Destroy(lobbyInstance);
        }

        PlayerPrefs.DeleteKey("Player1");
        PlayerPrefs.DeleteKey("Player2");
    }

    private void OnGUI()
    {

 

    }
}
