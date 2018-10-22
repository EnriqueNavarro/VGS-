using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDancerAttack : Attack {
    [SerializeField] private GameObject bladeGameObject;
     
	// Use this for initialization
	void Start () {
        bladeGameObject.GetComponent<Blade>().BaseDamage = Damage;
        Col.transform.localScale = new Vector3(Range, 2, Range);
    }

    // Update is called once per frame
    public override void Activate()
    {
        bladeGameObject.GetComponent<Blade>().Activate(enemies);
    }
}
