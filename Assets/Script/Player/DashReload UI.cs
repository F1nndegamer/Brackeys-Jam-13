using UnityEngine;
using UnityEngine.UI;

public class DashCooldownUI : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Image cooldownImage; // Assign in Inspector (UI Image for cooldown)

    private void Update()
    {
        if (playerMovement == null || cooldownImage == null) return;

        // Calculate remaining cooldown percentage
        float cooldownPercent = Mathf.Clamp01(playerMovement.GetDashCooldownRemaining() / playerMovement.dashCooldown);
        cooldownImage.fillAmount = 1 - cooldownPercent;
        Debug.Log("Cooldown Remaining: " + playerMovement.GetDashCooldownRemaining());

    }
}
