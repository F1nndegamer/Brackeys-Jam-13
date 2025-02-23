using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class EndGame : MonoBehaviour
{
    public GameObject Chest;
    public int treasurecode;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            
        StartCoroutine(EndGametime());
        Debug.Log("Collision detected with: " + other.gameObject.name);
        }
    }
    private IEnumerator EndGametime()
    {

        treasurecode = Treasure.instance.GetCode();
        GameObject tressure = PlayerScript.instance.inventory.FindItemByCode(treasurecode);
        if(!PlayerScript.instance.inventory.RemoveItem(tressure))
        {
            yield return null;
        }
        Chest.SetActive(true);
        yield return new WaitForSeconds(1f); //lenght of animation
        FadeController.instance.FadeIn();
        yield return new WaitForSeconds(2f); //lenght of fade out
        SceneManager.LoadScene("Title");
    }
}
