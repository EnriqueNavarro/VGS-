using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeyPickup : MonoBehaviour {
    public bool ready;
    [SerializeField] private GameObject keyManager;

    // Use this for initialization
    void Start () {
        this.transform.rotation = Quaternion.Euler(90, 0, 0);
        Invoke("Ready", 0.5f);
	}
    void Ready()
    {
        ready = true;
        keyManager = GameObject.FindGameObjectWithTag("KeyManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && ready)
        {
            Debug.Log("Key Picked Up");
            keyManager.GetComponent<KeyManager>().KeyPicked();
            Destroy(this.gameObject);
        }
    }
}
