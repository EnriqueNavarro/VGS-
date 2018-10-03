using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour {

    [SerializeField] private Enemies enemy;
    [SerializeField] private EnemyHealth stats;
    [SerializeField] private GameObject[] abilities = new GameObject[5]; 
    [SerializeField] private bool stealth = false;
    [SerializeField] private bool combat;
    private bool first = true;
    [SerializeField] private int[] baseDmgs;  
    [SerializeField] private GameObject[] players;

    public EnemyHealth Stats
    {
        get
        {
            return stats;
        }

        set
        {
            stats = value;
        }
    }

    public GameObject[] Abilities
    {
        get
        {
            return abilities;
        }

        set
        {
            abilities = value;
        }
    }

    public bool Stealth
    {
        get
        {
            return stealth;
        }

        set
        {
            stealth = value;
        }
    }

    public bool Combat
    {
        get
        {
            return combat;
        }

        set
        {
            combat = value;
        }
    }

    public Enemies Enemy
    {
        get
        {
            return enemy;
        }

        set
        {
            enemy = value;
        }
    }
    public ThreatMeter[] threats;
    // Use this for initialization
    void Start()
    {
        //increaseDmg();
        first = false;
        enemy = stats.GetComponent<EnemyHealth>().TypeName;

    }
    private void Update()
    {
        if (players.Length == 0)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            ThreatMeter[] threats = new ThreatMeter[players.Length];
            for(int i=0;i<players.Length;i++) {
                threats[i] = new ThreatMeter(0,players[i],false);
            }
            stats.Threat = threats;
            GetComponentInChildren<LineOfSight>().Players = players;
        }
    }
    public void increaseDmg()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (first)
            {
                baseDmgs[i] = abilities[i].GetComponent<Ability>().Damage;
                abilities[i].GetComponent<Ability>().Damage = (int)(abilities[i].GetComponent<Ability>().Damage * stats.BaseDmg);

            }
            else
            {
                abilities[i].GetComponent<Ability>().Damage = (int)(baseDmgs[i] * stats.BaseDmg);
            }

        }
    }

}
