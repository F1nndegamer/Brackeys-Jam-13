using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour
{
    public int keyID;
    private bool isPickedUp = false;
    void Update()
    {
        if(PlayerScript.instance.inventory.FindItemByID(keyID))
        {

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            if (PlayerScript.instance.inventory.AddItem(gameObject, true))
            {
                isPickedUp = true;
                gameObject.SetActive(false);
            }
        }

        if (other.CompareTag("Door") && IsInInventory())
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

    public void ResetKeyState()
    {
        isPickedUp = false;
    }

    public bool IsInInventory()
    {
        return PlayerScript.instance.inventory.FindItemByID(keyID) != null;
    }

    private void ShowTemporaryMessage(string message)
    {
        GameManager.instance.DoorLockText.text = message;
        GameManager.instance.DoorLockText.gameObject.SetActive(true);
        StartCoroutine(HideMessage());
    }

    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.DoorLockText.gameObject.SetActive(false);
    }
}
