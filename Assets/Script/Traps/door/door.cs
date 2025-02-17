using UnityEngine;
using System.Collections;

public class door : MonoBehaviour, KeyFunction 
{
    public int doornum;
    public void CalledFromTressure()
    {
        gameObject.SetActive(true);
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
}
