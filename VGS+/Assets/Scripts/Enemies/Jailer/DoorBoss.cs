using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorBoss : MonoBehaviour {
    [SerializeField] int numberOfKeys;
    [SerializeField] private KeyManager keyManager;
    [SerializeField] private bool open;
    [SerializeField] Text text;
    private void Start()
    {
        open = false;
    }
    void Update () {
        open = (keyManager.KeysCollected == numberOfKeys);
	}
    private void OnTriggerEnter(Collider other)
    {
        if(open && other.tag == "Player")
        {
            SceneManager.LoadScene("BossScene");
        } else
        {
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            text.text = "You need 3 keys in order to trespass";
        }
        else
        {
            text.text = "";
        }
    }
}
