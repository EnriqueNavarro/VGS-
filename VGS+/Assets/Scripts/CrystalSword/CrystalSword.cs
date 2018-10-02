using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSword : Class {
    [SerializeField] private float[] bloodShards;
    public bool testCost;
    public float tCost;
    private bool free;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(testCost) {
            expendShard(tCost);
            testCost = false;
        }
	}
    public void Free(float duration)
    {
        free = true;
        Invoke("nfree", duration);
    }
    void nfree()
    {
        free = false;
    }
    public bool expendShard(float cost) {
        if (free) cost = 0;
        float total=0;
        foreach(float shard in bloodShards) {
            total += shard;
        }
        if (total < cost) return false;
        for (int i = 0; i < bloodShards.Length; i++)
        {
            if (bloodShards[i] > 0 && cost > 0)
            {
                if (cost >= bloodShards[i] && bloodShards[i]>0)
                {
                    cost -= bloodShards[i];
                    bloodShards[i] = 0;
                }
                else
                {
                    bloodShards[i] -= cost;
                    cost = 0;
                }
            }

        }
        return cost == 0;
    }
    public void generateShard(float generated) {
        Debug.Log("Generating");
        for(int i=0; i<bloodShards.Length;i++) {
            if(bloodShards[i]<1 && generated>0) {
                float toFill = 1 - bloodShards[i];
                if (generated < toFill) {
                    bloodShards[i] += generated;
                    generated = 0;
                } else {
                    bloodShards[i] = 1;
                    generated -= toFill;                    
                }
            }
        }
    }
}
