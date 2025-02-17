using System.Linq;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.isTakenTreasure = true;

            var callables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<KeyFunction>();

            foreach (KeyFunction callable in callables)
            {
                callable.CalledFromTressure();
            }
        }
    }

    private void Update()
    {
        if (GameManager.instance.isTakenTreasure)
        {
            gameObject.transform.position = PlayerScript.instance.itemPos.position;
        }
    }
}
