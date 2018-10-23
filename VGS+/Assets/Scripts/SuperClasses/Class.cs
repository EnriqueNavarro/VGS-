using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Class : MonoBehaviour
{
    [SerializeField] private ClassNames cName;
    [SerializeField] private Stats stats;
    [SerializeField] private GameObject[] passives = new GameObject[2]; // each class has at least 1 passive
    [SerializeField] private GameObject[] actives = new GameObject[5]; //each class has at most 4 actives
    [SerializeField] private bool stealth = false;
    [SerializeField] private bool combat;
    [SerializeField] private GameObject[] keyboard = new GameObject[5];
    private bool first = true;
    [SerializeField] private int[] baseDmgs = new int[5];

    public ClassNames CName
    {
        get
        {
            return cName;
        }

        set
        {
            cName = value;
        }
    }

    public Stats Stats
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

    public GameObject[] Passives
    {
        get
        {
            return passives;
        }

        set
        {
            passives = value;
        }
    }

    public GameObject[] Actives
    {
        get
        {
            return actives;
        }

        set
        {
            actives = value;
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

    // Use this for initialization
    void Start()
    {
        increaseDmg();
        first = false;

        for (int i = 0; i < actives.Length; i++) {
            GameObject abi = actives[i];

            Image img = keyboard[i].GetComponent<Image>();
            img.sprite = abi.GetComponent<Ability>().Icon;
        }
    }
    public void increaseDmg()
    {
        for (int i = 0; i < actives.Length; i++)
        {
            if (first)
            {
                baseDmgs[i] = actives[i].GetComponent<Ability>().Damage;
                actives[i].GetComponent<Ability>().Damage = (int)(actives[i].GetComponent<Ability>().Damage * stats.BaseDmg);
                
            } else
            {
                actives[i].GetComponent<Ability>().Damage = (int)(baseDmgs[i] * stats.BaseDmg);
            }
            
        }
    }

   
}