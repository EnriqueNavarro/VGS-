using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Ability
{

	// Use this for initialization
	void Start () {
        keyBinding="a";
        name = "Basic attack";
	}
	
	// Update is called once per frame
	public override void Update () {
        if (Input.GetKeyDown(keyBinding)) Trigger();
    }
    public override void Activate()
    {
        Debug.Log("auto attack");
    }
}
