using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailerAutoAttack : BossAbility
{

     public override void Activate() {
        Invoke("DealDamage", Delay);
        Invoke("TurnWarningOn", (Delay - 1f));
    }

     
}
