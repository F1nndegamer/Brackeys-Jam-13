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

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private bool isDashing;
    private bool canDash = true;
    private int facingDirection = 1; // 1 = right, -1 = left
    public float dashCooldown = 1.1f;
    private float lastDashTime = -Mathf.Infinity;
    private TrailRenderer tr;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        if(transform.position.y < -40)
        {
            TempRespawn.instance.respawm();
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
        // Ground detection
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Reset jump count on landing
        if (isGrounded && !wasGrounded)
        {
            jumpCount = 0;
            animator.SetBool("isJumping", false);
        }
        if(!isGrounded && jumpCount == 0)
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
        
        if (isGrounded && Mathf.Abs(moveInput) > 0.1f && !isDashing)
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

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
         lastDashTime = Time.time;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(facingDirection * dashSpeed, 0f); // Dash in facing direction
    
        SFXManager.Instance.PlayDashSound();
        if(tr != null)
        {
        tr.emitting = true;
        }
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(0.5f); //Play the sound earlier
        SFXManager.Instance.PlayDashRecoverSound();
        yield return new WaitForSeconds(0.6f); // Dash cooldown
        canDash = true;
        if(tr != null)
        {
        tr.emitting = false;
        }
    }
    public float GetDashCooldownRemaining()
    {
        return Mathf.Max(0, (lastDashTime + dashCooldown) - Time.time);
    }
}
