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
    }

    private void FixedUpdate()
    {
        if (rotation == Rotation.Left)
        {
            Vector2 pos = rb2D.position;
            rb2D.position += Vector2.right * speed * Time.fixedDeltaTime;
            rb2D.MovePosition(pos);
        }
        else if (rotation == Rotation.Right)
        {
            Vector2 pos = rb2D.position;
            rb2D.position += -Vector2.right * speed * Time.fixedDeltaTime;
            rb2D.MovePosition(pos);
        }
        else
        {
            //do nothings
        }
    }
}
