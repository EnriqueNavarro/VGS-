﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UtilityBehaviors : MonoBehaviour {
	void Update () {
		if (Input.GetKeyDown("z")){//reload scene, for testing purposes
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
