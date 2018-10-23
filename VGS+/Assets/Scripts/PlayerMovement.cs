using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float speedModifier = 1;
    private float movement;
    private bool facingRight;
    public Animator animator;
    private Vector3 lastPos;
    public Vector3 deltaPos;

    public float SpeedModifier
    {
        get
        {
            return speedModifier;
        }

        set
        {
            speedModifier = value;
        }
    }
    
    private void FixedUpdate()
    {
        deltaPos = transform.position - lastPos;
        lastPos = transform.position;
        int size = this.GetComponent<Class>().Actives.Length;
        for(int i=0;i<size;i++) {
            this.GetComponent<Class>().Actives[i].GetComponent<Ability>().Movement = deltaPos;
        }
        if (Input.GetAxis("Horizontal") < 0 && facingRight) {
            flip();
        }

        if (Input.GetAxis("Horizontal") > 0 && !facingRight)
        {
            flip();
        }
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        this.transform.position += velocity*SpeedModifier;

        movement = (velocity.x + velocity.z) * SpeedModifier;
        animator.SetFloat("Speed", Mathf.Abs(movement));
      
    }

    void flip() {
        facingRight = !facingRight;
        transform.Rotate(Vector3.forward * 180);
    }
}
