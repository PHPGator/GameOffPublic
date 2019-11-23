using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float jumpForce;
    public float maxSpeed;
    private float moveInput;
    private int face = 1;

    public float fallModifier = 1f;
    public float jumpModifier = 4f;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;

    private bool isGrounded;
    private Vector2 direction;
    
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private PlayerHealth pHealth;
    [SerializeField] private float playerWeaponDamage = 50f;
    [SerializeField] private float playerWeaponRange = 1f;

    void Start() {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        pHealth = GetComponent<PlayerHealth>();
        playerWeaponDamage = 10f;
        direction = new Vector2(1,0);
    }

    void Update() {
        PlayerFacingDirection();
        pHealth.checkHealth();
        PlayerDeath();
        PlayerDescent();
    }

    void FixedUpdate() {
        PlayerGrounded();
        PlayerMovement();
        PlayerAttck();
        PlayerJump();
    }

    // Check to see if the Physics2D game object attached to the players feet is touching any object set to the "ground" layer
    void PlayerGrounded() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    // Simple function to trigger very basic swing attack.
    void PlayerAttck() {
		var canAttack = Input.GetKeyDown(KeyCode.LeftControl);
		if(canAttack) {
			anim.SetTrigger("New Trigger");
            GameObject enemyObj = checkForEnemyHit();
            if(enemyObj != null)
            {
                enemyObj.GetComponent<EnemyHealth>().decreaseHealth(playerWeaponDamage);
            }
		}
    }

    // Updates the player sprite facing direction depending on the direction its moving
    void PlayerFacingDirection() {
        if(moveInput < 0) {
            sr.flipX = true;
            direction.x= -1;
        }
        else if(moveInput > 0) {
            sr.flipX = false;
            direction.x = 1;
        }
    }

    // Handle basic player jumping
    void PlayerJump() {
        if(isGrounded && Input.GetButton("Jump")) {
            anim.SetTrigger("IsJumpingStart");
            rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
            //rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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
        //rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        rb.AddRelativeForce(new Vector2(moveInput, 0f), ForceMode2D.Impulse);

        if (moveInput == 0)
        {

            rb.AddRelativeForce(new Vector2((-rb.velocity.x/3), 0f), ForceMode2D.Impulse);
        }
        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {

            rb.velocity *= 0.9f;
        }
        if (moveInput == 0) {
            anim.SetBool("IsRunning", false);
        }
        else {
            anim.SetBool("IsRunning", true);
        }
    }

    public void PlayerDeath()
    {
        if(!pHealth.isAlive)
            anim.SetBool("IsDead", true);
    }

    /** Combat Damage **/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collObj = collision.gameObject;
        if (collObj.CompareTag("Enemy"))
        {
            Debug.Log("Ran into enemy damage");
            pHealth.decreaseHealth(25f);
        }

    }

    private GameObject checkForEnemyHit()
    {
        RaycastHit2D hit;
        float colliderRadius = gameObject.GetComponent<BoxCollider2D>().size.x / 2;
        var rayStartingPos = transform.position + (new Vector3(colliderRadius + 1f, 0, 0) * direction.x);

        //for(int i =0; i < 24; i++)
        // {
        hit = Physics2D.Raycast(rayStartingPos, direction, playerWeaponRange);
        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject != null && hitObject.CompareTag("Enemy"))
            {
                Debug.Log("Player: Hit Enemy");
                Debug.DrawRay(rayStartingPos, direction * hit.distance, Color.red);
                return hitObject;
            }
            else if (hitObject != null && hitObject.CompareTag("Player"))
            {
                Debug.Log("hitting something");
                Debug.Log("Vec2 dir: " + direction);
            }
            
        }
        return null;
    }
    private void PlayerDescent()
    {

        if (rb.velocity.y < 0)
        {

            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallModifier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {

            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpModifier - 1) * Time.deltaTime;
        }
    }

}
