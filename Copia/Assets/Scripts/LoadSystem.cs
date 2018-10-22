using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSystem : MonoBehaviour {
	
	private GameObject screenLoading;
	private float loadDelay = 2f;

	private void OnEnable()
	{
		Debug.Log("OnEnable");
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		Debug.Log("OnDisable");
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
	
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("OnSceneLoaded");
		InitGame();
	}

	public void InitGame()
	{
		Debug.Log("InitGame");
		screenLoading = GameObject.Find("LoadScreen");
		screenLoading.SetActive(true);
		
		StartCoroutine (HideLoading());
	}

	IEnumerator HideLoading()
	{
		yield return new WaitForSeconds(2f);
		Debug.Log("Hide");
		screenLoading.SetActive(false);
		
	}
}
