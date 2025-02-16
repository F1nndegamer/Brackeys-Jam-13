using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Traps traps;
    public bool isTakenTreasure = false; //shows that the treasure has been taken

    private Vector3 lastCheckpoint;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SetCheckpoint(Vector3 position)
    {
        lastCheckpoint = position;
        Debug.Log("Checkpoint set at" + position);
    }
    public Vector3 GetCheckpoint()
    {
        return lastCheckpoint;
    }
}
