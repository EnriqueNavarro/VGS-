using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAA : EnemyAbility {
    public override void Activate() {
        Invoke("DealDamage",Delay);
        Invoke("TurnWarningOn", (Delay - 0.5f));
        
    }


}
