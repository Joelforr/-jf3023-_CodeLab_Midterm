using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using SimpleJSON;


public class PlayerManager : MonoBehaviour {

    private InputState inputState;
    private Walk walkBehaviour;
    private Animator animator;
    private CollisionState collisionState;

    WebClient client = new WebClient();
    private string content;
    private Champion champion = new Champion();

    [SerializeField]
    private string id;

    public string champName;
    public int health;
    public int attackDamage;
    public int defense;
    public int movespeed;
    public int attackRange;
    public float attackspeedDelay;


    void Awake() {
        inputState = GetComponent<InputState>();
        walkBehaviour = GetComponent<Walk>();
        animator = GetComponent<Animator>();
        collisionState = GetComponent<CollisionState>();
    }

	// Use this for initialization
	void Start () {
        string downloadUrl = "https://global.api.riotgames.com/api/lol/static-data/NA/v1.2/champion/@id?champData=info,stats&api_key=RGAPI-464649D2-859B-4952-A3EE-7D33B45AF1B0";
        downloadUrl = downloadUrl.Replace("@id", id);
        ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
        content = client.DownloadString(downloadUrl);

        JsonUtility.FromJsonOverwrite(content, champion);
        champName = champion.name;
        health = (int)champion.stats.hp;
        attackDamage = (int)champion.stats.attackdamage;
        defense = (int)champion.stats.armor;
        movespeed = (int)champion.stats.movespeed / 10;
        attackRange = (int)champion.stats.attackrange;
        attackspeedDelay = 1f/champion.stats.attackspeedperlevel;
    }
	
	// Update is called once per frame
	void Update () {
        if(health <= 0) {
            OnDestroy();
        }

        if (collisionState.standing) {
            ChangeAnimationState(0);
        }

        if(inputState.absVelX > 0) {
            ChangeAnimationState(1);
        }

        if (inputState.absVelY > 0) {
            ChangeAnimationState(2);
        }
    }

    void ChangeAnimationState(int value) {
        animator.SetInteger("AnimState", value);
    }

    void OnDestroy() {
        Destroy(gameObject);
    }

    public bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        bool isOk = true;
        // If there are errors in the certificate chain, look at each error to determine the cause.
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            for (int i = 0; i < chain.ChainStatus.Length; i++)
            {
                if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
                {
                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                    chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                    bool chainIsValid = chain.Build((X509Certificate2)certificate);
                    if (!chainIsValid)
                    {
                        isOk = false;
                    }
                }
            }
        }
        return isOk;
    }


}
