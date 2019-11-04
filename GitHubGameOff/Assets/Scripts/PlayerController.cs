using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float jumpForce;
    private float moveInput;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    void Start() {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        PlayerFacingDirection();
    }

    void FixedUpdate() {
        PlayerGrounded();
        PlayerMovement();
        PlayerJump();
    }

    // Check to see if the Physics2D game object attached to the players feet is touching any object set to the "ground" layer
    void PlayerGrounded() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    // Updates the player sprite facing direction depending on the direction its moving
    void PlayerFacingDirection() {
        if(moveInput < 0) {
            sr.flipX = true;
        }
        else if(moveInput > 0) {
            sr.flipX = false;
        }
    }

    // Handle basic player jumping
    void PlayerJump() {
        if(Input.GetKeyDown(KeyCode.UpArrow) && isGrounded) {
            anim.SetTrigger("IsJumpingStart");
            rb.velocity = Vector2.up * jumpForce;
        }
        else if(isGrounded) {
            anim.SetBool("IsJumpingEnd", false);
        }
        else {
            anim.SetBool("IsJumpingEnd", true);
        }
    }

    // Handles very basic left/right movement as well as the animations the movement is tied to.
    // In the future if we want more snappy movement we can replace GetAxis with GetAxisRaw
    void PlayerMovement() {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if(moveInput == 0) {
            anim.SetBool("IsRunning", false);
        }
        else {
            anim.SetBool("IsRunning", true);
        }
    }

}
