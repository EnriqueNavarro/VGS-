using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbility : EnemyAbility
{
    private Vector3 lastPos;
    private Vector3 Movement;
    // Use this for initialization
    void Start () {
        if (melle)
        {
            Col.transform.localScale = new Vector3(Range / 10, 2, Range / 10);
            Col.transform.localPosition = new Vector3(Range / 20, 0, 0);
            lastPos = transform.position;
            Debug.Log(Col.transform.localPosition);
        }
        else
        {
            Col.transform.localScale = new Vector3(Range, 2, Range);
        }
    }
	
	// Update is called once per frame
	new void Update () {
        if (melle)
        {
            if (!Vector3.Equals(transform.position, lastPos))
            {
                Movement = transform.position - lastPos;
                AdjustCol();
            }
            lastPos = transform.position;

        }
    }
}
