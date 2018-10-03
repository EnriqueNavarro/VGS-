using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSwordAttack : Attack {
    [SerializeField] private GameObject resource;
    [SerializeField] private float generate;
	
	

    // Update is called once per frame
    public override void Activate()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyHealth>().damage(Damage,resource.GetComponent<Stats>().CritChance, resource.GetComponent<Stats>().CritDamage, this.gameObject);
            resource.GetComponent<CrystalSword>().generateShard(generate);
        }
    }
}
