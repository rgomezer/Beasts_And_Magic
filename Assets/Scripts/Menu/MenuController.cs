using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour {

    [SerializeField] GameObject LobbyObject;
    [SerializeField] GameObject MenuObject;
    [SerializeField] GameObject CharacterObject;

    // Use this for initialization
    void Start () {

        Debug.Assert(LobbyObject != null);
        LobbyObject.SetActive(false);
        CharacterObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartNetworkLobby()
    {
        LobbyObject.SetActive(true);
        MenuObject.SetActive(false);
        CharacterObject.SetActive(false);
    }

    public void StartMenu()
    {
        LobbyObject.SetActive(false);
        MenuObject.SetActive(true);
        CharacterObject.SetActive(false);
    }

    public void StartPracticeScene()
    {
        LobbyObject.SetActive(false);
        MenuObject.SetActive(false);
        CharacterObject.SetActive(false);
        SceneManager.LoadScene(GameConstants.LEVELS.MainWorldPractice.ToString());
    }

    public void StartCharacterSelection()
    {
        LobbyObject.SetActive(false);
        MenuObject.SetActive(false);
        CharacterObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
