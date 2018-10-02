using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenManager : MonoBehaviour {
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();//all enemies when spawn are added to this list
    [SerializeField] private int maxTokens;
    [SerializeField] private int currentTokens;
    [SerializeField] private float timer;
    [SerializeField] private List<Request> requests = new List<Request>();
    [SerializeField] private List<Request> sorted = new List<Request>();
    private int iterator=0;
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
        requests.Add(r);
    }
    // Update is called once per frame
    void FixedUpdate () {
        iterator++;
        if (iterator % 5 == 0) CheckRequests();
	}
    private Request Compare(Request r1, Request r2)
    {
        if (r1.totalValue >= r2.totalValue)return r1;
        return r2;
    }
    private void Sort()
    {
        Request r1=null;
        Request r2=null;
        Request r3=null;
        for (int i = 0; i < requests.Count; i++)
        {
            
            for(int j =0; j < sorted.Count; j++)
            {

            }
            if (sorted.Count == 0) sorted.Add(requests[i]);



        }

    }
    private void CheckRequests()
    {
        
        
    }
}
