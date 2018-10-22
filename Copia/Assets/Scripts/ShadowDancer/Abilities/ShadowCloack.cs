using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCloack : Ability
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    

    
    public override void Activate()
    {
        GetComponentInParent<Stats>().Stealth = true;
        Invoke("endStealth", Duration);
        
    }
    void endStealth() {
        GetComponentInParent<Stats>().Stealth = false;
    }
}
