using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class : MonoBehaviour
{
    [SerializeField]
    private ClassNames cName;
    [SerializeField]
    private Stats stats;
    [SerializeField]
    private GameObject[] passives = new GameObject[2]; // each class has at least 1 passive
    [SerializeField]
    private GameObject[] actives = new GameObject[4]; //each class has at most 4 actives
    [SerializeField]
    private bool stealth = false;

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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}