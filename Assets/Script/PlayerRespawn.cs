using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;  
    }

    // Update is called once per frame
    public void Respawn()
    {
        if (GameManager.instance.GetCheckpoint() != Vector3.zero)
            transform.position = GameManager.instance.GetCheckpoint();
        else
            transform.position = startPos;
    }
}
