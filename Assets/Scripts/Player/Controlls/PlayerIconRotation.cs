using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIconRotation : MonoBehaviour {

    [SerializeField] private float rotationSpeead = 1.5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0.0f, rotationSpeead, 0.0f);	
	}
}
