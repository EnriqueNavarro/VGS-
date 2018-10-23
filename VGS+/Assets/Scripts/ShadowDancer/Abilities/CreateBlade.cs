using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBlade : Ability
{
    public GameObject blade;
    [SerializeField] private int extraInstability;

    
    public override void Activate() {        
        if(!blade.GetComponent<Blade>().Live) {
            blade.GetComponent<Blade>().create();
            blade.GetComponent<Blade>().Live = true;
        } else {
            blade.GetComponent<Blade>().addInstability(extraInstability);
        }
    }

}
	// Update is called once per frame
	
