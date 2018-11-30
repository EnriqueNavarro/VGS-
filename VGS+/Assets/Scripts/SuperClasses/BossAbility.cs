using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAbility : EnemyAbility
{
    private Vector3 lastPos2;
    private Vector3 Movement2;
    [SerializeField] private bool busy;
    public bool activate;
    private ThreatMeter tuple;

    public Vector3 LastPos2
    {
        get
        {
            return lastPos2;
        }

        set
        {
            lastPos2 = value;
        }
    }

    public Vector3 Movement21
    {
        get
        {
            return Movement2;
        }

        set
        {
            Movement2 = value;
        }
    }

    public ThreatMeter Tuple
    {
        get
        {
            return tuple;
        }

        set
        {
            tuple = value;
        }
    }

   

    // Use this for initialization
    void Start () {
        if (melle)
        {
            Col.transform.localScale = new Vector3(Range / 10, 2, Range / 10);
            Col.transform.localPosition = new Vector3(Range / 20, 0, 0);
            LastPos2 = transform.position;
            
        }
        else
        {
            Col.transform.localScale = new Vector3(Range, 2, Range);
        }
    }
    new public void AdjustCol()
    {
        //if (this.name == "Flurry") Debug.Log(Movement);
        
        if (InProcess||Target==null) return;
        Movement21 = transform.position - LastPos2;
        float r = 1;
        float signZ = -Mathf.Sign(User.transform.position.z - Target.transform.position.z); 
        float signX = -Mathf.Sign(User.transform.position.x - Target.transform.position.x);
        float deltax = Mathf.Abs(User.transform.position.x - Target.transform.position.x);
        float deltay = Mathf.Abs(User.transform.position.z - Target.transform.position.z);
        //Debug.Log("SignX:" + signX + " SignY:" + signZ + " DeltaX" + deltax + " DeltaY" + deltay);
        if (deltax>deltay) {
            Col.transform.localPosition = new Vector3(r * signX * Range / 10, Col.transform.localPosition.y, 0);           
        } else {
            Col.transform.localPosition = new Vector3(0, Col.transform.localPosition.y, r * Range / 10 * signZ);
        }
    }
    // Update is called once per frame
    new void Update () {
        if (!IsInvoking()) InProcess = false;
        AdjustCol();
        int i = User.GetComponent<EnemyHealth>().Number;
        if (User.GetComponent<EnemyHealth>().combat)
        {
            Tuple = User.GetComponent<EnemyHealth>().Threat[i];
            Target = Tuple.player;
            if (melle)
            {
                if (!Vector3.Equals(transform.position, LastPos2))
                {
                    Movement21 = transform.position - LastPos2;
                    AdjustCol();
                }
                LastPos2 = transform.position;

            }
        }
        if(!InProcess && activate) {
            Trigger();
            activate = false;
            InProcess = true;
        }
    }
    new public void DealDamage()
    {
        this.GetComponentInParent<JailerMovement>().Current = MovementType.MoveToPlayer;
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Stats>().damage(Damage, DmgType);
        }
        Invoke("MovePlayer", 2);
        Aoe.SetActive(false);
    }
    public void MovePlayer() {
        this.GetComponentInParent<JailerMovement>().Current = MovementType.MoveToPlayer;
        InProcess = false;
    }
    new public void Trigger()
    {
        Timer = Time.fixedTime;
        Activate();
        InProcess = true;
        if (hasAnimation)
        {
            Invoke("ActivateAnimation", (Delay - animationDuration));
        }
        //Debug.Log("Activating " + this.name);        
    }
}
