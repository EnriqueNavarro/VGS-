using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatMeter : MonoBehaviour {
    public int threat;
    public GameObject player;//each enemy will have a threat meter per player
    [SerializeField] private bool taunt;//while taunted will ignore aggro and focus a player
    public void Taunt(float duration) {
        taunt = true;
        Invoke("Expire", duration);
    }
    private void Expire() {
        taunt = false;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
