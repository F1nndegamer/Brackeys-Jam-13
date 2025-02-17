using UnityEngine;
using System.Collections;

public class door : MonoBehaviour, KeyFunction 
{
    //to make it invisible at start
    private BoxCollider2D[] colliders;
    private SpriteRenderer spriteRenderer;    public int doornum;
    private void Start()
    {
        colliders = GetComponents<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ToggleComponentsState();
    }
    public void CalledFromTressure()
    {
        ToggleComponentsState();
    }
    public void KeyOpened()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(PlayerScript.instance.HasKey == false)
            {
            GameManager.instance.DoorLockText.text = "This door is locked";
            GameManager.instance.DoorLockText.gameObject.SetActive(true);
            StartCoroutine(DoorTextDeact());
            }
        }
    }
    IEnumerator DoorTextDeact()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.DoorLockText.gameObject.SetActive(false);
    }
    public void ToggleComponentsState()
    {
        if (colliders.Length >= 2)
        {
            bool newState = !colliders[0].enabled;
            
            foreach (BoxCollider2D collider in colliders)
            {
                collider.enabled = newState;
            }
            
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = newState;
            }
        }
        else
        {
            Debug.LogWarning("Not enough BoxCollider2D components found on this GameObject!");
        }
    }
}
