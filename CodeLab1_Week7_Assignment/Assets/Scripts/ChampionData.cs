using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionData : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}


[System.Serializable]
public class Champion
{
    public int id;
    public string key;
    public string name;
    public string title;
    public Info info;
    public Stats stats;
}

[System.Serializable]
public class Info
{
    public int attack;
    public int defense;
    public int magic;
    public int difficulty;
}

[System.Serializable]
public class Stats
{
    public float armor;
    public float armorperlevel;
    public float attackdamage;
    public float attackdamageperlevel;
    public float attackrange;
    public float attackspeedoffset;
    public float attackspeedperlevel;
    public float crit;
    public float critperlevel;
    public float hp;
    public float hpperlevel;
    public float hpregen;
    public float hpregenperlevel;
    public float movespeed;
    public float mp;
    public float mpperlevel;
    public float mpregen;
    public float mpregenperlevel;
    public float spellblock;
    public float spellblockperlevel;
}
