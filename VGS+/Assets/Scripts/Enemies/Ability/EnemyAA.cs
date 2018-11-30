using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAA : EnemyAbility {
    public override void Activate() {
        Invoke("DealDamage",Delay);
        Invoke("TurnWarningOn", (Delay - 1f));
        Invoke("StartAnim", Delay - 0.5f);
    }
    private void StartAnim()
    {
        Anim.SetBool("auto", true);
        Invoke("EndAnim", 0.1f);
    }
    private void EndAnim()
    {
        Anim.SetBool("auto", false);
    }


}
