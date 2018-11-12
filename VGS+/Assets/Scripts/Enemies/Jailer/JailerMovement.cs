using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailerMovement : MonoBehaviour {
    [SerializeField] private bool combat;
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 lastKnownPos;
    [SerializeField] float maxRange;
    [SerializeField] float maxDistanceDelta;
    [SerializeField] float minDistance;
    [SerializeField] private LayerMask lm;
    [SerializeField] private MovementType current;

    public MovementType Current
    {
        get
        {
            return current;
        }

        set
        {
            current = value;
        }
    }


    // Update is called once per frame
    void MoveToPlayer () {
        target = this.GetComponent<EnemyHealth>().Attacker;
        if(target!=null){
            lastKnownPos = target.transform.position;
            if (Vector3.Distance(lastKnownPos, transform.position) < minDistance) lastKnownPos = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, lastKnownPos, maxDistanceDelta);
        }
    }
    void Teleport() {
        if (this.GetComponent<JailerControler>().Busy) return;
        target = this.GetComponent<EnemyHealth>().Attacker;
        if(target!=null) transform.position = new Vector3(target.transform.position.x-15, target.transform.position.y, target.transform.position.z);
        Current = MovementType.Halt;
        Invoke("Name", 1);
    }
    void Name() {
        Current= MovementType.MoveToPlayer;
    }
    void Center() {
        transform.position = Vector3.zero;
    }
    void Update()
    {
        switch(Current) {
            case MovementType.MoveToPlayer:
                MoveToPlayer();
                break;
            case MovementType.teleport:
                Teleport();
                break;
            case MovementType.Execute:
                Center();
                break;
        }
    }
}
public enum MovementType {
    MoveToPlayer,
    Halt,
    teleport,
    Execute
};
