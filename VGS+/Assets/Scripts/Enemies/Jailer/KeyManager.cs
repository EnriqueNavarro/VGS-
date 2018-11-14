using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour {
    [SerializeField] private Image[] keys = new Image[3];
    [SerializeField] private int keysCollected;
    public bool test;

    public int KeysCollected
    {
        get
        {
            return keysCollected;
        }

        set
        {
            keysCollected = value;
        }
    }

    // Use this for initialization
    void Start () {
        KeysCollected = 0;
		foreach(Image key in keys)
        {
            key.color = Color.black;
        }
	}
    public void KeyPicked()
    {
        
        KeysCollected = (int)(Mathf.Clamp(KeysCollected, 0, keys.Length));
        keys[KeysCollected].color = Color.white;
        KeysCollected++;
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
