using UnityEngine;

public class Traps : MonoBehaviour
{
    public float damage;
    public bool isActive = true; //when press Deactive button, is false
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive && GameManager.instance.isTakenTreasure && collision.gameObject.CompareTag("Player"))
        {
            //die an respawn
        }
    }

    public virtual void WorkTrap(GameObject player)
    {
        Debug.Log("Damage:" + damage); 
    }
}
