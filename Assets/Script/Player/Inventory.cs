using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    [SerializeField] private Sprite sprite;
    
    public void AddInventory(GameObject item)
    {
        if (image1.sprite == sprite)
        {
            image1.sprite = item.GetComponent<SpriteRenderer>().sprite;
            image1.color = item.GetComponent<SpriteRenderer>().color;
        }
        else if (image2.sprite == sprite)
        {
            image2.sprite = item.GetComponent<SpriteRenderer>().sprite;
            image2.color = item.GetComponent<SpriteRenderer>().color;
        }
        else
        {
            Debug.Log("Inventory Full");
        }
    }
    public void RemoveInventory(GameObject item)
    {
        if (image1.sprite == item.GetComponent<SpriteRenderer>().sprite)
        {
            image1.sprite = sprite;
            image1.color = Color.white;
        }
        else if (image2.sprite = item.GetComponent<SpriteRenderer>().sprite)
        {
            image2.sprite = sprite;
            image2.color = Color.white;
        }
        else
        {
            Debug.Log("You don't have");
        }
    }
}
