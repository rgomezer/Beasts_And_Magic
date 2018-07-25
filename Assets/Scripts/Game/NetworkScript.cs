using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Networking;



public class NetworkScript : NetworkBehaviour
{
    [SerializeField] private float playerSpeed = 1.0f;
    [SerializeField] private bool useMobileControls = false;

    public AndroidJoystick joyStick;
    //CharacterController controller;
    // Use this for initialization
    void Start()
    {
        //controller = GetComponent<CharacterController>();

        if (isServer)
        {

            //  Vector3 v3 = new Vector3(-3, 0, 2);
            // Quaternion q = new Quaternion(0, 0, 0, 0);
            // this.gameObject.transform.SetPositionAndRotation(v3,q);
            //this.transform.position.Set(3, 3, 3);
            transform.Translate(0.0f, 0.0f, 6f * Time.deltaTime);

        }


    }
    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;

    }
    // Update is called once per frame
    void Update()
    {

        useMobileControls = false;
        if (!useMobileControls)
        {
            if (!isLocalPlayer)
                return;
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(0.0f, 0.0f, playerSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0.0f, 0.0f, -playerSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(playerSpeed * Time.deltaTime, 0.0f, 0.0f);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-playerSpeed * Time.deltaTime, 0.0f, 0.0f);
            }
        }
        else
        {
            transform.Translate(joyStick.InputDirection * playerSpeed * Time.deltaTime);
            //controller.Move(joyStick.InputDirection * playerSpeed * Time.deltaTime);
        }
    }
}
