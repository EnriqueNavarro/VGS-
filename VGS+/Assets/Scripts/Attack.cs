using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Ability
{
    float modifier;
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
    public void attackSpeed(float _modifier, float time) {
        modifier = _modifier;
        Cd *= modifier;
        Invoke("expire", time);
    }
    private void expire() {
        Cd /= modifier;
    }
}
