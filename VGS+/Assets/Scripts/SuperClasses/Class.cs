using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Class : MonoBehaviour
{
    [SerializeField] private ClassNames cName;
    [SerializeField] private Stats stats;
    [SerializeField] private GameObject[] passives = new GameObject[2]; // each class has at least 1 passive
    [SerializeField] private GameObject[] actives = new GameObject[5]; //each class has at most 4 actives
    [SerializeField] private bool stealth = false;
    [SerializeField] private bool combat;
    [SerializeField] private Text[] CDs;
    [SerializeField] private GameObject sprite;
    private bool first = true;
    [SerializeField] private int[] baseDmgs = new int[5];
    [SerializeField] private Color stealthColor;
    [SerializeField] private GameObject UI;

    public ClassNames CName
    {
        get
        {
            return cName;
        }

        set
        {
            cName = value;
        }
    }

    public Stats Stats
    {
        get
        {
            return stats;
        }

        set
        {
            stats = value;
        }
    }

    public GameObject[] Passives
    {
        get
        {
            return passives;
        }

        set
        {
            passives = value;
        }
    }

    public GameObject[] Actives
    {
        get
        {
            return actives;
        }

        set
        {
            actives = value;
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

    public bool Combat
    {
        get
        {
            return combat;
        }

        set
        {
            combat = value;
        }
    }

    public GameObject UI1
    {
        get
        {
            return UI;
        }

        set
        {
            UI = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        increaseDmg();
        first = false;

        /*for (int i = 0; i < actives.Length; i++) {
            GameObject abi = actives[i];
            //Image img = keyboard[i].GetComponent<Image>();
            //img.sprite = abi.GetComponent<Ability>().Icon;
        }*/
        UI.SetActive(true);
    }
    public void UpdateCds()
    {
        //Debug.Log("a");
        for(int i=1;i<Actives.Length;i++) {
            //Debug.Log(((int)(Actives[i].GetComponent<Ability>().remainingCD)).ToString());
            //Debug.Log(CDs.Length + " cds " + "actives-> " + Actives.Length);
            if (Actives[i].GetComponent<Ability>().remainingCD == 0) {
                CDs[i-1].text = "";
            } else {
                CDs[i-1].text = ((int)(Actives[i].GetComponent<Ability>().remainingCD)).ToString();
            }
        }
    }
    public void increaseDmg()
    {
        for (int i = 0; i < actives.Length; i++)
        {
            if (first)
            {
                baseDmgs[i] = actives[i].GetComponent<Ability>().Damage;
                //Debug.Log(actives[i].GetComponent<Ability>().Name + " deals: " + actives[i].GetComponent<Ability>().Damage);
                //Debug.Log(stats.BaseDmg);
                actives[i].GetComponent<Ability>().Damage = (int)(actives[i].GetComponent<Ability>().Damage * stats.BaseDmg);
                
            } else
            {
                actives[i].GetComponent<Ability>().Damage = (int)(baseDmgs[i] * stats.BaseDmg);
            }
            
        }
    }
    public void MakeInvisible()
    {
        sprite.GetComponent<SpriteRenderer>().color = stealthColor;
        //Debug.Log("Changing Color");
    }
    public  void MakeVisible()
    {
        sprite.GetComponent<SpriteRenderer>().color = Color.white;
    }

   
}