using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] new private string name;
    [SerializeField] private string description;
    [SerializeField] public Sprite icon;
    [SerializeField] private float cd; //secs
    private float timer;
    public float remainingCD;
    [SerializeField] private GameObject particleflefx;
    [SerializeField] private bool hasAnimation;
    [SerializeField] private float duration;
    [SerializeField] private float range;
    [SerializeField] private Elements dmgType;
    [SerializeField] private int damage;
    [SerializeField] private bool targetEnemies;
    [SerializeField] private bool targetAllies;
    [SerializeField] private Collider col;
    [SerializeField] private GameObject Player;
    [SerializeField] private Animator animator;
    float animationDuration=0.1f;
    private bool f;
    private Vector3 movement;
    private string animationName;
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

    public GameObject Player1
    {
        get
        {
            return Player;
        }

        set
        {
            Player = value;
        }
    }

    public bool F
    {
        get
        {
            return f;
        }

        set
        {
            f = value;
        }
    }

    public Vector3 Movement
    {
        get
        {
            return Movement1;
        }

        set
        {
            Movement1 = value;
        }
    }

    public Vector3 Movement1
    {
        get
        {
            return movement;
        }

        set
        {
            movement = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        Col.transform.localScale = new Vector3(Range, 2, Range);
        AdjustCol();

        
    }
    public void AdjustCol() {
        //if (this.name == "Flurry") Debug.Log(Movement);
        float r = 3;
        float signZ;
        float signX;  
        if(Movement1.z>0) {
            signZ = 1;
        } else {
            signZ = -1;
        }
        if (Movement1.x > 0)
        {
            signX = -1;
        }
        else
        {
            signX = -1;
        }

        Col.transform.localScale = new Vector3(Range, 2, Range);
        
            if(Movement1.x!=0 &&Movement1.z!=0) {
                Col.transform.localPosition = new Vector3(r * signX * Range, Col.transform.localPosition.y, r * Range* signZ);
            } else {
                if(Movement1.x!=0) {
                    //Debug.Log(Movement);
                    Col.transform.localPosition = new Vector3(r * signX * Range, Col.transform.localPosition.y,0);
                } else {
                    if(Movement1.z!=0) Col.transform.localPosition = new Vector3(0, Col.transform.localPosition.y, r * Range* signZ);
                }
            }
        
        
    }
    private void FixedUpdate()
    {
        AdjustCol();
    }
    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(keyBinding))
        {
            Trigger();
            F = true;
        }
        if (F) { 
            elapsed = Time.fixedTime - Timer; 
        } else {
            elapsed = Cd;
        }
        remainingCD = Mathf.Clamp((Cd-elapsed), 0, Cd);
    }
    //if (Input.GetKeyDown(keyBinding)) Trigger(); agregar esa linea en cada update
    public void Trigger()
    {
        if ((Time.fixedTime - Timer) >= Cd || !F)
        {
            if(hasAnimation) {
                animator.SetBool(Name, true);
                Invoke("EndAnimation", animationDuration);
            }
            Timer = Time.fixedTime;
            Activate();
            Debug.Log("Activating " + this.name);
        }
    }
    void EndAnimation() {
        animator.SetBool(Name, false);
    }
    abstract public void Activate();
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag + " added");
        
        
        
            if (TargetEnemies)
            {
                if (other.tag == "Enemy") addEnemy(other);
            }
            if (TargetAllies)
            {
                if (other.tag == "Player") addAlly(other);
            }
        
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.tag + " removed");
        
            if (TargetEnemies)
            {
                if (other.tag == "Enemy") removeEnemy(other);
            }
            if (TargetAllies)
            {
                if (other.tag == "Player") removeAlly(other);
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
                AdjustCol();
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
                AdjustCol();
                break;
        }
    }
}