using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour {
    [SerializeField] private Image[] keys = new Image[3];
    [SerializeField] private int keysCollected;
    public bool test;
	// Use this for initialization
	void Start () {
        keysCollected = 0;
		foreach(Image key in keys)
        {
            key.color = Color.black;
        }
	}
    public void KeyPicked()
    {
        
        keysCollected = (int)(Mathf.Clamp(keysCollected, 0, keys.Length));
        keys[keysCollected].color = Color.white;
        keysCollected++;
    }
	
	// Update is called once per frame
	void Update () {
        if (test)
        {
            foreach (Image key in keys)
            {
                key.color = Color.white;
            }
            test = false;
        }
	}
}
