﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbility : MonoBehaviour {
    [SerializeField] new private string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private bool requireTarget;
    [SerializeField] private bool selfTarget;
    [SerializeField] private float cd; //secs
    [SerializeField] private float timer;
    [SerializeField] private GameObject particleflefx;
    [SerializeField] private float duration;
    [SerializeField] private float range;
    [SerializeField] private Elements dmgType;
    [SerializeField] private int damage;
    [SerializeField] private Collider col;
    [SerializeField] private GameObject user;
    [SerializeField] private bool LOS;
    [SerializeField] private bool inRange;
    public GameObject tokens;
    private TokenManager tokenManager;
    [SerializeField] private bool requestSent=false;
    [SerializeField] private bool approved;
    [SerializeField] private GameObject target;
    [SerializeField] private Request request;
    private ThreatMeter tuple;
    public float elapsed;
    public List<GameObject> enemies;
    public List<GameObject> allies;
    public string keyBinding; // this must be rewritten
    private float cdModifier;
    float rangeModifier;
    private float modifier;
    private sStats change;
    private int cost;
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

    public float Duration
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
    
    public Collider Col
    {
        get
        {
            return col;
        }

        set
        {
            col = value;
        }
    }

    public GameObject User
    {
        get
        {
            return user;
        }

        set
        {
            user = value;
        }
    }

    public bool LOS1
    {
        get
        {
            return LOS;
        }

        set
        {
            LOS = value;
        }
    }

    public bool InRange
    {
        get
        {
            return inRange;
        }

        set
        {
            inRange = value;
        }
    }

    public Request Request
    {
        get
        {
            return request;
        }

        set
        {
            request = value;
        }
    }

    public bool Approved
    {
        get
        {
            return approved;
        }

        set
        {
            approved = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        Col.transform.localScale = new Vector3(Range, 2, Range);
        tokenManager = tokens.GetComponent<TokenManager>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (user.GetComponent<EnemyHealth>().combat)
        {
            float distance = Vector3.Distance(user.transform.position, user.GetComponent<EnemyHealth>().Attacker.transform.position);
            int i = user.GetComponent<EnemyHealth>().Number;
            tuple = user.GetComponent<EnemyHealth>().Threat[i];
            LOS1 = user.GetComponent<LineOfSight>().LOS1[i];
            if ((Time.fixedTime - Timer) >= Cd && distance <= Range && LOS1)
            {
                if (!requestSent)
                {                    
                    target = tuple.player;
                    int targetHp = target.GetComponent<Stats>().Health;
                    inRange = Vector3.Distance(user.transform.position, target.transform.position) < Range;
                    Request = new Request(user, InRange, LOS1, Damage, distance, tuple.threat, targetHp, Range);
                    tokenManager.AddRequest(Request);
                    cost = request.cost;
                    requestSent = true;
                }
                else
                {
                    if (Approved)
                    {
                        requestSent = false;
                        approved = false;
                        Trigger();
                        Invoke("Return", Duration);
                        request = new Request();
                    }
                }
            }
        }
        elapsed = Time.fixedTime - Timer;
    }
    private void Return() {
        tokenManager.Refund(cost);
    }
    //if (Input.GetKeyDown(keyBinding)) Trigger(); agregar esa linea en cada update
    public void Trigger()
    {        
        Timer = Time.fixedTime;
        Activate();
        Debug.Log("Activating " + this.name);        
    }

    public void Activate() {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Stats>().damage(Damage, DmgType);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "Player") addEnemy(other);  
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") removeEnemy(other);   
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
    public void changer(float _modifier, float time, sStats toChange)
    {
        change = toChange;
        modifier = _modifier;
        switch (toChange)
        {
            case sStats.CD:
                Cd *= modifier;
                break;
            case sStats.Damage:
                Damage = (int)(Damage * modifier);
                break;
            case sStats.Duration:
                Duration *= modifier;
                break;
            case sStats.Range:
                Range *= modifier;
                break;
        }
        Invoke("reverter", time);
    }
    private void reverter()
    {
        switch (change)
        {
            case sStats.CD:
                Cd /= modifier;
                break;
            case sStats.Damage:
                Damage = (int)(Damage / modifier);
                break;
            case sStats.Duration:
                Duration /= modifier;
                break;
            case sStats.Range:
                Range /= modifier;
                break;
        }
    }
}
