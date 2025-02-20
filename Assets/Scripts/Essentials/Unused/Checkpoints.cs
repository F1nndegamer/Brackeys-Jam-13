using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private enum WhichOne { beforetreasury, aftertreasury }

    [SerializeField] private WhichOne whichOne;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (whichOne == WhichOne.beforetreasury & collision.CompareTag("Player"))
        {
            GameManager.instance.SetCheckpoint(transform.position);
            PlayerScript.instance.inventory.SaveItem();
        }
        if (whichOne == WhichOne.aftertreasury & GameManager.instance.isTakenTreasure && collision.CompareTag("Player"))
        {
            GameManager.instance.SetCheckpoint(transform.position);
            PlayerScript.instance.inventory.SaveItem();
        }
    }
}