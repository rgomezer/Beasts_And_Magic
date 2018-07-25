using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[NetworkSettings (channel = 1, sendInterval = 10f) ]
public class PlayerAttack : NetworkBehaviour {
	
    [SerializeField] private GameObject projectileSpawn;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectilePrefab2;
    [SerializeField] private bool hasProjectileAttack;
    [SerializeField] private bool mobileMode;

    private PlayerData pData;
    private Button attackButton1;
    private Button attackButton2;

    public SoundOnline sound;
    public GameObject soundThing;

    //data
    private bool attack1;
    private bool attack2;

    private PlayerAnim anim;

    // Use this for initialization
    void Start () {
        sound = GetComponentInChildren<SoundOnline>();
        anim = GetComponent<PlayerAnim>();
        Debug.Assert(anim != null);

        attack1 = false;
        attack2 = false;

        pData = GetComponent<PlayerData>();
        Debug.Assert(pData != null);
	}

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (projectilePrefab && projectilePrefab2 != null && hasProjectileAttack)
        {
            ClientScene.RegisterPrefab(projectilePrefab);
            ClientScene.RegisterPrefab(projectilePrefab2);
        }
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        if (mobileMode)
        {
            attackButton1 = GameObject.Find("/Canvas MainWorld/AttackButton1").GetComponent<Button>();
            Debug.Assert(attackButton1 != null);

            attackButton2 = GameObject.Find("/Canvas MainWorld/AttackButton2").GetComponent<Button>();
            Debug.Assert(attackButton2 != null);

            attackButton1.onClick.RemoveAllListeners();
            attackButton2.onClick.RemoveAllListeners();

            attackButton1.onClick.AddListener(() => Attack1());
            attackButton2.onClick.AddListener(() => Attack2());
        }
    }

    // Update is called once per frame
    void Update () {

        if (isLocalPlayer)
        {
            if (!mobileMode)
            {
                if (Input.GetMouseButtonDown(0) && !attack1 && !attack2 && pData.isAlive)
                {
                    Debug.Log("Attack 1 Start!");
                    attack1 = true;
                    StartCoroutine(DisableAttack1());
                    if (isServer)
                    {
                        RpcPlayLight();
                    }
                    else
                    {
                        CmdPlayLight();
                    }
                    sound.lightAttackSound();

                }

                if (Input.GetMouseButtonDown(1) && !attack1 && !attack2 && pData.isAlive)
                {
                    Debug.Log("Attack 2 Start");
                    attack2 = true;
                    StartCoroutine(DisableAttack2());
                    if (isServer)
                    {
                        RpcPlayHeavy();
                    }
                    else
                    {
                        CmdPlayHeavy();
                    }
                    sound.heavyAttackSound();
                }
            }
        }
        else
        {
            return;
        }
    }

    private IEnumerator DisableAttack1()
    {
        anim.attackOne(attack1);

        yield return new WaitForSeconds(pData.attack1CoolDown);

        if (hasProjectileAttack && attack1)
        {
            CmdSpawnProjectile(projectileSpawn.transform.position, false);
        }
        
        Debug.Log("Attack 1 Stop");
        this.attack1 = false;
    }

    private IEnumerator DisableAttack2()
    {
        anim.attackTwo(attack2);
        
        yield return new WaitForSeconds(pData.attack2CoolDown);

        if (hasProjectileAttack && attack2)
        {
            CmdSpawnProjectile2(projectileSpawn.transform.position, true);
        }
        
        this.attack2 = false;
        Debug.Log("Attack 2 Stop");
    }

    public bool getAttack1
    {
        get { return attack1; }
    }

    public bool getAttack2
    {
        get { return attack2; }
    }
    [ClientRpc]
    public void RpcPlayLight()
    {
        sound.lightAttackedSound();
    }
    [Command]
    public void CmdPlayLight()
    {
        sound.lightAttackedSound();

    }
    [ClientRpc]
    public void RpcPlayHeavy()
    {
        sound.heavyAttackedSound();
    }
    [Command]
    public void CmdPlayHeavy()
    {
        sound.heavyAttackedSound();

    }
    public void Attack1()
    {
        if (!attack1 && !attack2 && pData.isAlive)
        {
            Debug.Log("Attack 2 Start");
            attack1 = true;
            StartCoroutine(DisableAttack1());
            if (isServer)
            {
                RpcPlayLight();
            }
            else
            {
                CmdPlayLight();
            }

            sound.lightAttackSound();
        }
    }

    public void Attack2()
    {
        if (!attack1 && !attack2 && pData.isAlive)
        {
            Debug.Log("Attack 2 Start");
            attack2 = true;
            StartCoroutine(DisableAttack2());
            if (isServer)
            {
                RpcPlayHeavy();
            }
            else
            {
                CmdPlayHeavy();
            }
            sound.heavyAttackSound();
        }
    }

    //Prefabs needs to be explicitly stated to spawn or else it won't work F*ck UNET
    [Command]
    private void CmdSpawnProjectile(Vector3 pos, bool isStrongAttack)
    {
        Debug.Log("Spawn Projectile Fire");
        GameObject temp = Instantiate(projectilePrefab, pos, projectileSpawn.transform.rotation);
        temp.GetComponent<SpellFire>().setStrongAttack(isStrongAttack);
        NetworkServer.Spawn(temp);
    }

    [Command]
    private void CmdSpawnProjectile2(Vector3 pos, bool isStrongAttack)
    {
        Debug.Log("Spawn Projectile Magic");
        GameObject temp = Instantiate(projectilePrefab2 ,pos, projectileSpawn.transform.rotation);
        temp.GetComponent<SpellFire>().setStrongAttack(isStrongAttack);
        NetworkServer.Spawn(temp);
    }
}
