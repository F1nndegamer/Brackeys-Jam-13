using UnityEngine;
using UnityEngine.SceneManagement;
public class TempRespawn : MonoBehaviour
{
    public static TempRespawn instance;
    void Start()
    {
        instance = this;
    }
    public void respawm()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
