using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ConveerBelt : MonoBehaviour
{
    public enum Rotation { Right, Left, Stop }
    
    private Rigidbody2D rb2D;
    [SerializeField] private float speed = 1;
    public Rotation rotation;
    public bool activateswitch;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        
        // Ensure the conveyor belt has a trigger collider
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }

    private void FixedUpdate()
    {
        if (!activateswitch) return;

        Vector2 movement = Vector2.zero;

        if (rotation == Rotation.Right)
        {
            movement = Vector2.right * speed;
        }
        else if (rotation == Rotation.Left)
        {
            movement = Vector2.left * speed;
        }

        rb2D.MovePosition(rb2D.position + movement * Time.fixedDeltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D objRb = other.attachedRigidbody;
        if (objRb != null)
        {
            if (rotation == Rotation.Right)
            {
                objRb.linearVelocity = new Vector2(speed, objRb.linearVelocity.y);
            }
            else if (rotation == Rotation.Left)
            {
                objRb.linearVelocity = new Vector2(-speed, objRb.linearVelocity.y);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D objRb = other.attachedRigidbody;
        if (objRb != null)
        {
            objRb.linearVelocity = new Vector2(0, objRb.linearVelocity.y); // Reset horizontal velocity when leaving the belt
        }
    }
}
