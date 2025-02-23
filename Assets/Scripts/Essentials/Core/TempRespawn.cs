using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempRespawn : MonoBehaviour
{
    public static TempRespawn instance;
    public string sceneName; // Conventie: variabelen beginnen met kleine letter

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Respawn()
    {
        LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(Loading(sceneName));
    }

    private IEnumerator Loading(string sceneName)
    {
        if (FadeController.instance != null)
        {
            FadeController.instance.FadeIn();
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene(sceneName);
    }
}
