using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class PlayerBlock : NetworkBehaviour
{

    [SerializeField] private GameObject blockingEffect;
    [SerializeField] private GameObject breakBlockingEffect;
    [SerializeField] private GameObject breakBlockEffectTrans;
    [SerializeField] private bool mobileMode;

    private bool canBlock;
    private bool canSpawnBreakingBlockEffect;

    private Button blockButton;

    private PlayerData pData;
    private PlayerAttack pAttack;
    private PlayerAnim anim;

    public override void OnStartClient()
    {
        base.OnStartClient();

        ClientScene.RegisterPrefab(breakBlockingEffect);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
    }

    // Use this for initialization
    void Start ()
    {
        canBlock = false;
        canSpawnBreakingBlockEffect = false;

        pData = GetComponent<PlayerData>();
        Debug.Assert(pData != null);

        blockingEffect.SetActive(false);
        Debug.Assert(blockingEffect != null);

        breakBlockingEffect.SetActive(false);
        Debug.Assert(breakBlockingEffect != null);

        anim = GetComponent<PlayerAnim>();
        Debug.Assert(anim != null);

        pAttack = GetComponent<PlayerAttack>();
        Debug.Assert(pAttack != null);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        if (!mobileMode)
        {
            if (Input.GetKey(KeyCode.Space) && !pAttack.getAttack1 && !pAttack.getAttack2 && pData.isAlive)
            {
                anim.blocking();

                LocalBlock();

                if (isServer)
                {
                    RpcBlock();             
                }
                else
                {
                    CmdBlock();
                }
            }
            
            if (Input.GetKeyUp(KeyCode.Space) && !pAttack.getAttack1 && !pAttack.getAttack2 && pData.isAlive)
            {
                canSpawnBreakingBlockEffect = true;

                anim.unBlocking();

                LocalStopBlock();

                if (isServer)
                {
                    RpcStopBlock(); 
                }
                else
                {
                    CmdStopBlock();
                }
            }

            if (canSpawnBreakingBlockEffect)
            {
                CmdSpawnBreakBlockEffect();
                canSpawnBreakingBlockEffect = false;
                //anim.blockBreak(); //will play the block Break animation
            }
        }
    }

    public bool getBlock
    {
        get { return canBlock; }
    }

    public void Block()
    {
        if (!pAttack.getAttack1 && !pAttack.getAttack2 && pData.isAlive)
        {
            if(!isLocalPlayer)
            {
                return;
            }

            anim.blocking();

            LocalBlock();

            if (isServer)
            {
                RpcBlock();
            }
            else
            {
                CmdBlock();
            }
        }
    }

    public void StopBlock()
    {
        if (!pAttack.getAttack1 && !pAttack.getAttack2 && pData.isAlive)
        {
            if (!isLocalPlayer)
            {
                return;
            }

            canSpawnBreakingBlockEffect = true;

            anim.unBlocking();

            LocalStopBlock();

            if (isServer)
            {
                RpcStopBlock();
            }
            else
            {
                CmdStopBlock();
            }
        }

        if (canSpawnBreakingBlockEffect)
        {
            CmdSpawnBreakBlockEffect();
            canSpawnBreakingBlockEffect = false;
        }
    }

    void LocalBlock()
    {
        canBlock = true;
        pData.isBlocking = true;
        blockingEffect.SetActive(true);
        Debug.Log("Blocking Attack");
    }

    void LocalStopBlock()
    {
        canBlock = false;
        pData.isBlocking = false;
        blockingEffect.SetActive(false);
        Debug.Log("Stopping Blocking Attack");
    }

    [Command]
    void CmdSpawnBreakBlockEffect()
    {
        GameObject temp = Instantiate(breakBlockingEffect, breakBlockEffectTrans.transform);
        Destroy(temp, 2.1f);
        NetworkServer.Spawn(temp);
    }

    [Command]
    public void CmdBlock()
    {
        LocalBlock();
        RpcBlock();
    }

    [ClientRpc]
    public void RpcBlock()
    {
        if (isLocalPlayer)
        {
            return;
        }

        LocalBlock();
    }

    [Command]
    public void CmdStopBlock()
    {
        LocalStopBlock();
        RpcStopBlock();
    }

    [ClientRpc]
    public void RpcStopBlock()
    {
        if (isLocalPlayer)
        {
            return;
        }

        LocalStopBlock();
    }
}