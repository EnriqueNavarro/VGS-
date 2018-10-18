using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			other.transform.position = transform.GetChild (0).position;
		}
	}
}
