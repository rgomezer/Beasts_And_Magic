using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOffline : MonoBehaviour {

    //[SerializeField] private bool useMobileControls = false;
    [SerializeField] private bool player2 = false;
    private PlayerData playerData;

    public AndroidJoystick joyStick;
    public GameObject otherPlayer;

    private PlayerAnimOffline anim;
    private PlayerAttackOffline pAttack;
    private float movementDirTemp;

    private OfflineGameController pController;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<PlayerAnimOffline>();
        Debug.Assert(anim != null);

        playerData = GetComponent<PlayerData>();
        Debug.Assert(playerData != null);

        pAttack = GetComponent<PlayerAttackOffline>();
        Debug.Assert(pAttack != null);

        pController = GameObject.Find("GameManager").GetComponent<OfflineGameController>();
        Debug.Assert(pController != null);
    }

    // Update is called once per frame
    void Update()
    {
        if (!player2)
        {
            if (Input.GetKey(KeyCode.W) && !pAttack.getBlock && !pAttack.getAttack2 && !pController.getGameOver)
            {
                movementDirTemp = playerData.movementSpeed * Time.deltaTime;

                transform.Translate(0.0f, 0.0f, movementDirTemp);
                transform.LookAt(otherPlayer.transform);
                anim.walkForward();
            }

            else if (Input.GetKey(KeyCode.S) && !pAttack.getBlock && !pAttack.getAttack2 && !pController.getGameOver)
            {
                movementDirTemp = -playerData.movementSpeed * Time.deltaTime;

                transform.Translate(0.0f, 0.0f, movementDirTemp);
                transform.LookAt(otherPlayer.transform);
                anim.walkBackward();
            }

            else if (Input.GetKey(KeyCode.D) && !pAttack.getBlock && !pAttack.getAttack2 && !pController.getGameOver)
            {
                movementDirTemp = playerData.movementSpeed * Time.deltaTime;

                transform.Translate(movementDirTemp, 0.0f, 0.0f);
                transform.LookAt(otherPlayer.transform);
                anim.walkRight();
            }

            else if (Input.GetKey(KeyCode.A) && !pAttack.getBlock && !pAttack.getAttack2 && !pController.getGameOver)
            {
                movementDirTemp = -playerData.movementSpeed * Time.deltaTime;

                transform.Translate(movementDirTemp, 0.0f, 0.0f);
                transform.LookAt(otherPlayer.transform);
                anim.walkLeft();
            }

            else if ((Input.GetKeyUp(KeyCode.W)) || (Input.GetKeyUp(KeyCode.S)) || (Input.GetKeyUp(KeyCode.D)) || (Input.GetKeyUp(KeyCode.A)))
            {
                anim.stopWalking();
            }
            else
            {
                if (!pAttack.getBlock && !pAttack.getAttack2 && !pController.getGameOver)
                {
                    transform.Translate(joyStick.InputDirection * playerData.movementSpeed * Time.deltaTime);
                    transform.LookAt(otherPlayer.transform);
                    //anim.walk(joyStick.InputDirection * playerData.movementSpeed * Time.deltaTime, playerData.movementSpeed);
                }
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                movementDirTemp = playerData.movementSpeed * Time.deltaTime;

                transform.Translate(0.0f, 0.0f, movementDirTemp);
                transform.LookAt(otherPlayer.transform);
                //anim.walkForward(movementDirTemp, playerData.movementSpeed);
            }

            else if (Input.GetKey(KeyCode.DownArrow))
            {
                movementDirTemp = -playerData.movementSpeed * Time.deltaTime;

                transform.Translate(0.0f, 0.0f, movementDirTemp);
                transform.LookAt(otherPlayer.transform);
                //anim.walkBackward(movementDirTemp, playerData.movementSpeed);
            }

            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                movementDirTemp = playerData.movementSpeed * Time.deltaTime;

                transform.Translate(movementDirTemp, 0.0f, 0.0f);
                transform.LookAt(otherPlayer.transform);
                //anim.walkLateral(movementDirTemp, playerData.movementSpeed);
            }

            else if (Input.GetKey(KeyCode.RightArrow))
            {
                movementDirTemp = -playerData.movementSpeed * Time.deltaTime;

                transform.Translate(movementDirTemp, 0.0f, 0.0f);
                transform.LookAt(otherPlayer.transform);
                //anim.walkLateral(movementDirTemp, playerData.movementSpeed);
            }

            //ugly i know, but won't be used in final version anyway. Just there for debugging
            else if ((Input.GetKeyUp(KeyCode.UpArrow)) || (Input.GetKeyUp(KeyCode.DownArrow)) || (Input.GetKeyUp(KeyCode.LeftArrow)) || (Input.GetKeyUp(KeyCode.RightArrow)))
            {
                //anim.stopWalking();
            }

        }

    }
}
