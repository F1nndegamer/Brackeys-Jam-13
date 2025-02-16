using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapDeactivators : MonoBehaviour
{
    bool isActiv = true;
    [SerializeField] private List<Traps> traps;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") & isActiv)
        {
            isActiv = false;
            traps.ForEach(trap => trap.isActive = false);
        }
    }
}