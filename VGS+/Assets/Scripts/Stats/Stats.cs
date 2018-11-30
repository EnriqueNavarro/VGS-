using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] private float critChance;
    [SerializeField] private float critDamage;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private bool imunity;
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
    [SerializeField] private float baseDmg; 
    [SerializeField] private bool stealth = false;
    [SerializeField] private bool slowImmunity;
    [SerializeField] private GameObject EndGameScreen;

    public Slider healthSlider;

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

    public ClassNames ClassName
    {
        get
        {
            return className;
        }

        set
        {
            className = value;
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

    public int Xp
    {
        get
        {
            return xp;
        }

        set
        {
            xp = value;
        }
    }

    public int XpRequiered
    {
        get
        {
            return xpRequiered;
        }

        set
        {
            xpRequiered = value;
        }
    }

    public float BaseDmg
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

    public float CritChance
    {
        get
        {
            return critChance;
        }

        set
        {
            critChance = value;
        }
    }

    public float CritDamage
    {
        get
        {
            return critDamage;
        }

        set
        {
            critDamage = value;
        }
    }

    public bool SlowImmunity
    {
        get
        {
            return slowImmunity;
        }

        set
        {
            slowImmunity = value;
        }
    }

    public bool Imunity
    {
        get
        {
            return imunity;
        }

        set
        {
            imunity = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        GetComponent<PlayerMovement>().SpeedModifier = Speed+1.25f;
        BaseDmg = 1;
        critDamage = 2;
        switch (ClassName)
        {
            case ClassNames.ShadowDancer:
                Health = 80;
                MaxHealth = Health;
                PhysicalRes = 1;
                BaseMagicRes = 2;
                FireRes = BaseMagicRes - 1;
                FrostRes = BaseMagicRes + 1;
                LightRes = BaseMagicRes - 1;
                ShadowRes = BaseMagicRes + 1;
                PoisonRes = BaseMagicRes + 1;
                critChance = 0.25f;
                break;
            case ClassNames.CrystalSword:
                Health = 90;
                MaxHealth = Health;
                PhysicalRes = 2;
                BaseMagicRes = 1;
                FireRes = BaseMagicRes + 1;
                FrostRes = BaseMagicRes + 1;
                LightRes = BaseMagicRes;
                ShadowRes = BaseMagicRes;
                PoisonRes = BaseMagicRes;
                critChance = 0.2f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = (Health * 100) / MaxHealth;        
    }
    private void EndImmunity()
    {
        Imunity = false;
    }
    public void StartImunnity(float t)
    {
        Imunity = true;
        Invoke("EndImmunity", t);
    }
    public void damage(int dmg)
    {
        dmg = Health - (int)Mathf.Floor(dmg * (10 - PhysicalRes * 2) / 10);
        if (!imunity)
        {
            Health = Mathf.Clamp(dmg, 0, MaxHealth);
            healthSlider.value = Health;
            if (Health == 0) death();
        }
        
    }

    public void Potion() {

        int amount = (MaxHealth / 20);
        print(amount);
        Health = Health + amount;
        healthSlider.value = Health;
    }
    public void Heal(int amount, Elements element) {
        switch (element)
        {
            case Elements.fire:
                amount = Health + (int)Mathf.Floor(amount * (10 + FireRes * 2) / 10);
                break;
            case Elements.frost:
                amount = Health + (int)Mathf.Floor(amount * (10 + FrostRes * 2) / 10);
                break;
            case Elements.poison:
                amount = Health + (int)Mathf.Floor(amount * (10 + PoisonRes * 2) / 10);
                break;
            case Elements.light:
                amount = Health + (int)Mathf.Floor(amount * (10 + LightRes * 2) / 10);
                break;
            case Elements.shadow:
                amount = Health + (int)Mathf.Floor(amount * (10 + ShadowRes * 2) / 10);
                break;
            case Elements.physical:
                amount = Health + (int)Mathf.Floor(amount * (10 + PhysicalRes * 2) / 10);
                break;
            case Elements.none:
                amount = Health + (int)Mathf.Floor(amount * (10 + PhysicalRes * 2) / 10);
                break;
        }
        Health = Mathf.Clamp(amount, 0, MaxHealth);
        healthSlider.value = Health;
    }
    public void damage(int dmg, Elements element)
    {
        dmg = (int)(dmg * baseDmg);
        switch (element)
        {
            case Elements.fire:
                dmg = Health - (int)Mathf.Floor(dmg * (10 - FireRes * 2) / 10);
                break;
            case Elements.frost:
                dmg = Health - (int)Mathf.Floor(dmg * (10 - FrostRes * 2) / 10);
                break;
            case Elements.poison:
                dmg = Health - (int)Mathf.Floor(dmg * (10 - PoisonRes * 2) / 10);
                break;
            case Elements.light:
                dmg = Health - (int)Mathf.Floor(dmg * (10 - LightRes * 2) / 10);
                break;
            case Elements.shadow:
                dmg = Health - (int)Mathf.Floor(dmg * (10 - ShadowRes * 2) / 10);
                break;
            case Elements.physical:
                dmg = Health - (int)Mathf.Floor(dmg * (10 - PhysicalRes * 2) / 10);
                break;
            case Elements.none:
                dmg = Health - (int)Mathf.Floor(dmg * (10 - PhysicalRes * 2) / 10);
                break;
        }
        if (!imunity)
        {
            Health = Mathf.Clamp(dmg, 0, MaxHealth);
            healthSlider.value = Health;
            if (Health == 0) death();
        }
    }
    void death()
    {
        //to decide
        EndGameScreen.SetActive(true);
        Time.timeScale=0;
    }
}