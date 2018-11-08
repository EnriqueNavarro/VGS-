using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailerControler : MonoBehaviour {
    [SerializeField] private bool combat;
    [SerializeField] private GameObject target;
    public int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int phase;
    private int lastPhase;
    public int percentage;
    [SerializeField] private GameObject[] actives;
    [SerializeField] private GameObject undeadMist;
	// Use this for initialization
	void Start () {
        phase = 1;
        maxHealth = this.GetComponent<EnemyHealth>().MaxHealth;
        actives = this.GetComponent<EnemyType>().Abilities;
	}
	void CheckPhase() {
        if(percentage<=80&& phase==1) {
            phase = 2;
        } else {
            if (percentage <= 60 && phase == 2) {
                phase = 3;
            } else {
                if (percentage <= 40 && phase == 3) {
                    phase = 4;
                } else {
                    if (percentage <= 20 && phase == 4) {
                        phase = 5;
                    }
                }
            }
        }
    }
    void CancelAllInvokes() {
        foreach(GameObject ability in actives) {
            ability.GetComponent<EnemyAbility>().CancelInvoke();
        }
    }
	// Update is called once per frame
	void Update () {
        combat = this.GetComponent<EnemyHealth>().combat;
        target = this.GetComponent<EnemyHealth>().Attacker;
        maxHealth = this.GetComponent<EnemyHealth>().MaxHealth;
        currentHealth = this.GetComponent<EnemyHealth>().Health;
        percentage = (currentHealth * 100 / maxHealth);
        lastPhase = phase;
        if (phase != 5) CheckPhase();
        if(lastPhase!=phase) {
            CancelAllInvokes();
            this.GetComponentInChildren<Execute>().activate=true;
            undeadMist.GetComponentInChildren<UndeadMist>().ChangePhase(phase);
        }



    }
}
