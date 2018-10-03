using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour {
    GameObject[] sighted;
    private GameObject[] players= new GameObject[0];
    private int iterator = 0;
    [SerializeField] private float maxRange;
    [SerializeField] private bool[] inRange;
    [SerializeField] private bool[] LOS;
    public GameObject[] Players
    {
        get
        {
            return players;
        }

        set
        {
            players = new GameObject[value.Length];
            sighted = new GameObject[value.Length];
            inRange = new bool[value.Length];
            LOS = new bool[value.Length];
            players = value;
        }
    }

    public bool[] InRange
    {
        get
        {
            return inRange;
        }

        set
        {
            inRange = value;
        }
    }

    public bool[] LOS1
    {
        get
        {
            return LOS;
        }

        set
        {
            LOS = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        iterator++;
        if (iterator % 10 == 0)
        {
            RaycastHit hit;
            for (int i = 0; i < players.Length; i++)
            {
                if (Vector3.Distance(transform.position, players[i].transform.position) < maxRange)
                {
                    inRange[i] = true;
                    if (Physics.Raycast(transform.position, (players[i].transform.position - transform.position), out hit, maxRange))
                    {
                        if (hit.transform == players[i].transform)
                        {
                            LOS[i] = true;
                            // In Range and i can see you!
                            sighted[i] = players[i];
                        } else {
                            LOS[i] = false;
                        }
                    }
                } else {
                    inRange[i] = false;
                    LOS[i] = false;
                }
            }
        }
    }
    
}
