using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CrystalSword : Class {
    [SerializeField] private float[] bloodShards;
    [SerializeField] private GameObject[] shards;
    public bool testGenerator;
    public float tGenerate;
    private bool free;
    // Use this for initialization
   

    // Update is called once per frame
    void Update () {
        UpdateCds();
        if (Stealth)
        {
            MakeInvisible();
        }
        else
        {
            MakeVisible();
        }
        if (testGenerator) {
            generateShard(tGenerate);
            testGenerator = false;
        }
        for(int i=0;i<bloodShards.Length;i++) {
            shards[i].GetComponent<Animator>().SetFloat("Shard", ((int)(bloodShards[i] * 10)));
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
    public bool CheckShards(float cost)
    {
        if (free) cost = 0;
        float total = 0;
        foreach (float shard in bloodShards)
        {
            total += shard;
        }
        if (total < cost) return false;
        return true;
    }
    public bool expendShard(float cost) {
        //Debug.Log("expend");
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
        for(int i=0; i<bloodShards.Length;i++) {
            if(bloodShards[i]<1 && generated>0) {
                float toFill = 1 - bloodShards[i];
                if (generated <= toFill) {
                    //Debug.Log(bloodShards[i] + "+" + generated);
                    bloodShards[i] += generated;
                    generated = 0;
                    //Debug.Log(bloodShards[i] + "+" + generated);
                } else {
                    bloodShards[i] = 1;
                    generated -= toFill;                    
                }
            }
        }
    }
}
