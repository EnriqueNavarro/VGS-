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
    [SerializeField] private float distance;
    [SerializeField] private GameObject player;
    private float delay;
    [SerializeField] private float autoCD;
    [SerializeField] private float ensaringCD;
    [SerializeField] private bool busy;
    private float lastAA;
    private float lastEnsaring;
    [SerializeField] private float aaRemainingCD;
    [SerializeField] private float enRemainingCD;
    [SerializeField] private bool useAA;
    [SerializeField] private bool useEnsaring;
    private float deltaTime = 2;
    private float deltaStart;
    private bool wasInProcess;

    public bool Busy
    {
        get
        {
            return busy;
        }

        set
        {
            busy = value;
        }
    }

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
    void ActivateAA() {
        this.GetComponentInChildren<JailerAutoAttack>().activate = true;
        lastAA = Time.fixedTime;
    }
    void ActivateEnsaringStrike() {
        this.GetComponentInChildren<EnsaringStrike>().activate = true;
        lastEnsaring = Time.fixedTime;
    }
	// Update is called once per frame
	void Update () {
        combat = this.GetComponent<EnemyHealth>().combat;
        if(combat) {
            player = this.GetComponent<EnemyHealth>().Attacker;
            wasInProcess = busy;
            Busy = false;
            aaRemainingCD = Mathf.Clamp(autoCD + (lastAA - Time.fixedTime),0,autoCD);
            enRemainingCD = Mathf.Clamp(ensaringCD + (lastEnsaring - Time.fixedTime), 0, ensaringCD);
            foreach (GameObject ability in actives) {
                Busy = Busy || ability.GetComponent<BossAbility>().InProcess;
            }
            if (Busy) this.GetComponent<JailerMovement>().Current = MovementType.Halt;
            if (!Busy && wasInProcess) deltaStart = Time.fixedTime;
            if(!Busy && (Time.fixedTime-deltaStart>=deltaTime)) {
                if(aaRemainingCD==0 && this.GetComponentInChildren<JailerAutoAttack>().enemies.Count>0) {
                    ActivateAA();
                } else {
                if(enRemainingCD==0 && this.GetComponentInChildren<EnsaringStrike>().enemies.Count>0) {
                        ActivateEnsaringStrike();
                }
                }
            }
        } else {
            player = null;
        }
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
