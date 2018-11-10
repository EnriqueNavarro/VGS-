using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportJailer : BossAbility {

    [SerializeField] float maxDistance;//227
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
            activate = (Vector3.Distance(Target.transform.position, transform.position) > maxDistance);
        }
        
        if (!InProcess && activate)
        {
            Trigger();
            activate = false;
            this.GetComponentInParent<JailerMovement>().Current = MovementType.Halt;
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
