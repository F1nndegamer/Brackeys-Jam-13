using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    private float moveInput;
    private bool isSprinting;

    [Header("Jump Settings")]
    public float jumpForce = 12f;
    public int maxJumps = 2;
    private int jumpCount = 0;
    private bool isGrounded;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private bool isDashing;
    private bool canDash = true;
    private int facingDirection = 1; // 1 = right, -1 = left

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get horizontal input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Sprinting
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps || Input.GetKeyDown(KeyCode.W) && jumpCount < maxJumps || Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < maxJumps)
        {
            Jump();
        }

        // Dashing
        if (Input.GetKeyDown(KeyCode.LeftControl) && canDash && !isDashing)
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
            transform.localScale = new Vector3(facingDirection, transform.localScale.y, transform.localScale.z);
        }
    }

    void FixedUpdate()
    {
        // Ground detection
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Reset jump count on landing
        if (isGrounded && !wasGrounded)
        {
            jumpCount = 0;
        }

        // Move player (disable movement while dashing)
        if (!isDashing)
        {
            float speed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
            rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
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

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(facingDirection * dashSpeed, 0f); // Dash in facing direction
    
        SFXManager.Instance.PlayDashSound();
        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(1f); // Dash cooldown
        canDash = true;
        SFXManager.Instance.PlayDashRecoverSound();
    }
}
