using System;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    private float moveInput;

    [Header("Jump Settings")]
    public float jumpForce = 12f;
    private int jumpCount = 0;
    private bool isGrounded;
    private bool isTree;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private bool isDashing;
    private bool canDash = true;
    private int facingDirection = 1; // 1 = right, -1 = left
    public float dashCooldown = 1.1f;
    private float lastDashTime = -Mathf.Infinity;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public LayerMask treeLayer;

    private Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(transform.position.y < -40)
        {
            GetComponent<PlayerRespawn>().Respawn();
        }
        // Get horizontal input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2 || Input.GetKeyDown(KeyCode.W) && jumpCount < 2 || Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && canDash && !isDashing)
        {
            StartCoroutine(Dash());
        }

        // Update facing direction
        if (moveInput > 0)
        {
            facingDirection = 1;
        }
        else if (moveInput < 0)
        {
            facingDirection = -1;
        }

        // Flip player sprite
        if (moveInput != 0)
        {
            if(transform.localScale.x < 0 && facingDirection == 1)
            {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            if(transform.localScale.x > 0 && facingDirection == -1)
            {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    void FixedUpdate()
    {
        animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x)); 
        animator.SetFloat("yVelocity", rb.linearVelocity.y); 
        // Ground detection
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isTree = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, treeLayer);

        // Reset jump count on landing
        if (isGrounded || isTree)
        {
            jumpCount = 0;
            animator.SetBool("isJumping", false);
        }
        
        if((!isGrounded || isTree) && jumpCount == 0)
        {
            jumpCount = 1;
        }

        // Move player (disable movement while dashing)
        if (!isDashing)
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }

        /* --- Footstep SFX Trigger ---
         Only play footsteps when:
         1. The player is grounded.
         2. There is horizontal movement.
         3. The player is not dashing. */
        
        if ((isGrounded || isTree) && Mathf.Abs(moveInput) > 0.1f && !isDashing)
        {
            SFXManager.Instance.StartFootsteps();
        }
        else
        {
            SFXManager.Instance.StopFootsteps();
        }
        
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        jumpCount++;
        animator.SetBool("isJumping", true);
        if(jumpCount == 1)
        {
             SFXManager.Instance.PlayJumpSound();
             Debug.Log("jump 1");
        }
        else if(jumpCount > 1)
        {
            SFXManager.Instance.PlayDubbleJumpSound();
            Debug.Log("jump 2");
        }
    }
    private void OTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        
        
    }
    IEnumerator Dash()
    {
        animator.SetBool("isDashing", true);
        canDash = false;
        isDashing = true;
         lastDashTime = Time.time;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(facingDirection * dashSpeed, 0f); // Dash in facing direction
    
        SFXManager.Instance.PlayDashSound();
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = originalGravity;
        isDashing = false;
        
        animator.SetBool("isDashing", false);
        yield return new WaitForSeconds(0.5f); //Play the sound earlier
        SFXManager.Instance.PlayDashRecoverSound();
        yield return new WaitForSeconds(0.6f); // Dash cooldown
        canDash = true;
    }
    public float GetDashCooldownRemaining()
    {
        float fullDashCooldown = dashDuration + 0.5f + 0.6f; // total dash recovery time
        return canDash ? 0f : Mathf.Max(0, (lastDashTime + fullDashCooldown) - Time.time);
    }
}