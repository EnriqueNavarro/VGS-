using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class : MonoBehaviour {
    [SerializeField] private ClassNames cName;
    [SerializeField] private Stats stats;
    [SerializeField] private List<Ability> passives; // each class has at *least* 1 passive
    [SerializeField] private List<Ability> actives; //each class has at *most* 4 actives
    [SerializeField] private bool stealth = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
