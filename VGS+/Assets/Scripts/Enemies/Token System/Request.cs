using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request  {
    public GameObject requester;// who is making the request
    public bool inRange;// if the requester is in range to do the attack, only applicable to melle
    public bool LOS;//if the requester would be able to shoot and hit the target
    public int toHit;// how hard the hit is going to be for
    public float distance;//distance to target
    public int threat;//how much threat does the requester have on its target
    public int targetHP;//how much hp does the target have
    public float range;//range of the attack
    public int priority;//used to make a certain enemy have priority, the higher the number the more important
    public float totalValue=0;
    public int cost=0;
    public Request() {

    }
    
    public Request(GameObject requester, bool inRange, bool lOS, int toHit, float distance, int threat, int targetHP, float range, int priority)
    {
        this.requester = requester;
        this.inRange = inRange;
        LOS = lOS;
        this.toHit = toHit;
        this.distance = distance;
        this.threat = threat;
        this.targetHP = targetHP;
        this.range = range;
        this.priority = priority;
        CalculateValue();
        CalculateCost();
    }

    public  Request(GameObject requester, bool inRange, bool lOS, int toHit, float distance, int threat, int targetHP, float range)
    {
        this.requester = requester;
        this.inRange = inRange;
        LOS = lOS;
        this.toHit = toHit;
        this.distance = distance;
        this.threat = threat;
        this.targetHP = targetHP;
        this.range = range;
        CalculateValue();
        CalculateCost();
    }

    private void CalculateValue() {
        if (!inRange && !LOS) totalValue -= int.MaxValue;
        totalValue += toHit;
        totalValue += 10 / distance;
        totalValue += threat;
        totalValue += targetHP;
        totalValue += range;
        totalValue += priority * 100;
    }
    private void CalculateCost() {
        cost += toHit;
        cost += (int)range;
        //Debug.Log("cost: " + cost + "dmg: "+toHit);
    }
}
