using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageTest : MonoBehaviour {
    private bool once=true;
    [SerializeField] private GameObject UI;
    [SerializeField] bool test;
    [SerializeField] GameObject ShadowDancer;
    [SerializeField] GameObject CrystalSword;
	// Use this for initialization
	void Start () {
        if (!test)
        {
            ShadowDancer.SetActive(false);
            CrystalSword.SetActive(false);
            Time.timeScale = 0;
            UI.SetActive(true);
            if (CharacterSelection.chosen == ClassNames.ShadowDancer)
            {
                ShadowDancer.SetActive(true);
            }
            if (CharacterSelection.chosen == ClassNames.CrystalSword)
            {
                CrystalSword.SetActive(true);
            }
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (once) Time.timeScale = 0;
		if(once && Input.GetKeyDown("1")&&!test)
        {
            once = false;
            Time.timeScale = 1;
            UI.SetActive(false);
        }
	}
}
