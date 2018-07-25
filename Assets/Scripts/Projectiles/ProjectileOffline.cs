using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOffline : MonoBehaviour {

    [SerializeField] private float projectileSpeed = 10.0f;

    private Rigidbody bulletRigidBody;
    private bool strongAttack;

    // Use this for initialization
    void Start()
    {

        bulletRigidBody = gameObject.GetComponent<Rigidbody>();
        Debug.Assert(bulletRigidBody != null);
        strongAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
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
