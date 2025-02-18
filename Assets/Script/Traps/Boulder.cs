using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Boulder : Traps
{
    public float boulderMove;
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<Rigidbody2D>().linearVelocityX != 0.01f || GetComponent<Rigidbody2D>().linearVelocityY != 0.01f)
        {
            base.OnCollisionEnter2D(collision);
        }
    }
    private void Update()
    {
        if (isActive && GameManager.instance.isTakenTreasure)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().AddForceX(boulderMove, ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}