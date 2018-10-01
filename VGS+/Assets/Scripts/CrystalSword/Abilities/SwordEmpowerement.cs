using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEmpowerement : Ability {
    [SerializeField] private GameObject autoAttack;
    [SerializeField] private float attackSpeed;
    public override void Activate()
    {
        autoAttack.GetComponent<CrystalSwordAttack>().changer(Damage, Duration, sStats.Damage);
        autoAttack.GetComponent<CrystalSwordAttack>().changer(attackSpeed, Duration, sStats.CD);
    }
}
