using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Axe : MonoBehaviour
{
    private bool isPickedUp = false;
    private static HashSet<int> usedCodes = new HashSet<int>();
    [SerializeField] private int uniqueCode;
    void Update()
    {
        if(!PlayerScript.instance.inventory.FindItemByCode(uniqueCode))
        {
            isPickedUp = false;
        }
    }

    void Start()
    {
        PlayerChoping.instance.axeCode = new int[]{uniqueCode};
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            if (PlayerScript.instance.inventory.AddItem(gameObject, uniqueCode, true))
            {
                isPickedUp = true;
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetKeyState()
    {
        isPickedUp = false;
    }

    public bool IsInInventory()
    {
        return PlayerScript.instance.inventory.FindItemByCode(uniqueCode) != null;
    }
}
