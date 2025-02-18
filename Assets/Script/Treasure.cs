using System.Linq;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private bool isTaken;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if(isTaken) return;
        if (PlayerScript.instance.inventory.AddItem(gameObject, true))
        {
            GameManager.instance.isTakenTreasure = true;
            gameObject.SetActive(false);

            foreach (var callable in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<KeyFunction>())
            {
                callable.CalledFromTressure();
            }
        }
        else
        {
            Debug.Log("Inventory Full");
        }
    }
}
