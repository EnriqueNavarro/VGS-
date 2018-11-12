using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTheThickOfIt : Ability {

    [SerializeField] private GameObject resource;
    [SerializeField] private float cost;
    [SerializeField] private bool combat;
    [SerializeField] private GameObject autoAttack;
    private bool c2;//to check when entered combat
    [SerializeField] private float timerT;
    private float startingTime;
    [SerializeField] private bool[] tiers= new bool[7];
    [SerializeField] private bool tierChecker=false;
    private float exitTime;
    private bool once=false;
    [SerializeField] private float t1AttackSpeed;
    [SerializeField] private float t2MoveSpeed;
    [SerializeField] private float t3BaseDmg;
    [SerializeField] private float t4BaseDmg;
    [SerializeField] private bool t5SlowImmunity;//if true cant be slowed
    [SerializeField] private float t6CritChance;
    [SerializeField] private float tnCritDamage;//if crit chance reaches 100% from stacks of t6, increase crit damage
    private void checker()
    {
        tierChecker = true;
    }
    new public void Update()
    {
        combat = resource.GetComponent<CrystalSword>().Combat;
        if (!combat && once)
        {
            exitTime = Time.fixedTime;
            once = false;
        }
        if (combat)
        {
            once = true;
            exitTime = Time.fixedTime;
        }
        if(combat && !c2)
        {
            startingTime = Time.fixedTime;
            CancelInvoke();
            InvokeRepeating("checker", 5f, 5f);
        }
        if (Time.fixedTime - exitTime < 5)
        {
            timerT = Time.fixedTime - startingTime;
            if (timerT >= 5  && timerT < 10 && tierChecker) Tier1();
            if (timerT >= 10 && timerT < 15 && tierChecker) Tier2();
            if (timerT >= 15 && timerT < 20 && tierChecker) Tier3();
            if (timerT >= 20 && timerT < 25 && tierChecker) Tier4();
            if (timerT >= 25 && timerT < 30 && tierChecker) Tier5();
            if (timerT >= 30 && tierChecker) Tier6();
        } else
        {
            timerT = 0;
            deActivate();
        }
        c2 = combat;
    }
    void deActivate()
    {
        if (tiers[0])
        {
            autoAttack.GetComponent<CrystalSwordAttack>().Cd /= t1AttackSpeed;
            tiers[0] = false;
        }
        if (tiers[1])
        {
            resource.GetComponent<PlayerMovement>().SpeedModifier -= t2MoveSpeed;
            tiers[1] = false;
        }
        if (tiers[2])
        {
            resource.GetComponent<Stats>().BaseDmg -= t3BaseDmg;
            resource.GetComponent<CrystalSword>().increaseDmg();
            tiers[2] = false;
        }
        if (tiers[4])
        {
            resource.GetComponent<Stats>().SlowImmunity = false;
            tiers[4] = false;
        }
        if (tiers[3])
        {
            resource.GetComponent<Stats>().BaseDmg -= t4BaseDmg;
            resource.GetComponent<CrystalSword>().increaseDmg();
            tiers[3] = false;
        }
        if (tiers[5])
        {
            resource.GetComponent<Stats>().CritChance -= t6CritChance;
            tiers[5] = false;
        }
        if (tiers[6])
        {
            resource.GetComponent<Stats>().CritDamage -= tnCritDamage;
            tiers[6] = false;
        }  
    }
    void Tier1()
    {
        if (tiers[0])
        {
            Tier2();
            return;
        }
        autoAttack.GetComponent<CrystalSwordAttack>().Cd *= t1AttackSpeed;
        tiers[0]=true;
        tierChecker = false;
    }
    void Tier2()
    {
        if (tiers[1])
        {
            Tier3();
            return;
        }
        resource.GetComponent<PlayerMovement>().SpeedModifier += t2MoveSpeed;
        tiers[1]=true;
        tierChecker = false;
    }
    void Tier3()
    {
        if (tiers[2])
        {
            Tier4();
            return;
        }
        resource.GetComponent<Stats>().BaseDmg += t3BaseDmg;
        resource.GetComponent<CrystalSword>().increaseDmg();
        tiers[2]=true;
        tierChecker = false;
    }
    void Tier4()
    {
        if (tiers[3])
        {
            Tier5();
            return;
        }
        resource.GetComponent<Stats>().BaseDmg += t4BaseDmg;
        resource.GetComponent<CrystalSword>().increaseDmg();
        tiers[3]=true;
        tierChecker = false;
    }
    void Tier5()
    {
        if (tiers[4])
        {
            Tier6();
            return;
        }
        resource.GetComponent<Stats>().SlowImmunity = true;
        tiers[4] = true;
        tierChecker = false;
    }
    void Tier6()
    {
        if (tiers[5])
        {
            Tiern();
            return;
        }
        resource.GetComponent<Stats>().CritChance += t6CritChance;
        tiers[5] = true;
        tierChecker = false;
    }
    void Tiern()
    {
        if (tiers[6]) return;
        resource.GetComponent<Stats>().CritDamage += tnCritDamage;
        tiers[6]=true;
        tierChecker = false;
    }
    public override void Activate()
    {

    }

}
