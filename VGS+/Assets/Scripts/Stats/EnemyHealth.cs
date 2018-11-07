using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int lvl = 0;
    [SerializeField] private float speed;
    [SerializeField] private Enemies typeName;
    [SerializeField] private int physicalRes;//0-> none, 1->small, 2->medium, 3->large, 4->great
    [SerializeField] private int baseMagicRes;
    [SerializeField] private int fireRes;
    [SerializeField] private int frostRes;
    [SerializeField] private int lightRes;
    [SerializeField] private int shadowRes;
    [SerializeField] private int poisonRes;
    [SerializeField] private int baseDmg;
    [SerializeField] private bool stealth = false;
    [SerializeField] private GameObject attacker;//the player with the most threat
    public TokenManager tokenManager;
    private int number;//number of target in the array
    private ThreatMeter[] threat;
    public int max;
    public bool combat;
    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = value;
        }
    }

    public int Lvl
    {
        get
        {
            return lvl;
        }

        set
        {
            lvl = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public int PhysicalRes
    {
        get
        {
            return physicalRes;
        }

        set
        {
            physicalRes = value;
        }
    }

    public int BaseMagicRes
    {
        get
        {
            return baseMagicRes;
        }

        set
        {
            baseMagicRes = value;
        }
    }

    public int FireRes
    {
        get
        {
            return fireRes;
        }

        set
        {
            fireRes = value;
        }
    }

    public int FrostRes
    {
        get
        {
            return frostRes;
        }

        set
        {
            frostRes = value;
        }
    }

    public int LightRes
    {
        get
        {
            return lightRes;
        }

        set
        {
            lightRes = value;
        }
    }

    public int ShadowRes
    {
        get
        {
            return shadowRes;
        }

        set
        {
            shadowRes = value;
        }
    }

    public int PoisonRes
    {
        get
        {
            return poisonRes;
        }

        set
        {
            poisonRes = value;
        }
    }

    public int BaseDmg
    {
        get
        {
            return baseDmg;
        }

        set
        {
            baseDmg = value;
        }
    }

    public bool Stealth
    {
        get
        {
            return stealth;
        }

        set
        {
            stealth = value;
        }
    }

    public Enemies TypeName
    {
        get
        {
            return typeName;
        }

        set
        {
            typeName = value;
        }
    }

    public ThreatMeter[] Threat
    {
        get
        {
            return threat;
        }

        set
        {
            threat = value;
        }
    }

    public GameObject Attacker
    {
        get
        {
            return attacker;
        }

        set
        {
            attacker = value;
        }
    }

    public int Number
    {
        get
        {
            return number;
        }

        set
        {
            number = value;
        }
    }

    void Start()
    {
        tokenManager = GameObject.FindGameObjectWithTag("TokenManager").GetComponent<TokenManager>();
        Attacker = null;
        if (lvl == 0) lvl = 1;
        BaseDmg = 1;
        switch (TypeName)
        {
            case Enemies.Plate:
                health = 60;
                maxHealth = health;
                physicalRes = 3;
                baseMagicRes = 0;
                fireRes = baseMagicRes;
                frostRes = baseMagicRes;
                LightRes = baseMagicRes;
                ShadowRes = baseMagicRes;
                poisonRes = baseMagicRes;
                break;
            case Enemies.Mail:
                health = 45;
                maxHealth = health;
                physicalRes = 2;
                baseMagicRes = 1;
                fireRes = baseMagicRes;
                frostRes = baseMagicRes;
                LightRes = baseMagicRes;
                ShadowRes = baseMagicRes;
                poisonRes = baseMagicRes;
                break;
            case Enemies.Leather:
                health = 30;
                maxHealth = health;
                physicalRes = 1;
                baseMagicRes = 2;
                fireRes = baseMagicRes;
                frostRes = baseMagicRes;
                LightRes = baseMagicRes;
                ShadowRes = baseMagicRes;
                poisonRes = baseMagicRes;
                break;
            case Enemies.Cloth:
                health = 20;
                maxHealth = health;
                physicalRes = 0;
                baseMagicRes = 3;
                fireRes = baseMagicRes;
                frostRes = baseMagicRes;
                LightRes = baseMagicRes;
                ShadowRes = baseMagicRes;
                poisonRes = baseMagicRes;
                break;
            case Enemies.Jailer:
                health = 600;
                maxHealth = health;
                physicalRes = 2;
                baseMagicRes = 2;
                fireRes = baseMagicRes;
                frostRes = baseMagicRes;
                LightRes = baseMagicRes;
                ShadowRes = baseMagicRes;
                poisonRes = baseMagicRes;
                break;

        }
        tokenManager.Adder(this.gameObject);
    }

    private void Update()
    {
        if (attacker != null)
        {
            combat = true;
            if (attacker.GetComponent<Stats>().Stealth)
            {
                UpdateTarget();
            }
        }
        this.GetComponentInChildren<Slider>().value= (Health * 100) / MaxHealth;
    }
    public void CheckLOS(bool[] LOS) {
        for(int i=0;i<threat.Length;i++) {
            if(LOS[i] && threat[i].threat==0) {
                threat[i].threat += 1;//when a player comes into line of sight and the enemy has no threat it must start pursing said player
            }
        }
        UpdateTarget();
    }
    private void UpdateTarget() {
        max = 0;
        number = -1;
        for (int i = 0; i < threat.Length; i++)
        {
            if (threat[i].threat >= max)
            {
                max = threat[i].threat;
                
                if (!threat[i].player.GetComponent<Stats>().Stealth)
                {
                    Attacker = threat[i].player;
                    number = i;
                }
            }
        }
        if (number == -1)
        {
            combat = false;
            Attacker = null;
        }
    }
    public void AddThreat(int dmg, GameObject attacker1) {
        for(int i=0;i<threat.Length;i++) {
            if (threat[i].player==attacker1) {
                threat[i].threat += dmg;
                //Debug.Log(threat[i].player + " gained threat=" + dmg);
            }
        }
        UpdateTarget();
    }
    public void damage(int dmg, GameObject attacker1)
    {
        AddThreat(dmg, attacker1);
        dmg = health - (int)Mathf.Floor(dmg * (10 - physicalRes * 2) / 10);
        Health = Mathf.Clamp(dmg, 0, MaxHealth);
        if (Health == 0) death();        
    }
    public void damage(int dmg, Elements element, GameObject attacker1)
    {
        AddThreat(dmg, attacker1);
        switch (element)
        {
            case Elements.fire:
                dmg = health - (int)Mathf.Floor(dmg * (10 - fireRes * 2) / 10);
                break;
            case Elements.frost:
                dmg = health - (int)Mathf.Floor(dmg * (10 - frostRes * 2) / 10);
                break;
            case Elements.poison:
                dmg = health - (int)Mathf.Floor(dmg * (10 - poisonRes * 2) / 10);
                break;
            case Elements.light:
                dmg = health - (int)Mathf.Floor(dmg * (10 - lightRes * 2) / 10);
                break;
            case Elements.shadow:
                dmg = health - (int)Mathf.Floor(dmg * (10 - shadowRes * 2) / 10);
                break;
            case Elements.physical:
                dmg = Health - (int)Mathf.Floor(dmg * (10 - PhysicalRes * 2) / 10);
                break;
            case Elements.none:
                dmg = Health - (int)Mathf.Floor(dmg * (10 - PhysicalRes * 2) / 10);
                break;
        }
        Health = Mathf.Clamp(dmg, 0, MaxHealth);
        if (Health == 0) death();
    }
    public void damage(int dmg, float critChance, float CritDamage, GameObject attacker1)
    {
        bool crit = (Random.Range(0, 1) < critChance);
        if (crit) dmg = ((int)(dmg * CritDamage));
        AddThreat(dmg, attacker1);
        dmg = health - (int)Mathf.Floor(dmg * (10 - physicalRes * 2) / 10);
        Health = Mathf.Clamp(dmg, 0, MaxHealth);
        if (Health == 0) death();
    }
    public void damage(int dmg, Elements element, float critChance, float CritDamage, GameObject attacker1)
    {
        bool crit = (Random.Range(0, 1) < critChance);
        if (crit) dmg = ((int)(dmg * CritDamage));
        AddThreat(dmg, attacker1);
        switch (element)
        {
            case Elements.fire:
                dmg = health - (int)Mathf.Floor(dmg * (10 - fireRes * 2) / 10);
                break;
            case Elements.frost:
                dmg = health - (int)Mathf.Floor(dmg * (10 - frostRes * 2) / 10);
                break;
            case Elements.poison:
                dmg = health - (int)Mathf.Floor(dmg * (10 - poisonRes * 2) / 10);
                break;
            case Elements.light:
                dmg = health - (int)Mathf.Floor(dmg * (10 - lightRes * 2) / 10);
                break;
            case Elements.shadow:
                dmg = health - (int)Mathf.Floor(dmg * (10 - shadowRes * 2) / 10);
                break;
            case Elements.physical:
                dmg = Health - (int)Mathf.Floor(dmg * (10 - PhysicalRes * 2) / 10);
                break;
            case Elements.none:
                dmg = Health - (int)Mathf.Floor(dmg * (10 - PhysicalRes * 2) / 10);
                break;
        }
        Health = Mathf.Clamp(dmg, 0, MaxHealth);
        if (Health == 0) death();
    }
    void death()
    {
        tokenManager.Died(this.gameObject);
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }
}