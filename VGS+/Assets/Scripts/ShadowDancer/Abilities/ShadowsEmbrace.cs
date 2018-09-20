using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowsEmbrace : Ability
{
    [SerializeField] Vector3 pos;
    [SerializeField] float attackSpeedModifier;
    [SerializeField] float rangeModifier2;
    [SerializeField] float newCd;
    [SerializeField] GameObject attackGameObject;
    [SerializeField] GameObject ShadowStepGameObject;
    [SerializeField] private GameObject player;
    float oldCd;
    // Use this for initialization
    void Start()
    {

    }

    public override void Activate()
    {
        Debug.Log("activated");
        attackGameObject.GetComponent<Attack>().attackSpeed(attackSpeedModifier, Duration);
        attackGameObject.GetComponent<Attack>().changeRange(rangeModifier2,Duration);
        oldCd=ShadowStepGameObject.GetComponent<ShadowStep>().Cd;
        ShadowStepGameObject.GetComponent<ShadowStep>().Cd = newCd;
        Invoke("endCDs", Duration);
    }
    private void endCDs() {
        ShadowStepGameObject.GetComponent<ShadowStep>().Cd = oldCd;
    }
}
