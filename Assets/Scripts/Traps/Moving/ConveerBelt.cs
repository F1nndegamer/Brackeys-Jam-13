using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ConveyorBelt : MonoBehaviour
{
    public enum Rotation { Right, Left, Stop }

    [SerializeField] public float speed = 1;
    public Rotation rotation;

    private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D objRb = collision.rigidbody;
        if (objRb != null)
        {
            Vector2 moveDirection = rotation == Rotation.Left ? Vector2.left : Vector2.right;

            if (rotation != Rotation.Stop)
            {
                objRb.linearVelocity = new Vector2(moveDirection.x * speed, objRb.linearVelocity.y);
            }
        }
    }
}
