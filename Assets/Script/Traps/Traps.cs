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
                    Debug.Log("Damage:" + damage); //Base Trap work
                    collision.gameObject.GetComponent<PlayerRespawn>().Respawn();
                }
            }
            else
            {
                Debug.Log("Damage:" + damage); //Base Trap work
                collision.gameObject.GetComponent<PlayerRespawn>().Respawn();
            }
        }
    }
}
