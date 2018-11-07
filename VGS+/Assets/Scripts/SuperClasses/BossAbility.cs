using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAbility : EnemyAbility
{
    private Vector3 lastPos2;
    private Vector3 Movement2;
    public bool inProcess;
    public bool activate;
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
        if (inProcess) return;
        Movement2 = transform.position - lastPos2;
        float r = 1;
        float signZ;
        float signX;
        if (Movement2.z > 0)
        {
            signZ = 1;
        }
        else
        {
            signZ = -1;
        }
        if (Movement2.x > 0)
        {
            signX = 1;
        }
        else
        {
            signX = -1;
        }

        //Col.transform.localScale = new Vector3(Range, 2, Range);

        if (Movement2.x != 0 && Movement2.z != 0)
        {
            Col.transform.localPosition = new Vector3(r * signX * Range / 10, Col.transform.localPosition.y, r * Range / 10 * signZ);
        }
        else
        {
            if (Movement2.x != 0)
            {
                //Debug.Log(Movement);
                Col.transform.localPosition = new Vector3(r * signX * Range / 10, Col.transform.localPosition.y, 0);
            }
            else
            {
                if (Movement2.z != 0) Col.transform.localPosition = new Vector3(0, Col.transform.localPosition.y, r * Range / 10 * signZ);
            }
        }


    }
    // Update is called once per frame
    new void Update () {
        if (melle)
        {
            if (!Vector3.Equals(transform.position, lastPos2))
            {
                Movement2 = transform.position - lastPos2;
                AdjustCol();
            }
            lastPos2 = transform.position;

        }
        if(!inProcess && activate) {
            Trigger();
            activate = false;
        }
    }
    new public void DealDamage()
    {
        inProcess = false;
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Stats>().damage(Damage, DmgType);
        }
    }
    new public void Trigger()
    {
        Timer = Time.fixedTime;
        Activate();
        inProcess = true;
        if (hasAnimation)
        {
            Invoke("ActivateAnimation", (Delay - animationDuration));
        }
        //Debug.Log("Activating " + this.name);        
    }
}
