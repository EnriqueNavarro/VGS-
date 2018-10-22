using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDancer : Class {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Stealth = GetComponent<Stats>().Stealth;
	}
}
