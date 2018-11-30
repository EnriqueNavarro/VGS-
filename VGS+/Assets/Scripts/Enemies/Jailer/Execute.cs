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
        Invoke("EndAnim2", 0.1f);
    }
    private void StartAnim()
    {
        Anim.SetBool("execute", true);
        Invoke("EndAnim", 0.1f);
    }
    private void EndAnim()
    {
        Anim.SetBool("execute", false);
    }
    private void EndAnim2()
    {
        Anim.SetBool("strike", false);
    }
}
