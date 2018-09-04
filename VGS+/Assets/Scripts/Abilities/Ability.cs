using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private bool requireTarget;
    [SerializeField] private bool selfTarget;
    [SerializeField] private int cd; //secs
    [SerializeField] private float timer;
    [SerializeField] private GameObject particleflefx;
    [SerializeField] private int duration;
    [SerializeField] private float range;
    [SerializeField] private Elements dmgType;
    [SerializeField] private int damage;
    public string keyBinding; // this must be rewritten
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    abstract public void Update();
    //if (Input.GetKeyDown(keyBinding)) Trigger(); agregar esa linea en cada update
    public void Trigger() {
        if ((Time.fixedTime - timer) >= cd)
        {
            timer = Time.fixedTime;
            Activate();
        }
    }
    abstract public void Activate();
}
