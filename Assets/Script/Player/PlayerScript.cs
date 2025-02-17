using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;
    public Transform itemPos;
    public bool HasKey;
    private void Start()
    {
        instance = this;
    }
}
