using System.Linq;
using UnityEngine;
using System.Collections.Generic;
public class Treasure : MonoBehaviour
{
    
    public Transform TpPos;
    private bool isTaken;
    private static HashSet<int> usedCodes = new HashSet<int>();
    [SerializeField] private int uniqueCode;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if(isTaken) return;
        if (PlayerScript.instance.inventory.AddItem(gameObject, uniqueCode, true))
        {
            GameManager.instance.isTakenTreasure = true;
            gameObject.SetActive(false);

            foreach (var callable in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<KeyFunction>())
            {
                callable.CalledFromTressure();
            }
        }
        else
        {
            Debug.Log("Inventory Full");
        }
    }
    void Reset()
    {
        GenerateUniqueCode();
    }
    void GenerateUniqueCode()
    {
        if (uniqueCode == 0)
        {
            do
            {
                uniqueCode = Random.Range(1000, 9999);
            } while (usedCodes.Contains(uniqueCode));
            
            usedCodes.Add(uniqueCode);
            PlayerPrefs.SetInt(gameObject.name + "_Code", uniqueCode);
            PlayerPrefs.Save();
        }

        Debug.Log($"Generated unique code for {gameObject.name}: {uniqueCode}");
    
    }
    public int GetCode()
    {
        return uniqueCode;
    }
}