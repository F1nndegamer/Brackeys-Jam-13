using System.Collections.Generic;
using UnityEngine;

[Tooltip("a class to be used if traps need an additional activator (pressure plate, switch, etc.)")]
[RequireComponent(typeof(BoxCollider2D))]
public class TrapActivator: MonoBehaviour
{
    public List<Traps> traps;
    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (traps.Count != 0)
        {
            if (collision.CompareTag("Player") && GameManager.instance.isTakenTreasure)
            {
                traps.ForEach(x => x.WorkTrap(collision.gameObject));
            }
        }

    }
}