using System.Collections;
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
    private float duration;
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
    [SerializeField]
    private Collider col;
    public float elapsed;
    public List<GameObject> enemies;
    public List<GameObject> allies;
    public string keyBinding; // this must be rewritten
    private float cdModifier;
    float rangeModifier;
    private float modifier;
    private sStats change;
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

    // Use this for initialization
    void Start()
    {
        Col.transform.localScale = new Vector3(Range, 2, Range);
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(keyBinding)) Trigger();
        elapsed = Time.fixedTime - Timer;
    }
    //if (Input.GetKeyDown(keyBinding)) Trigger(); agregar esa linea en cada update
    public void Trigger()
    {
        if ((Time.fixedTime - Timer) >= Cd)
        {
            Timer = Time.fixedTime;
            Activate();
            Debug.Log("Activating " + this.name);
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
    public void changer(float _modifier,float time, sStats toChange) {
        change = toChange;
        modifier = _modifier;
        switch(toChange) {
            case sStats.CD:
                Cd *= modifier;
                break;
            case sStats.Damage:
                Damage = (int)(Damage*modifier);
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
    private void reverter() {
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
    /*public void changeCD(float _cdModifier,float time) {
        cdModifier = _cdModifier;
        Cd *= cdModifier;
        Invoke("expireCD", time);
    }
    private void expireCD() {
        Cd /= cdModifier;
    }
    public void changeRange(float _modifier, float time)
    {
        rangeModifier = _modifier;
        Range *= rangeModifier;
        Invoke("expireR", time);
    }
    private void expireR()
    {
        Range /= rangeModifier;
    }*/
}