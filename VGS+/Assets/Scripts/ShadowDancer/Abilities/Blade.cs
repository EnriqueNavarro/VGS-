using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : Ability {
    [SerializeField] private float instability;
    [SerializeField] private float plateModifier;
    [SerializeField] private float mailModifier;
    [SerializeField] private float leatherModifier;
    [SerializeField] private float clothModifier;
    [SerializeField] private int baseDamage;
    [SerializeField] private float stability;
    [SerializeField] private float baseInstability;
    [SerializeField] private float baseStability;
    [SerializeField] private bool live;
    private List<GameObject> aaEnemies;
    public int BaseDamage
    {
        get
        {
            return baseDamage;
        }

        set
        {
            baseDamage = value;
        }
    }

    public bool Live
    {
        get
        {
            return live;
        }

        set
        {
            live = value;
        }
    }

    // Use this for initialization
    void Start () {
        Col.transform.localScale = new Vector3(Range, 2, Range);
        Activate();
        instability = baseInstability;
        stability = baseStability;
        Live = true;

    }
    public void create() {
        Live = true;
        instability = baseInstability;
        stability = baseStability;
    }
    public void addInstability(int i) {
        instability += i;
    }
    public void Update(){}
    // Update is called once per frame
    public override void Activate(){}
    public  void Activate(List<GameObject> _enemies) {
        
        aaEnemies = _enemies;
        if (Live)
        {
            Damage = (int)(instability + (BaseDamage * 10)) / 10;//to balance
            foreach (GameObject enemy in aaEnemies)
            {
                Enemies target = enemy.GetComponent<EnemyHealth>().TypeName;
                enemy.GetComponent<EnemyHealth>().damage(Damage, DmgType);
                switch (target)
                {
                    case Enemies.Plate:
                        instability += plateModifier;
                        break;
                    case Enemies.Mail:
                        instability += mailModifier;
                        break;
                    case Enemies.Leather:
                        stability += leatherModifier;
                        instability += leatherModifier + 2;
                        break;
                    case Enemies.Cloth:
                        stability += clothModifier;
                        instability += clothModifier + 2;
                        break;
                }
            }
            checkInstability();
        }
        else
        {
            Damage = BaseDamage;//to balance
            foreach (GameObject enemy in aaEnemies)
            {
                Enemies target = enemy.GetComponent<EnemyHealth>().TypeName;
                enemy.GetComponent<EnemyHealth>().damage(Damage);
            }
        }
    }
    public void checkInstability() {
        if(Random.Range(0,instability)>stability) {
            Debug.Log("Blade exploded with "+instability);
            Live = false;
            Damage = (int)(instability + (BaseDamage * 10)) / 2;
            foreach (GameObject enemy in enemies) {
                enemy.GetComponent<EnemyHealth>().damage(Damage, DmgType);
            }
            foreach (GameObject ally in allies) {
                ally.GetComponent<Stats>().damage((int)Damage / 5, DmgType);
            }
            
        }

    }
}
