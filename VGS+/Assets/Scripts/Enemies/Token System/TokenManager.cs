using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenManager : MonoBehaviour {
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();//all enemies when spawn are added to this list
    [SerializeField] private int maxTokens;
    [SerializeField] private int currentTokens;
    [SerializeField] private float timer;
    [SerializeField] private List<Request> buffer = new List<Request>();
    [SerializeField] private float ageValue;//adds value to older requests, must be a very small number since it is added on fixed update
    public void Died(GameObject dead)
    {
        for (int i = 0; i < buffer.Count; i++)
        {
            if (buffer[i].requester == dead)
            {
                buffer.RemoveAt(i);
                i--;
            }
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            GameObject[] actives = player.GetComponent<Class>().Actives;
            for(int i=0;i< player.GetComponent<Class>().Actives.Length;i++) {
                player.GetComponent<Class>().Actives[i].GetComponent<Ability>().removeEnemy(dead);
            }
        }
    }
	// Use this for initialization
	void Start () {
        currentTokens = maxTokens;
	}
	public void Adder(GameObject e)//when an enemy spawns it must be added here
    {
        enemies.Add(e);
    }
    public void remover(GameObject e)//when an enemy dies, it must be eliminated here
    {
        enemies.Remove(e);
    }
    public void Refund(int refund) {
        currentTokens += refund;
    }
    public void AddRequest(Request r)
    {
        if(buffer.Count==0) {
            buffer.Add(r);
        } else {
            bool inserted = false;
            for(int i=0;i<buffer.Count;i++) 
            {
                if (r.totalValue < buffer[i].totalValue)
                {
                    buffer.Insert(i, r);
                    inserted = true;
                }
            }
            if (!inserted) buffer.Add(r);
        }
    }
    // Update is called once per frame
    void FixedUpdate () {
        for(int i=0;i<buffer.Count;i++) {
            if(buffer[i].cost<=currentTokens) {
                Approve(buffer[i]);
                buffer.Remove(buffer[i]);
            } else {
                buffer[i].totalValue += ageValue;
            }
        }
	}
    private void Approve(Request approved) {
        //to decide
        int j=-1;
        //Debug.Log(approved.cost);
        for(int i=0;i<enemies.Count;i++) {
            if (enemies[i] == approved.requester) j = i;
        }
        if (j < 0) return;
        int size = enemies[j].GetComponent<EnemyType>().Abilities.Length;
        for (int i=0;i<size;i++) {
            Request aux = enemies[j].GetComponent<EnemyType>().Abilities[i].GetComponent<EnemyAbility>().Request;
            if(aux.cost==approved.cost) {
                approved.requester.GetComponent<EnemyType>().Abilities[i].GetComponent<EnemyAbility>().Approved = true;
                currentTokens -= aux.cost;
                //Debug.Log("Approving for " + aux.cost);
            }
        }
    } 
    
}
