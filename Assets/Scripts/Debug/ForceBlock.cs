using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBlock : MonoBehaviour {

    //data accessible to the Inspector
    [SerializeField] private GameObject blockingEffect;
    [SerializeField] private GameObject breakBlockingEffect;
    [SerializeField] private bool canBlock = false;

    //private PlayerAnimation anim;
    private bool canBreakBlock;

    // Use this for initialization
    void Start () {

        canBreakBlock = false;

        //anim = GetComponent<PlayerAnimation>();
        //Debug.Assert(anim != null);

        blockingEffect.SetActive(false);
        Debug.Assert(blockingEffect != null);

        breakBlockingEffect.SetActive(false);
        Debug.Assert(breakBlockingEffect != null);
    }
	
	// Update is called once per frame
	void Update () {

        if(canBlock)
        {
            blockingEffect.SetActive(true);
            //anim.blocking();
        }
        else
        {
            SpawnBreakBlockEffect();

            blockingEffect.SetActive(false);
            //anim.unBlocking();
        }

        if(canBreakBlock)
        {
            SpawnBreakBlockEffect();

            blockingEffect.SetActive(false);
            canBreakBlock = false;
        }

    }

    void SpawnBreakBlockEffect()
    {
        GameObject temp = Instantiate(breakBlockingEffect, blockingEffect.transform);
        Destroy(temp, 2.0f);
    }

    public void setBlock(bool value)
    {
        canBlock = value;
    }

    public void setBreakBlockEffect(bool value)
    {
        canBreakBlock = value;
    }

    public bool getBlock
    {
        get { return canBlock; }
    }
}
