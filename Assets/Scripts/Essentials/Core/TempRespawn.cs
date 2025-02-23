using System.Collections;
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
        StartCoroutine(Loading());
    }
    private IEnumerator Loading()
    {
        FadeController.instance.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneName);
    }
}
