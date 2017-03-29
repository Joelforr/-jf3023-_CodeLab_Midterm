using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using SimpleJSON;

public class UIManager : MonoBehaviour {

    public InputField summonerInput;
    public Text inputText;
    public Text summonerName;
    public Button loadSummoner;
    public Text errorMessage;
    public string summonerInfo;


    public static UIManager instance;

    private WebClient client = new WebClient();


    
	// Use this for initialization
	void Start () {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }

	}
	
	// Update is called once per frame
	void Update () {
 
	}

    public void SetLocation() {
    }

    public void AttemptLogin() {
        string downloadUrl = "https://na.api.pvp.net/api/lol/na/v1.4/summoner/by-name/@summoner?api_key=RGAPI-464649D2-859B-4952-A3EE-7D33B45AF1B0";
        string summonerText = inputText.text;
        downloadUrl = downloadUrl.Replace("@summmoner",summonerText);
        ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
        summonerInfo = client.DownloadString("https://na.api.pvp.net/api/lol/na/v1.4/summoner/by-name/Xzero%20Reborn?api_key=RGAPI-464649D2-859B-4952-A3EE-7D33B45AF1B0");
        Debug.Log(summonerInfo);
        
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
