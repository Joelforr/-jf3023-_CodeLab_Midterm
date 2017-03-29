using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour {

    public Text p1Name;
    public Text p1Health;
    public Text p2Name;
    public Text p2Health;

    public GameObject player1;
    public GameObject player2;

    public static BattleUIManager instance;

	// Use this for initialization
	void Start () {


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        
	}
	
	// Update is called once per frame
	void Update () {
        p1Name.text = "Player1: " + player1.GetComponent<PlayerManager>().champName;
        p2Name.text = "Player2: " + player2.GetComponent<PlayerManager>().champName;

        p1Health.text = player1.GetComponent<PlayerManager>().health.ToString();
        p2Health.text = player2.GetComponent<PlayerManager>().health.ToString();
    }
}
