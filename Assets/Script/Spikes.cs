using UnityEngine;

public class Spikes : Traps
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        Debug.Log(collision.gameObject.name.ToString());
    }
}
