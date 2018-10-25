using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAA : EnemyAbility {
    public override void Activate() {
        Invoke("DealDamage",Duration);
        AttackWarning1.SetActive(true);
        Invoke("TurnWarningOff",Duration);
    }


}
