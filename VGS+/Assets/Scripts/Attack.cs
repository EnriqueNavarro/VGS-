using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Ability
{
    float modifierS;
	// Use this for initialization
	void Start () {
        Col.transform.localScale = new Vector3(Range, 2, Range);
    }
	
	// Update is called once per frame
	
    public override void Activate()
    {
        foreach(GameObject enemy in enemies) {
            enemy.GetComponent<EnemyHealth>().damage(Damage);
        }
    }
    public void attackSpeed(float _modifier, float time) {
        modifierS = _modifier;
        Cd *= modifierS;
        Invoke("expire", time);
    }
    private void expire() {
        Cd /= modifierS;
    }
    
}
