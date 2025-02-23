using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, KeyFunction
{
    private BoxCollider2D[] colliders;
    private SpriteRenderer[] spriteRenderer;
    public bool removeStart = true;
    public int doorID; // Changed 'doornum' to 'doorID' for clarity

    private void Start()
    {
        colliders = GetComponents<BoxCollider2D>();
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>();

        ToggleComponentsState();
    }

    public void CalledFromTressure()
    {
        ToggleComponentsState();
    }

    public void UnlockDoor()
    {
        Destroy(gameObject);
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

    public void ToggleComponentsState()
    {
        if (colliders.Length >= 2 && removeStart)
        {
            bool newState = !colliders[0].enabled;

            foreach (BoxCollider2D collider in colliders)
            {
                collider.enabled = newState;
            }

            foreach (SpriteRenderer renderer in spriteRenderer)
            {
                renderer.enabled = newState;
            }
        }
    }
}
