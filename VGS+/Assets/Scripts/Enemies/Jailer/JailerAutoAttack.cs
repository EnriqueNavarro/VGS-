using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailerAutoAttack : BossAbility
{

     public override void Activate() {
        Invoke("DealDamage", Delay);
        Invoke("TurnWarningOn", (Delay - 1f));
        StartAnim();
        Aoe.SetActive(true);
    }
    private void StartAnim() {
        Anim.SetBool("auto", true);
        Invoke("EndAnim", 0.1f);
    }
    private void EndAnim() {
        Anim.SetBool("auto", false);
    }
     
}
