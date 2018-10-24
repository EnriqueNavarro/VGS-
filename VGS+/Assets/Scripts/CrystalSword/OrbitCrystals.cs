using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCrystals : MonoBehaviour {
    [SerializeField] private GameObject father;
    [SerializeField] private float speed;
	// Use this for initialization
	void Start () {
		
	}
	void OrbitAround() {
        transform.RotateAround(father.transform.position, new Vector3(0,0,1), speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(90,0,0);
    }
	// Update is called once per frame
	void Update () {
        OrbitAround();
	}
}
