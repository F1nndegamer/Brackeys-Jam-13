using UnityEngine;

public class Traps : MonoBehaviour
{
    public float damage;
    public bool isActive;
    public bool isWork;
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive && isWork)
        {
            Debug.Log("Damage:" + damage); //Base Trap work
            collision.gameObject.GetComponent<PlayerRespawn>().Respawn();
        }
    }
}
