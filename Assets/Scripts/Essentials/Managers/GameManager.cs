using UnityEngine;
using TMPro;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Traps traps;
    public bool isTakenTreasure = false; //shows that the treasure has been taken
    public TextMeshProUGUI DoorLockText;
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
    public void StartWait()
    {
        StartCoroutine(WaitForAnimation());
    }
    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1);
        isTakenTreasure = true;
        Treasure.instance.gameObject.SetActive(false);

        foreach (var callable in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<KeyFunction>())
        {
          callable.CalledFromTressure();
        }
    }
}
