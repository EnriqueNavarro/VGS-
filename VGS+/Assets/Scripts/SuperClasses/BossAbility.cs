using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAbility : EnemyAbility
{
    private Vector3 lastPos2;
    private Vector3 Movement2;
    
    public bool activate;
    private ThreatMeter tuple;
    // Use this for initialization
    void Start () {
        if (melle)
        {
            Col.transform.localScale = new Vector3(Range / 10, 2, Range / 10);
            Col.transform.localPosition = new Vector3(Range / 20, 0, 0);
            lastPos2 = transform.position;
            InvokeRepeating("AdjustCol", 0, ColAdjustFreq);
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
        Movement2 = transform.position - lastPos2;
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
        int i = User.GetComponent<EnemyHealth>().Number;
        if (i > 0)
        {
            tuple = User.GetComponent<EnemyHealth>().Threat[i];
            Target = tuple.player;
            if (melle)
            {
                if (!Vector3.Equals(transform.position, lastPos2))
                {
                    Movement2 = transform.position - lastPos2;
                    AdjustCol();
                }
                lastPos2 = transform.position;

            }
        }
        if(!InProcess && activate) {
            Trigger();
            activate = false;
            this.GetComponentInParent<JailerMovement>().Current = MovementType.Halt;
        }
    }
    new public void DealDamage()
    {
        InProcess = false;
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Stats>().damage(Damage, DmgType);
        }
        this.GetComponentInParent<JailerMovement>().Current = MovementType.MoveToPlayer;
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
