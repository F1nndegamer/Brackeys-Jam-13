using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class Fan : Traps
{
    [SerializeField] private bool isWorking;
    [SerializeField] private float upwardForce;
    [SerializeField] private float rightwardForce;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isWorking)
        {
            Rigidbody2D other = collision.GetComponent<Rigidbody2D>();
            other.AddForce(Vector2.up * upwardForce * other.mass, ForceMode2D.Impulse);
            other.AddForce(Vector2.up * rightwardForce * other.mass, ForceMode2D.Impulse);
        }
    }
    public override void WorkTrap(GameObject player)
    {
        base.WorkTrap(player);
        isWorking = isWorking == true ? false : true;
    }
}
