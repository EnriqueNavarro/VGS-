using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int lvl;
    [SerializeField] private float speed;
    [SerializeField] private ClassNames className;
    [SerializeField] private int physicalRes;//0-> none, 1->small, 2->medium, 3->large, 4->great
    [SerializeField] private int baseMagicRes;
    [SerializeField] private int fireRes;
    [SerializeField] private int frostRes;
    [SerializeField] private int lightRes;
    [SerializeField] private int shadowRes;
    [SerializeField] private int poisonRes;
    [SerializeField] private int xp;
    [SerializeField] private int xpRequiered = 0;
    [SerializeField] private int baseDmg;
    [SerializeField] private bool stealth = false;
    // Use this for initialization
    void Start () {
        GetComponent<PlayerMovement>().SpeedModifier = speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void damage(int dmg)
    {
        dmg = health - (int)dmg / (physicalRes + 1);
        health = Mathf.Clamp(dmg, 0, maxHealth);
        if (health == 0) death();
    }
    public void damage(int dmg, Elements element)
    {
        switch (element)
        {
            case Elements.fire:
                dmg = health - (int)dmg / (fireRes + 1);
                break;
            case Elements.frost:
                dmg = health - (int)dmg / (frostRes + 1);
                break;
            case Elements.poison:
                dmg = health - (int)dmg / (poisonRes + 1);
                break;
            case Elements.light:
                dmg = health - (int)dmg / (lightRes + 1);
                break;
            case Elements.shadow:
                dmg = health - (int)dmg / (shadowRes + 1);
                break;
        }
        health = Mathf.Clamp(dmg, 0, maxHealth);
        if (health == 0) death();
    }
    void death()
    {
        //to decide
    }
}
