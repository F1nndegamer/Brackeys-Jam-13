using UnityEngine;
using System.Collections;
public class key : MonoBehaviour
{
    public int keynum;
    bool ispickedup;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(PlayerScript.instance.HasKey == false)
            {
            ispickedup = true;
            PlayerScript.instance.HasKey = true;
            }
            else
            {
            GameManager.instance.DoorLockText.text = "You already have a key";
            GameManager.instance.DoorLockText.gameObject.SetActive(true);
                StartCoroutine(TextDeact());
            }
        }
        if(other.gameObject.CompareTag("Door"))
        {
            door doorscript = other.gameObject.GetComponent<door>();
            if(doorscript.doornum == keynum)
            {
                doorscript.KeyOpened();
               Destroy(this.gameObject);
            }
            else
            {
                GameManager.instance.DoorLockText.text = "Wrong Key";
                GameManager.instance.DoorLockText.gameObject.SetActive(true);
                StartCoroutine(TextDeact());
            }
        }

    }
    void Update()
    {
        if(ispickedup)
        {
            gameObject.transform.position = PlayerScript.instance.itemPos.transform.position;
        }
    }
    IEnumerator TextDeact()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.DoorLockText.gameObject.SetActive(false);
    }
}
