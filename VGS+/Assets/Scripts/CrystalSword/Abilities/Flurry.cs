using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flurry : Ability {
    

    // Update is called once per frame
    public override void Activate()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyHealth>().damage(Damage, DmgType);
        }
    }
}
