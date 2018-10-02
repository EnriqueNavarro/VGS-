using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flurry : Ability {
    [SerializeField] private GameObject resource;
    [SerializeField] private float cost;
    // Update is called once per frame
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
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyHealth>().damage(Damage, DmgType);
        }
    }
}
