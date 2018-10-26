using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flurry : Ability {
    [SerializeField] private GameObject resource;
    [SerializeField] private float cost;
    // Update is called once per frame
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
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyHealth>().damage(Damage, DmgType,resource.GetComponent<Stats>().CritChance, resource.GetComponent<Stats>().CritDamage, Player1);
        }
    }
}
