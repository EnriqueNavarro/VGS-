using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAdder : MonoBehaviour {

    public GameObject father;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            father.GetComponent<UndeadMist>().addEnemy(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            father.GetComponent<UndeadMist>().removeEnemy(other);
        }
    }
}
