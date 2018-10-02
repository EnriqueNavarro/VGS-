using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEmpowerement : Ability {
    [SerializeField] private GameObject autoAttack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private GameObject resource;
    [SerializeField] private float cost;
    public void Update()
    {
        if (resource.GetComponent<CrystalSword>().expendShard(cost))
        {
            if (Input.GetKeyDown(keyBinding)) Trigger();
            elapsed = Time.fixedTime - Timer;
        }
    }
    public override void Activate()
    {
        autoAttack.GetComponent<CrystalSwordAttack>().changer(Damage, Duration, sStats.Damage);
        autoAttack.GetComponent<CrystalSwordAttack>().changer(attackSpeed, Duration, sStats.CD);
    }
}
