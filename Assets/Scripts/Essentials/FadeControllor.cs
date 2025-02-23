using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;
    public static FadeController instance;

    private void Awake()
    {
        instance = this;
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    private void Start()
    {
        FadeOut();
    }
    public void Cycle(int waittime)
    {
        StartCoroutine(Cycling(waittime));
    }

    private IEnumerator Cycling(int waittime)
    {
        FadeIn();
        yield return new WaitForSeconds(waittime);
        FadeOut();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvas(0f, 1f, true));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvas(1f, 0f, false));
    }

    public void FadeInOnly()
    {
        if (canvasGroup.alpha == 0f)
        {
            StartCoroutine(FadeCanvas(0f, 1f, true));
        }
    }

    private IEnumerator FadeCanvas(float startAlpha, float endAlpha, bool enableRaycasts)
    {
        float elapsedTime = 0f;
        canvasGroup.blocksRaycasts = enableRaycasts;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }
        
        canvasGroup.alpha = endAlpha;

        if (endAlpha == 0f)
        {
            canvasGroup.blocksRaycasts = false;
        }
    }
}
