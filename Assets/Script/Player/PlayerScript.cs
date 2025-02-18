using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;
    public Transform itemPos;
    public bool HasKey;
    public Inventory inventory;
    private void Start()
    {
        instance = this;
        inventory.GetComponent<Inventory>();
    }
}
