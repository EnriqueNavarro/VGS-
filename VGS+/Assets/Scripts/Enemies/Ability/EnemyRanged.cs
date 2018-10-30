using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : EnemyAbility {
    [SerializeField] private GameObject missile;
    [SerializeField] private float missileSpeed;
    [SerializeField] private float missileRange;
    private GameObject tracker;
    public override void Activate()
    {
        Invoke("Shoot", Delay);
        Invoke("TurnWarningOn", (Delay-0.5f));
        //Debug.Log("shoot");
    }
    private void Shoot()
    {
        tracker = Instantiate(missile, transform.position, Quaternion.Euler(90, 90, 0));
        tracker.GetComponent<Missile>().Target = Target;
        tracker.GetComponent<Missile>().Range = missileRange;
        tracker.GetComponent<Missile>().Speed = missileSpeed;
        tracker.GetComponent<Missile>().DmgType = DmgType;
        tracker.GetComponent<Missile>().Damage = Damage;
    }
}
