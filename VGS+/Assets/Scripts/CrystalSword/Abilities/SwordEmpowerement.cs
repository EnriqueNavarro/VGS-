using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEmpowerement : Ability {
    [SerializeField] private GameObject autoAttack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private GameObject resource;
    [SerializeField] private float cost;
    new public void Update()
    {
        if (resource.GetComponent<CrystalSword>().CheckShards(cost) && Input.GetKeyDown(keyBinding))
        {
            Trigger();
            resource.GetComponent<CrystalSword>().expendShard(cost);
        }
        remainingCD = Mathf.Clamp((Cd - elapsed), 0, Cd);
        if (F)
        {
            elapsed = Time.fixedTime - Timer;
        }
        else
        {
            elapsed = Cd;
        }
    }
    public override void Activate()
    {
        autoAttack.GetComponent<CrystalSwordAttack>().changer(Damage, Duration, sStats.Damage);
        autoAttack.GetComponent<CrystalSwordAttack>().changer(attackSpeed, Duration, sStats.CD);
    }
}
