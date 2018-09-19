using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Ability
{

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	
    public override void Activate()
    {
        foreach(GameObject enemy in enemies) {
            enemy.GetComponent<EnemyHealth>().damage(Damage);
        }
    }
}
