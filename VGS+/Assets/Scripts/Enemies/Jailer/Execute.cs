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
}
