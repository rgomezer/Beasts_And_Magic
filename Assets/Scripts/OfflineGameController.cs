using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfflineGameController : MonoBehaviour {

    [SerializeField] private GameObject endGameWindow;

    private bool gameOver;
    private int playerHealth1;
    private int playerHealth2;

	// Use this for initialization
	void Start () {

        gameOver = false;

        Debug.Assert(endGameWindow != null);
        endGameWindow.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (!gameOver)
        {
            playerHealth1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerData>().currentHealth;
            playerHealth2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerData>().currentHealth;

            if (playerHealth1 <= 0 || playerHealth2 <= 0)
            {
                endGameWindow.SetActive(true);
                gameOver = true;
            }
        }
		
	}

    public bool getGameOver
    {
        get { return gameOver; }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(GameConstants.LEVELS.MenuScene.ToString());
    }
}
