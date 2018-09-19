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
        GetComponent<Stats>().Stealth = true;
        Invoke("endStealth", Duration);
        
    }
    void endStealth() {
        GetComponent<Stats>().Stealth = false;
    }
}
