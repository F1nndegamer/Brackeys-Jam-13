using UnityEngine;

public class TreeClass : MonoBehaviour
{
    public int health = 3; // Default health value, can be adjusted per tree
    private Rigidbody2D rb;
    private bool isFalling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.isKinematic = true; // Keep it static until it falls
        }
    }

    private void Update()
    {
        if (health <= 0 && !isFalling)
        {
            ChopDown();
        }
    }

    private void ChopDown()
    {
        SFXManager.Instance.PlayTreeFallSound();
        isFalling = true;
        rb.isKinematic = false; // Enable physics
        rb.AddTorque(10f, ForceMode2D.Impulse); // Apply a random torque for a falling effecta
    }
}
