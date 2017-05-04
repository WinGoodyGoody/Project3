using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player Settings")]
    public float maxSpeed = 10f; //Fastest the player can travel
    public float jumpForce = 400f; //Amount of force for jump
    [Range(0, 1)]
    public float crouchSpeed = 0.30f; //Speed applied
    public bool airControl = false; //Allow steering while in air
    public LayerMask whatIsGround; // A layer mask to indicate ground

    private bool facingRight = true; //Which way player is facing
    private Transform groundCheck;
    private float groundRadius = 0.2f;
    private bool grounded = false; //Checking if we are grounded
    private Transform ceilingCheck;
    private float ceilingRadius = 0.1f;
    private Animator anim;
    private Rigidbody2D rigid;





	// Use this for initialization
	void Awake () { 
        // Set up all our references
        groundCheck = transform.Find("GroundCheck");
        ceilingCheck = transform.Find("CeilingCheck");
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
		
	}
	
	// Fixed Update is called at specific frame rate
	void FixedUpdate () {
        //Preforming ground check using Physics2D
        grounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            whatIsGround);



	}
    void Flip()
    {
        //Switch the way the player is facing
        facingRight = !facingRight;

        //Invert the scale of the player on x
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Inverts x
        transform.localScale = scale;

     }
    public void Move(float move, bool crouch, bool jump)
    {
        //If crouching, check to see if we can stand up
        if (crouch == false)
        {
            // Check to see if we hit ceiling
            if (Physics2D.OverlapCircle(ceilingCheck.position,
                ceilingRadius,
                whatIsGround))
            {
                crouch = true;
            }
        }
        //Only control player if grounded or airControl is on 
        if (grounded || airControl)
        {
            //Reduce the speed if we are crouching
            // ternary operation
            move = (crouch ? move * crouchSpeed : move);

            //Move the character
            rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);

            // If the input is moving player right
            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }
        }
        if (grounded && jump)
        {
            grounded = false;
            rigid.AddForce(new Vector2(0, jumpForce));
        }
    }
}

