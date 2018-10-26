using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
	public GameObject[] enemy;

	private Renderer rend;
	private int rnd;

	[SerializeField]
	private bool alreadySpawned = false;

	void Start () {
		rnd = Random.Range (0, 2);
		rend = GetComponent<Renderer> ();
	}

	void Update () {
		if (rend.isVisible && !alreadySpawned) {
			alreadySpawned = true;
			Instantiate (enemy [rnd].gameObject, transform.position, Quaternion.identity);
		}
	}
}
