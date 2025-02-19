using UnityEngine;

public class Traps : MonoBehaviour
{
    public float damage;
    public TrapActivator trapActivator;
    public bool isActive = true; //when press Deactive button, is false
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive && GameManager.instance.isTakenTreasure)
        {
            if (trapActivator != null)
            {
                if (trapActivator.isActive)
                {
                    TrapWork(collision.gameObject);
                }
            }
            else
            {
                TrapWork(collision.gameObject);
            }
        }
    }
    void TrapWork(GameObject player) //Base Trap work
    {
        Debug.Log("Damage:" + damage); 
        player.GetComponent<PlayerRespawn>().Respawn();
    }
}
