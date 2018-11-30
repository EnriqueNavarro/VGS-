using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnsaringStrike : BossAbility
{
    [SerializeField] private GameObject mist;
    public override void Activate()
    {
        Invoke("DealDamage", Delay);
        Invoke("TurnWarningOn", (Delay - 1f));
        Aoe.SetActive(true);
    }


    new public void AdjustCol()
    {
        if (InProcess || Target == null) return;
        Movement21 = transform.position - LastPos2;
        float signZ = -Mathf.Sign(User.transform.position.z - Target.transform.position.z);
        float signX = -Mathf.Sign(User.transform.position.x - Target.transform.position.x);
        float deltax = Mathf.Abs(User.transform.position.x - Target.transform.position.x);
        float deltay = Mathf.Abs(User.transform.position.z - Target.transform.position.z);
        //Debug.Log("SignX:" + signX + " SignY:" + signZ + " DeltaX" + deltax + " DeltaY" + deltay);
        if (deltax > deltay)
        {
            if (signX > 0)
            {
                Col.transform.rotation = Quaternion.Euler(0, 0, 0);
            } else
            {
                Col.transform.rotation = Quaternion.Euler(0, -180 * signX, 0);
            }
            
        }
        else
        {
            Col.transform.rotation = Quaternion.Euler(0, -90 * signZ, 0);
        }

    }
    new void Update()
    {
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
        if (!InProcess && activate)
        {
            Trigger();
            activate = false;            
            mist.SetActive(true);
            Invoke("StartAnim", 1f);
        }
        
    }
    new public void DealDamage()
    {
        this.GetComponentInParent<JailerMovement>().Current = MovementType.MoveToPlayer;
        mist.SetActive(false);
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Stats>().damage(Damage, DmgType);
        }
        Invoke("MovePlayer", 2);
        Aoe.SetActive(false);
    }
    private void StartAnim()
    {
        Anim.SetBool("ensaring", true);
        Invoke("EndAnim", 0.1f);
    }
    private void EndAnim()
    {
        Anim.SetBool("ensaring", false);
    }

}
