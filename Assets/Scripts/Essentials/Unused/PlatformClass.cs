using UnityEngine;

public class PlatformClass : MonoBehaviour
{
    public bool isMade = false; //it ensures that it will not work until the tree is cut down
    void Update()
    {
        if (!isMade)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        
    }
}
