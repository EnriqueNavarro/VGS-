using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowDancer : Class {
    [SerializeField] private GameObject UI;
    [SerializeField] private Slider stabilitySlide;
    [SerializeField] private Slider instabilitySlide;
    [SerializeField] private int stability;
    [SerializeField] private int instability;
    [SerializeField] private GameObject Blade;
    [SerializeField] private bool live;
	// Use this for initialization
	void Start () {
        UI.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCds();
        if (Stealth)
        {
            MakeInvisible();
        } else
        {
            MakeVisible();
        }
        Stealth = GetComponent<Stats>().Stealth;
        live = Blade.GetComponent<Blade>().Live;
        if (live)
        {
            stability = (int)Blade.GetComponent<Blade>().Stability;
            instability = (int)Blade.GetComponent<Blade>().Instability;
            int total = stability + instability+1;
            instabilitySlide.value = instability * 100 / total;
            stabilitySlide.value = stability * 100 / total;
        } else {
            instabilitySlide.value = 0;
            stabilitySlide.value = 100;
        }
    }
}
