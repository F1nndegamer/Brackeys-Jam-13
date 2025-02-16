using UnityEngine;

public class Treasure : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.isTakenTreasure = true;
        }
    }
    private void Update()
    {
        if(GameManager.instance.isTakenTreasure)
        {
            gameObject.transform.position = PlayerScript.instance.itemPos.position;
        }
    }
}
