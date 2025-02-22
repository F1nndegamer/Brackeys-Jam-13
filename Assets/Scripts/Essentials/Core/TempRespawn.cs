using UnityEngine;
using UnityEngine.SceneManagement;
public class TempRespawn : MonoBehaviour
{
    public static TempRespawn instance;
    public string SceneName;
    void Start()
    {
        instance = this;
    }
    public void respawm()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneName);
    }
}
