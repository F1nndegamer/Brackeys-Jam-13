using UnityEngine;
using UnityEngine.UI;

public class DashCooldownUI : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Image cooldownImage;
    private bool isFading = false;
    private bool canTurnGreen = true;
    private float fadeDuration = 1.0f;
    private float fadeTimer = 0f;

    private Color startColor = Color.green;
    private Color endColor = Color.white;

    private void Update()
    {
        if (playerMovement == null || cooldownImage == null) return;

        float cooldownPercent = Mathf.Clamp01(playerMovement.GetDashCooldownRemaining() / playerMovement.dashCooldown);
        cooldownImage.fillAmount = 1 - cooldownPercent;

        if (cooldownImage.fillAmount >= 1f && !isFading && canTurnGreen)
        {
            cooldownImage.color = startColor;
            isFading = true;
            fadeTimer = 0f;
            canTurnGreen = false;
        }

        if (isFading)
        {
            fadeTimer += Time.deltaTime / fadeDuration;
            cooldownImage.color = Color.Lerp(startColor, endColor, fadeTimer);

            if (fadeTimer >= 1f)
            {
                isFading = false;
            }
        }

        if (cooldownImage.fillAmount < 1f)
        {
            canTurnGreen = true;
        }
    }
}
