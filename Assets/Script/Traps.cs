using UnityEngine;

public class Traps : MonoBehaviour
{
    public float damage;
    public bool isActive;
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive)
        {
            Debug.Log("Damage:" + damage);
        }
    }
}
