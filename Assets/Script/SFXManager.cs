using System.Collections;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Footstep Sounds")]
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioClip[] footstepClips;

    [Header("Jump Sound")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip dubblejumpClip;

    [Header("Dash Sound")]
    [SerializeField] private AudioClip dashClip;
    [SerializeField] private AudioClip dashRecoverClip;

    [Header("UI Sounds (Unused)")]
    [SerializeField] private AudioClip uiPressClip;
    [SerializeField] private AudioClip uiHoverClip;

    private bool isPlayingFootsteps = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayRandomFootstep()
    {
        if (footstepClips.Length == 0 || footstepSource == null) return;
        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        footstepSource.PlayOneShot(clip);
    }

    public void StartFootsteps()
    {
        if (!isPlayingFootsteps)
        {
            isPlayingFootsteps = true;
            StartCoroutine(FootstepLoop());
        }
    }

    public void StopFootsteps()
    {
        isPlayingFootsteps = false;
    }

    private IEnumerator FootstepLoop()
    {
        while (isPlayingFootsteps)
        {
            PlayRandomFootstep();
            yield return new WaitForSeconds(Random.Range(0.3f, 0.5f));
        }
    }

    public void PlayJumpSound()
    {
        if (sfxSource != null && jumpClip != null)
        {
            sfxSource.PlayOneShot(jumpClip);
        }
    }
    public void PlayDubbleJumpSound()
    {
        if (sfxSource != null && dubblejumpClip != null)
        {
            sfxSource.PlayOneShot(dubblejumpClip);
        }
    }

    public void PlayDashSound()
    {
        if (sfxSource != null && dashClip != null)
        {
            sfxSource.PlayOneShot(dashClip);
        }
    }
    public void PlayDashRecoverSound()
    {
        if (sfxSource != null && dashRecoverClip != null)
        {
            sfxSource.PlayOneShot(dashRecoverClip);
        }
    }

    public void PlayUIPressSound()
    {
        if (sfxSource != null && uiPressClip != null)
        {
            sfxSource.PlayOneShot(uiPressClip);
        }
    }

    public void PlayUIHoverSound()
    {
        if (sfxSource != null && uiHoverClip != null)
        {
            sfxSource.PlayOneShot(uiHoverClip);
        }
    }
}
