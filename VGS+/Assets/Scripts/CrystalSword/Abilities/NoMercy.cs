using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMercy : Ability {
    [SerializeField] private GameObject[] activeAbilities;
    [SerializeField] private float _rangeModifier;
    [SerializeField] private GameObject resource;
    [SerializeField] private float cost;
    new public void Update()
    {
        if (resource.GetComponent<CrystalSword>().CheckShards(cost))
        {
            if (Input.GetKeyDown(keyBinding))
            {
                Trigger();
                elapsed = Time.fixedTime - Timer;
                resource.GetComponent<CrystalSword>().expendShard(cost);
            }
        }
    }
    public override void Activate()
    {
        resource.GetComponent<CrystalSword>().Free(Duration);
        for(int i = 0; i < activeAbilities.Length; i++)
        {
            activeAbilities[i].GetComponent<Ability>().changer(_rangeModifier, Duration, sStats.Range);
        }
    }
}
