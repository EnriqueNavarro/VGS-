using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : Ability {
    private Enemies aux;
    private float auxHP;
    [SerializeField] GameObject higher;
    [SerializeField] private GameObject resource;
    [SerializeField] private float cost;
    new public void Update()
    {
        if (resource.GetComponent<CrystalSword>().CheckShards(cost) && Input.GetKeyDown(keyBinding))
        {
           Trigger();
        }
        remainingCD = Mathf.Clamp((Cd - elapsed), 0, Cd);
        if (F)
        {
            elapsed = Time.fixedTime - Timer;
        }
        else
        {
            elapsed = Cd;
        }
    }
    public override void Activate()
    {

        higher = null;
        foreach (GameObject enemy in enemies)
        {
            if (higher == null)
            {
                higher = enemy;
            }
            else
            {
                aux = enemy.GetComponent<EnemyHealth>().TypeName;
                auxHP = enemy.GetComponent<EnemyHealth>().Health;
                switch (aux)
                {
                    case Enemies.Plate:
                        if (aux == Enemies.Cloth || aux == Enemies.Leather || aux == Enemies.Mail || aux==Enemies.Plate)
                        {
                            if (auxHP > higher.GetComponent<EnemyHealth>().Health)
                            {
                                higher = enemy;
                            }
                        }
                        break;
                    case Enemies.Mail:
                        if (aux == Enemies.Cloth || aux == Enemies.Leather|| aux==Enemies.Mail)
                        {
                            if (auxHP > higher.GetComponent<EnemyHealth>().Health)
                            {
                                higher = enemy;
                            }
                        }
                        break;
                    case Enemies.Leather:
                        if (aux == Enemies.Cloth || aux == Enemies.Leather)
                        {
                            if (auxHP > higher.GetComponent<EnemyHealth>().Health)
                            {
                                higher = enemy;
                            }
                        }
                        break;
                    case Enemies.Cloth:
                        if (aux == Enemies.Cloth)
                        {
                            if(auxHP>higher.GetComponent<EnemyHealth>().Health) {
                                higher = enemy;
                            }
                        }
                        break;
                }
                //switch ends here

            }    
        }
        //foreach ends here
        this.GetComponentInParent<Stats>().PhysicalRes += higher.GetComponent<EnemyHealth>().PhysicalRes;
        higher.GetComponent<EnemyHealth>().AddThreat(Damage*2, resource);
        Invoke("ExpireRes", Duration);
    }
    private void ExpireRes()
    {
        this.GetComponentInParent<Stats>().PhysicalRes -= higher.GetComponent<EnemyHealth>().PhysicalRes;
    }
}
