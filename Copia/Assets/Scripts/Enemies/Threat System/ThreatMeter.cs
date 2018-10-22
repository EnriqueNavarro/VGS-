using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatMeter  {
    public int threat;
    public GameObject player;//each enemy will have a threat meter per player
    [SerializeField] private bool taunt;//while taunted will ignore aggro and focus a player

    public bool Taunt
    {
        get
        {
            return taunt;
        }

        set
        {
            taunt = value;
        }
    }

    public ThreatMeter(int threat, GameObject player, bool taunt)
    {
        this.threat = threat;
        this.player = player;
        this.Taunt = taunt;
    }

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
