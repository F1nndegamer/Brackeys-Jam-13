using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Boulder : Traps
{
    public float boulderMove;
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<Rigidbody2D>().linearVelocityX != 0 || GetComponent<Rigidbody2D>().linearVelocityY != 0)
        {
            base.OnCollisionEnter2D(collision);
        }
    }
    private void Update()
    {
        if (isActive && GameManager.instance.isTakenTreasure)
        {
            GetComponent<Rigidbody2D>().linearVelocityX = boulderMove;
        }
    }
}