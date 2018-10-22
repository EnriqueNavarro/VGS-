using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float speedModifier = 1;
    private float movement;
    private bool facingRight;
    public Animator animator;
    

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
    
    private void Update()
    {
        

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
