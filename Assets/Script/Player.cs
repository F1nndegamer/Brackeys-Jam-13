using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float jumpForce = 12f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;

    [Header("Ground Check")]
    public LayerMask groundLayer; // Assign this to "Ground" in Unity
    private bool isGrounded;

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private int jumpCount = 0;
    private bool isDashing = false;
    private float dashTime;
    private float defaultGravity;
    private float lastDashTime;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        defaultGravity = rb.gravityScale;
    }

    void Update()
    {
        HandleGroundCheck();
        HandleMovement();
        HandleJump();
        HandleDash();
    }

    void HandleGroundCheck()
    {
        float extraHeight = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down * (col.bounds.extents.y), Vector2.down, extraHeight, groundLayer);
        isGrounded = hit.collider != null;

        if (isGrounded)
            jumpCount = 0;
    }

    void HandleMovement()
    {
        if (isDashing) return;

        float moveInput = Input.GetAxis("Horizontal");
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed; // Sprinting

        rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);

        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
        }
    }

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time - lastDashTime < 0.3f && canDash) // Double-tap Shift to dash
        {
            isDashing = true;
            dashTime = dashDuration;
            rb.linearVelocity = new Vector2(transform.localScale.x * dashSpeed, 0);
            rb.gravityScale = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            lastDashTime = Time.time;
        }

        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                isDashing = false;
                rb.gravityScale = defaultGravity;
            }
        }

        if (isGrounded) 
        {
            canDash = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.down * col.bounds.extents.y, 
                        transform.position + Vector3.down * (col.bounds.extents.y + 0.1f));
    }
}
