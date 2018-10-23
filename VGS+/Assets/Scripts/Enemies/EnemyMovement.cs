using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] private bool combat;
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 lastKnownPos;
    [SerializeField] float maxRange;
    [SerializeField] float maxDistanceDelta;
    [SerializeField] float minDistance;
    [SerializeField] private LayerMask lm;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        combat = this.GetComponent<EnemyHealth>().combat;
        if (this.GetComponent<EnemyHealth>().Attacker != null)
        {
            target = this.GetComponent<EnemyHealth>().Attacker;
        } else
        {
            target = null;
        }
	}
    private void FixedUpdate()
    {
        if (combat)
        {
            RaycastHit hit;
            if (Vector3.Distance(transform.position, target.transform.position) < maxRange)
            {
                if (Physics.Raycast(transform.position, (target.transform.position - transform.position), out hit, maxRange,
                    lm))
                {
                    if (hit.transform == target.transform)
                    {
                        if(Vector2.Distance(new Vector2(transform.position.x,transform.position.z),new Vector2(target.transform.position.x, target.transform.position.z))<minDistance)
                        {
                            //lastKnownPos = transform.position;
                        }
                        else
                        {
                            lastKnownPos = target.transform.position;
                        }
                    }
                    if (Input.GetKeyDown("m")) Debug.Log("Enemy position: " + transform.position + " Player position: " + target.transform.position + " Distance: " + Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.transform.position.x, target.transform.position.z))
                  + " Last known pos:" + lastKnownPos + " seen" + (hit.transform == target.transform));
                }
            }
            if (target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, lastKnownPos, maxDistanceDelta);
                //Debug.Log("Going to " + lastKnownPos +"from"+transform.position+ " distance=" + Vector3.Distance(transform.position, target.transform.position));
            }
            
        }
        
    }
}
