using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageTest : MonoBehaviour {
    private bool once=true;
    [SerializeField] private GameObject UI;
    [SerializeField] bool test;
    [SerializeField] GameObject ShadowDancer;
    [SerializeField] GameObject CrystalSword;
    [SerializeField] bool bossRoom;
    [SerializeField] string sceneName;
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
        if (once&&!test) Time.timeScale = 0;
		if(once && Input.GetKeyDown("1")&&!test)
        {
            once = false;
            Time.timeScale = 1;
            UI.SetActive(false);
        }
        if(bossRoom && Input.GetKeyDown("z"))
        {
            Debug.Log("Reloading level");
            SceneManager.LoadScene(sceneName);
        }
	}
}
