using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : NetworkBehaviour
{
    //[SerializeField] private bool useMobileControls = false;
    [SerializeField] private bool player2 = false;
    [SerializeField] private AndroidJoystick joyStick;

    private PlayerData playerData;
    //private UnityStandardAssets.Cameras.AutoCam theCamera;
    private NetOp netOp;
    
    private PlayerAnim anim;
    private PlayerAttack pAttack;
    private GameObject otherPlayer;
    private float movementDirTemp;

    // Use this for initialization
    void Start () {
        //theCamera = Camera.main.GetComponentInParent<UnityStandardAssets.Cameras.AutoCam>();

        if(joyStick == null)
        {
            joyStick = GameObject.Find("JoystickController").GetComponent<AndroidJoystick>();
            Debug.Assert(joyStick != null);
        }

        anim = GetComponent<PlayerAnim>();
        playerData = GetComponent<PlayerData>();
        pAttack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update () {
            if(otherPlayer==null)
            {
                otherPlayer = GetComponent<NetOp>().getOtherPlayer();
            }
        if (otherPlayer == null) return;
        //////////////////////////////////////////
        if ((!player2) && (playerData.isBlocking == false) && (playerData.isAlive == true) && !pAttack.getAttack1 && !pAttack.getAttack2)
        {
            if (Input.GetKey(KeyCode.W))
            {
                movementDirTemp = playerData.movementSpeed * Time.deltaTime;

                transform.Translate(0.0f, 0.0f, movementDirTemp);
                transform.LookAt(otherPlayer.transform);
                anim.walkForward();
            }

            else if (Input.GetKey(KeyCode.S))
            {
                movementDirTemp = -playerData.movementSpeed * Time.deltaTime;

                transform.Translate(0.0f, 0.0f, movementDirTemp);
                transform.LookAt(otherPlayer.transform);
                anim.walkBackward();
            }

            else if (Input.GetKey(KeyCode.D))
            {
                movementDirTemp = playerData.movementSpeed * Time.deltaTime;

                transform.Translate(movementDirTemp, 0.0f, 0.0f);
                transform.LookAt(otherPlayer.transform);
                anim.walkRight();
            }

            else if (Input.GetKey(KeyCode.A))
            {
                movementDirTemp = -playerData.movementSpeed * Time.deltaTime;

                transform.Translate(movementDirTemp, 0.0f, 0.0f);
                transform.LookAt(otherPlayer.transform);
                anim.walkLeft();
            }

            //ugly i know, but won't be used in final version anyway. Just there for debugging
            else if ((Input.GetKeyUp(KeyCode.W)) || (Input.GetKeyUp(KeyCode.S)) || (Input.GetKeyUp(KeyCode.D)) || (Input.GetKeyUp(KeyCode.A)))
            {
                anim.stopWalking();
            }
            else
            {
                //Debug.Log("yo");
                transform.Translate(joyStick.InputDirection * playerData.movementSpeed * Time.deltaTime);
                transform.LookAt(otherPlayer.transform);
                anim.walk(joyStick.InputDirection * playerData.movementSpeed * Time.deltaTime, playerData.movementSpeed);
            }

        }
        else
        {
            //anim.stopWalking();

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
    //public override void OnS
}
