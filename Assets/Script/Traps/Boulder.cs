using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Boulder : Traps
{
    public Vector2 boulderMove;

    private void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<Rigidbody2D>().linearVelocityX != 0.01f || GetComponent<Rigidbody2D>().linearVelocityY != 0.01f)
        {
            base.OnCollisionEnter2D(collision);
        }
    }
    public override void WorkTrap(GameObject player)
    {
        base.WorkTrap(player);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}