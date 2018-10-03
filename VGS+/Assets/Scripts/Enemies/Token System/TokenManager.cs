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
            } else {
                buffer[i].totalValue += ageValue;
            }
        }
	}
    private void Approve(Request approved) {
        //to decide
    } 
    
}
