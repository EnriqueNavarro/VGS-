using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbility : MonoBehaviour {
    [SerializeField] new private string name;
    [SerializeField] private GameObject AttackWarning;
    public bool hasAnimation;
    public float animationDuration;
    [SerializeField] private Animator animator;
    [SerializeField] private string description;
    [SerializeField] private float cd; //secs
    [SerializeField] private float timer;
    [SerializeField] private GameObject particleflefx;
    [SerializeField] private float delay;
    [SerializeField] private float range;
    [SerializeField] private Elements dmgType;
    [SerializeField] private int damage;
    [SerializeField] private Collider col;
    [SerializeField] private GameObject user;
    [SerializeField] private bool LOS;
    [SerializeField] private bool inRange;
    private Vector3 lastPos;
    public bool melle;
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
    private float cdModifier;
    float rangeModifier;
    private float modifier;
    private sStats change;
    private int cost;
    private Vector3 Movement;
    private bool inProcess;
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

    public float Delay
    {
        get
        {
            return delay;
        }

        set
        {
            delay = value;
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

    public GameObject Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    public GameObject AttackWarning1
    {
        get
        {
            return AttackWarning;
        }

        set
        {
            AttackWarning = value;
        }
    }

    

    public bool InProcess
    {
        get
        {
            return inProcess;
        }

        set
        {
            inProcess = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        tokens = GameObject.FindGameObjectWithTag("TokenManager");
        if (melle)
        {
            Col.transform.localScale = new Vector3(Range / 10, 2, Range / 10);
            Col.transform.localPosition = new Vector3(Range / 20, 0, 0);
            lastPos = transform.position;
           
        }
        else
        {
            Col.transform.localScale = new Vector3(Range, 2, Range);
        }
        tokenManager = tokens.GetComponent<TokenManager>();
        AttackWarning1.SetActive(false);
        request = new Request();
        
    }
    public void AdjustCol()
    {
        //if (this.name == "Flurry") Debug.Log(Movement);
        if (InProcess || Target == null) return;
        Movement = transform.position - lastPos;
        float r = 1;
        float signZ = -Mathf.Sign(User.transform.position.z - Target.transform.position.z);
        float signX = -Mathf.Sign(User.transform.position.x - Target.transform.position.x);
        float deltax = Mathf.Abs(User.transform.position.x - Target.transform.position.x);
        float deltay = Mathf.Abs(User.transform.position.z - Target.transform.position.z);

        if (deltax > deltay)
        {
            Col.transform.localPosition = new Vector3(r * signX * Range / 10, Col.transform.localPosition.y, 0);
        }
        else
        {
            Col.transform.localPosition = new Vector3(0, Col.transform.localPosition.y, r * Range / 10 * signZ);
        }
    }
    // Update is called once per frame
    public void Update()
    {
        //Debug.Log(Movement);
        
        
        if (user.GetComponent<EnemyHealth>().combat && !requestSent && (Time.fixedTime - Timer) >= Cd )
        {
            float distance = Vector3.Distance(user.transform.position, user.GetComponent<EnemyHealth>().Attacker.transform.position);
            int i = user.GetComponent<EnemyHealth>().Number;
            tuple = user.GetComponent<EnemyHealth>().Threat[i];
            LOS1 = user.GetComponent<LineOfSight>().LOS1[i];
            //Debug.Log(distance);
            if ( distance <= Range*10 && LOS1)
            {
                Target = tuple.player;
                int targetHp = Target.GetComponent<Stats>().Health;
                inRange = Vector3.Distance(user.transform.position, Target.transform.position) < Range;
                //Request = Request(user, InRange, LOS1, Damage, distance, tuple.threat, targetHp, Range);
                request.requester = user;
                request.inRange = inRange;
                request.LOS = LOS1;
                request.toHit = damage;
                request.distance = distance;
                request.threat = tuple.threat;
                request.targetHP = targetHp;
                request.range = Range;
                tokenManager.AddRequest(Request);
                //Debug.Log("Sending request");
                cost = request.cost;
                requestSent = true;
                
            }
        }
        if (Approved)
        {
            requestSent = false;
            approved = false;
            Trigger();
            Invoke("Return", Cd);
            request.Clear();
            InProcess = true;
        }
        elapsed = Time.fixedTime - Timer;
        AdjustCol();
    }
    private void Return() {
        tokenManager.Refund(cost);
    }
    //if (Input.GetKeyDown(keyBinding)) Trigger(); agregar esa linea en cada update
    public void Trigger()
    {        
        Timer = Time.fixedTime;
        Activate();
        if (hasAnimation)
        {
            Invoke("ActivateAnimation", (Delay - animationDuration));
        }
        //Debug.Log("Activating " + this.name);        
    }
    public void ActivateAnimation()
    {
        animator.SetBool("attack", true);
        Invoke("EndAnimation", 0.2f);
    }
    public void EndAnimation()
    {
        animator.SetBool("attack", false);
    }
    abstract public void Activate();
    public void TurnWarningOff()
    {
        AttackWarning1.SetActive(false);
    }
    public void TurnWarningOn()
    {
        AttackWarning1.SetActive(true);
        Invoke("TurnWarningOff", 1f);
    }
    public void DealDamage() {

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Stats>().damage(Damage, DmgType);
        }
        InProcess = false;
    }
    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "Player")
        {
            addEnemy(other);
        } 
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            removeEnemy(other);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Class>().Combat = true;
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
                Delay *= modifier;
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
                Delay /= modifier;
                break;
            case sStats.Range:
                Range /= modifier;
                break;
        }
    }
}
