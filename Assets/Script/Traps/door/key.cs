using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour
{
    public int keyID;
    private bool isPickedUp = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
                isPickedUp = true;
                PlayerScript.instance.HasKey = true;

                if (PlayerScript.instance.inventory.AddItem(gameObject, true))
                {
                    gameObject.SetActive(false); 
                }
            
        }

        if (other.CompareTag("Door") && isPickedUp)
        {
            Door doorScript = other.GetComponent<Door>();

            if (doorScript != null && doorScript.doorID == keyID)
            {
                doorScript.UnlockDoor();
                PlayerScript.instance.inventory.RemoveItem(gameObject);
                Destroy(gameObject);
            }
            else
            {
                ShowTemporaryMessage("Wrong Key");
            }
        }
    }

    private void ShowTemporaryMessage(string message)
    {
        GameManager.instance.DoorLockText.text = message;
        GameManager.instance.DoorLockText.gameObject.SetActive(true);
        StartCoroutine(HideMessage());
    }

    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.DoorLockText.gameObject.SetActive(false);
    }
}
