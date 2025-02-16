using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.SetCheckpoint(transform.position);
        }
    }
}
