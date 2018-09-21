using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowStep : Ability {
    
    [SerializeField] Vector3 pos;
    [SerializeField] GameObject lower;
    [SerializeField] float attackSpeedModifier;
    [SerializeField] GameObject attackGameObject;
    [SerializeField] private GameObject player;
    private Enemies aux;
	// Use this for initialization
	void Start () {
        Col.transform.localScale = new Vector3(Range, 2, Range);
	}
    

    public override void Activate()
    {
        
        lower = null;
        foreach (GameObject enemy in enemies)
        {
            if (lower == null) {
                lower = enemy;
            } else {
                aux = enemy.GetComponent<EnemyHealth>().TypeName;
                switch (aux) {
                    case Enemies.Plate:
                        if (aux == Enemies.Plate) lower = enemy;
                        break;
                    case Enemies.Mail:
                        if (aux == Enemies.Plate || aux == Enemies.Mail) lower = enemy;
                        break;
                    case Enemies.Leather:
                        if (aux == Enemies.Plate || aux == Enemies.Mail || aux== Enemies.Leather) lower = enemy;
                        break;
                    case Enemies.Cloth:
                        lower = enemy;
                        break;
                }
            }
            if(lower!=null) {
                pos = lower.transform.position;
                player.transform.position = pos;
                lower.GetComponent<EnemyHealth>().damage(Damage,DmgType);
                attackGameObject.GetComponent<Attack>().attackSpeed(attackSpeedModifier, Duration);
            }
            
        }
    }
}
