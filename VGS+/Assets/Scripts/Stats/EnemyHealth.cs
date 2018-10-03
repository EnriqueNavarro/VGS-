using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Use this for initialization
    void Start()
    {
        if (lvl == 0) lvl = 1;
        BaseDmg = 1;
        switch (TypeName)
        {
            case Enemies.Plate:
                health = 100;
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
                health = 85;
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
                health = 70;
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
                health = 65;
                maxHealth = health;
                physicalRes = 0;
                baseMagicRes = 3;
                fireRes = baseMagicRes;
                frostRes = baseMagicRes;
                LightRes = baseMagicRes;
                ShadowRes = baseMagicRes;
                poisonRes = baseMagicRes;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void damage(int dmg, GameObject attacker)
    {
        dmg = health - (int)Mathf.Floor(dmg * (10 - physicalRes * 2) / 10);
        Health = Mathf.Clamp(dmg, 0, MaxHealth);
        if (Health == 0) death();
    }
    public void damage(int dmg, Elements element, GameObject attacker)
    {
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
        }
        Health = Mathf.Clamp(dmg, 0, MaxHealth);
        if (Health == 0) death();
    }
    public void damage(int dmg, float critChance, float CritDamage, GameObject attacker)
    {
        bool crit = (Random.Range(0, 1) < critChance);
        if (crit) dmg = ((int)(dmg * CritDamage));
        dmg = health - (int)Mathf.Floor(dmg * (10 - physicalRes * 2) / 10);
        Health = Mathf.Clamp(dmg, 0, MaxHealth);
        if (Health == 0) death();
    }
    public void damage(int dmg, Elements element, float critChance, float CritDamage, GameObject attacker)
    {
        bool crit = (Random.Range(0, 1) < critChance);
        if (crit) dmg = ((int)(dmg * CritDamage));
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
        }
        Health = Mathf.Clamp(dmg, 0, MaxHealth);
        if (Health == 0) death();
    }
    void death()
    {
        //to decide
    }
}