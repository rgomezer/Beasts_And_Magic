using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpellFire : NetworkBehaviour {

    [SerializeField] private float projectileSpeed = 10.0f;
    [SerializeField] private bool destroyOnStart = false;

    private Rigidbody bulletRigidBody;
    private bool strongAttack;

	// Use this for initialization
	void Start () {

        bulletRigidBody = gameObject.GetComponent<Rigidbody>();
        Debug.Assert(bulletRigidBody != null);
        strongAttack = false;

        if(destroyOnStart)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {

        //if(!isServer)
        //{
        //    return;
        //}

        bulletRigidBody.velocity = transform.forward * projectileSpeed;
	}

    public void setStrongAttack(bool value)
    {
        strongAttack = value;
    }

    public bool getStrongAttack
    {
        get { return strongAttack; }
    }
}
