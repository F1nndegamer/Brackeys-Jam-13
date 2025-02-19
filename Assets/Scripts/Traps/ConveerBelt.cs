using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ConveerBelt : MonoBehaviour
{
    public enum Rotation { rigth, left, stop}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb2D;
    [SerializeField] private float speed = 1;

    public Rotation rotation;
    public bool activateswitch;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!activateswitch) return;

        if (rotation == Rotation.rigth)
        {
            //play animation
            SFXManager.Instance.PlayConveerBeltSound();
            Vector2 pos = rb2D.position;
            rb2D.position += Vector2.left * speed * Time.fixedDeltaTime;
            rb2D.MovePosition(pos);
        }
        else if (rotation == Rotation.left)
        {
            //play animation and sfx
            SFXManager.Instance.PlayConveerBeltSound();
            Vector2 pos = rb2D.position;
            rb2D.position += Vector2.left * speed * Time.fixedDeltaTime * -1;
            rb2D.MovePosition(pos);
        }
        else
        {
            //do nothing
        }
    }
}
