using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Execute : BossAbility
{
    [SerializeField] private int startingHealth;
    public int currentHealth;
    [SerializeField] int dmgTaken;
    [SerializeField] private int shield;
    public override void Activate()
    {
        Invoke("DealDamage", Delay);
        this.GetComponentInParent<JailerMovement>().Current = MovementType.MoveToPlayer;
        StartAnim();
        this.GetComponentInParent<EnemyHealth>().Health += shield;
    }
    
    new void Update()
    {
        if (!InProcess && activate)
        {
            Trigger();
            activate = false;
            startingHealth = this.GetComponentInParent<EnemyHealth>().Health;
            this.GetComponentInParent<JailerMovement>().Current = MovementType.Execute;
        }
        if(InProcess) 
        {
            currentHealth = this.GetComponentInParent<EnemyHealth>().Health;
            dmgTaken = startingHealth - currentHealth;
            if(dmgTaken>=shield) {
                CancelInvoke();
                InProcess = false;                
                this.GetComponentInParent<JailerMovement>().Current = MovementType.MoveToPlayer;
                Debug.Log(this.GetComponentInParent<EnemyHealth>().Health);
                this.GetComponentInParent<EnemyHealth>().Health += shield;
                Debug.Log(this.GetComponentInParent<EnemyHealth>().Health);
                Anim.SetBool("execute", false);
            }

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
        Anim.SetBool("strike", true);
        Anim.SetBool("execute", false);
        Invoke("EndAnim2", 0.1f);
    }
    private void StartAnim()
    {
        Anim.SetBool("execute", true);
    }
    private void EndAnim2()
    {
        Anim.SetBool("strike", false);
    }
}
