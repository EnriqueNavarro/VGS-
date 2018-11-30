using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportJailer : BossAbility {

    [SerializeField] float maxDistance;//227
    public float distance;
    void Start()
    {
        
    }
    new void Update()
    {
        int i = User.GetComponent<EnemyHealth>().Number;
        if (User.GetComponent<EnemyHealth>().combat)
        {
            Tuple = User.GetComponent<EnemyHealth>().Threat[i];
            Target = Tuple.player;
            distance = Vector3.Distance(Target.transform.position, transform.position);
            activate = (distance>maxDistance &&  !this.GetComponentInParent<JailerControler>().Busy);
        }
        
        if (!InProcess && activate)
        {
            Trigger();
            activate = false;
        }
    }
    public override void Activate()
    {
        Invoke("Teleport", Delay);
    }
    void Teleport()
    {
        this.GetComponentInParent<JailerMovement>().Current = MovementType.teleport;
        InProcess = false;
        
    }
}
