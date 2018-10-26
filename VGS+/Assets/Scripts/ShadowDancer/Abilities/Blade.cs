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

    public float Instability
    {
        get
        {
            return instability;
        }

        set
        {
            instability = value;
        }
    }

    public float Stability
    {
        get
        {
            return stability;
        }

        set
        {
            stability = value;
        }
    }

    // Use this for initialization
    void Start () {
        Col.transform.localScale = new Vector3(Range, 2, Range);
        Activate();
        Instability = baseInstability;
        Stability = baseStability;
        Live = true;
        AdjustCol();

    }
    public void create() {
        Live = true;
        Instability = baseInstability;
        Stability = baseStability;
    }
    public void addInstability(int i) {
        Instability += i;
    }
    new public void Update(){}
    // Update is called once per frame
    public override void Activate(){}
    public  void Activate(List<GameObject> _enemies) {
        
        aaEnemies = _enemies;
        if (Live)
        {
            Damage = (int)(Instability + (BaseDamage * 10)) / 10;//to balance
            foreach (GameObject enemy in aaEnemies)
            {
                if (enemy.GetComponent<EnemyHealth>() != null)
                {
                    
                        Enemies target = enemy.GetComponent<EnemyHealth>().TypeName;
                        enemy.GetComponent<EnemyHealth>().damage(Damage, DmgType, Player1);
                        switch (target)
                        {
                            case Enemies.Plate:
                                Instability += plateModifier;
                                break;
                            case Enemies.Mail:
                                Instability += mailModifier;
                                break;
                            case Enemies.Leather:
                                Stability += leatherModifier;
                                Instability += leatherModifier + 2;
                                break;
                            case Enemies.Cloth:
                                Stability += clothModifier;
                                Instability += clothModifier + 2;
                                break;
                        }
                    
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
                enemy.GetComponent<EnemyHealth>().damage(Damage, Player1);
            }
        }
    }
    public void checkInstability() {
        if(Random.Range(0,Instability)>Stability) {
            Debug.Log("Blade exploded with "+Instability);
            Live = false;
            Damage = (int)(Instability + (BaseDamage * 10)) / 2;
            foreach (GameObject enemy in enemies) {
                enemy.GetComponent<EnemyHealth>().damage(Damage, DmgType, Player1);
            }
            foreach (GameObject ally in allies) {
                ally.GetComponent<Stats>().damage((int)Damage / 5, DmgType);
            }
            
        }

    }
}
