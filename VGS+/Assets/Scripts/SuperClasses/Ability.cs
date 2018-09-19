﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField]
    private string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private bool requireTarget;
    [SerializeField]
    private bool selfTarget;
    [SerializeField]
    private float cd; //secs
    [SerializeField]
    private float timer;
    [SerializeField]
    private GameObject particleflefx;
    [SerializeField]
    private int duration;
    [SerializeField]
    private float range;
    [SerializeField]
    private Elements dmgType;
    [SerializeField]
    private int damage;
    [SerializeField]
    private bool targetEnemies;
    [SerializeField]
    private bool targetAllies;
    [SerializeField]
    private bool targetAll;
    public List<GameObject> enemies;
    public List<GameObject> allies;
    public string keyBinding; // this must be rewritten

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    public Sprite Icon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public bool RequireTarget
    {
        get
        {
            return requireTarget;
        }

        set
        {
            requireTarget = value;
        }
    }

    public bool SelfTarget
    {
        get
        {
            return selfTarget;
        }

        set
        {
            selfTarget = value;
        }
    }

    public float Cd
    {
        get
        {
            return cd;
        }

        set
        {
            cd = value;
        }
    }

    public float Timer
    {
        get
        {
            return timer;
        }

        set
        {
            timer = value;
        }
    }

    public GameObject Particleflefx
    {
        get
        {
            return particleflefx;
        }

        set
        {
            particleflefx = value;
        }
    }

    public int Duration
    {
        get
        {
            return duration;
        }

        set
        {
            duration = value;
        }
    }

    public float Range
    {
        get
        {
            return range;
        }

        set
        {
            range = value;
        }
    }

    public Elements DmgType
    {
        get
        {
            return dmgType;
        }

        set
        {
            dmgType = value;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public bool TargetEnemies
    {
        get
        {
            return targetEnemies;
        }

        set
        {
            targetEnemies = value;
        }
    }

    public bool TargetAllies
    {
        get
        {
            return targetAllies;
        }

        set
        {
            targetAllies = value;
        }
    }

    public bool TargetAll
    {
        get
        {
            return targetAll;
        }

        set
        {
            targetAll = value;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(keyBinding)) Trigger();
    }
    //if (Input.GetKeyDown(keyBinding)) Trigger(); agregar esa linea en cada update
    public void Trigger()
    {
        if ((Time.fixedTime - Timer) >= Cd)
        {
            Timer = Time.fixedTime;
            Activate();
        }
    }

    abstract public void Activate();
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag + " added");
        if (TargetAll)
        {
            if (other.tag == "Player") addAlly(other);
            if (other.tag == "Enemy") addEnemy(other);
        }
        else
        {
            if (TargetEnemies)
            {
                if (other.tag == "Enemy") addEnemy(other);
            }
            if (TargetAllies)
            {
                if (other.tag == "Player") addAlly(other);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.tag + " removed");
        if (TargetAll)
        {
            if (other.tag == "Player") removeAlly(other);
            if (other.tag == "Enemy") removeEnemy(other);
        }
        else
        {
            if (TargetEnemies)
            {
                if (other.tag == "Enemy") removeEnemy(other);
            }
            if (TargetAllies)
            {
                if (other.tag == "Player") removeAlly(other);
            }
        }
    }
    private void removeEnemy(Collider other)
    {
        enemies.Remove(other.gameObject);
    }
    private void removeAlly(Collider other)
    {
        enemies.Remove(other.gameObject);
    }
    private void addEnemy(Collider other)
    {
        enemies.Add(other.gameObject);
    }
    private void addAlly(Collider other)
    {
        allies.Add(other.gameObject);
    }
}