using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;
    public Transform itemPos;
    private void Start()
    {
        instance = this;
    }
}
