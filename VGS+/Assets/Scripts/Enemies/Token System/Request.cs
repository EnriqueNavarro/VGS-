using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request : MonoBehaviour {
    public GameObject requester;// who is making the request
    public bool inRange;// if the requester is in range to do the attack, only applicable to melle
    public bool LOS;//if the requester would be able to shoot and hit the target
    public int toHit;// how hard the hit is going to be for
    public float distance;//distance to target
    public int threat;//how much threat does the requester have on its target
    public int targetHP;//how much hp does the target have
    public int priority;//used to make a certain enemy have priority, the higher the number the more important
    public float totalValue=0;
    public int cost;

    public Request(GameObject requester, bool inRange, bool lOS, int toHit, float distance, int threat, int targetHP)
    {
        this.requester = requester;
        this.inRange = inRange;
        LOS = lOS;
        this.toHit = toHit;
        this.distance = distance;
        this.threat = threat;
        this.targetHP = targetHP;
        if (!inRange && !LOS) totalValue -= int.MaxValue;
        totalValue += toHit;
        totalValue += 10 + 1 / distance;
        totalValue += threat;
        totalValue += targetHP * 2;
    }

    public Request(GameObject requester, bool inRange, bool lOS, int toHit, float distance, int threat, int targetHP, int priority)
    {
        this.requester = requester;
        this.inRange = inRange;
        LOS = lOS;
        this.toHit = toHit;
        this.distance = distance;
        this.threat = threat;
        this.targetHP = targetHP;
        this.priority = priority;
        if (!inRange && !LOS) totalValue -= int.MaxValue;
        totalValue += toHit;
        totalValue += 10 + 1 / distance;
        totalValue += threat;
        totalValue += targetHP * 2;
        totalValue += priority*1000;

    }
}
