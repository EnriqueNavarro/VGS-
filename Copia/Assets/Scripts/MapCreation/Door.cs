using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	[SerializeField]
	MapCamera camera;

	void Start () {
		camera = FindObjectOfType <MapCamera> ();
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			other.transform.position = transform.GetChild (0).position;
			camera.Move (gameObject);
		}
	}
}
