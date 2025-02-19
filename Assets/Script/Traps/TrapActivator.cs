using UnityEngine;

[Tooltip("a class to be used if traps need an additional activator (pressure plate, switch, etc.)")]
public class TrapActivator: MonoBehaviour
{
    public bool isActive = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.instance.isTakenTreasure)
        {
            isActive = true;
        }
    }
}
