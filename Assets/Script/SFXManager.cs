using System.Collections;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    [Header("Audio Sources")]    
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Footstep Sounds")]
    [SerializeField] private AudioClip[] footstepClips;

    [Header("Jump Sound")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip dubblejumpClip;

    [Header("Dash Sound")]
    [SerializeField] private AudioClip dashClip;
    [SerializeField] private AudioClip dashRecoverClip;

    [Header("UI Sounds (Unused)")]
    [SerializeField] private AudioClip uiPressClip;
    [SerializeField] private AudioClip uiHoverClip;

    [Header("Conveer Belt Sounds")]
    [SerializeField] private AudioClip conveerBelt;

    [Header("Tree Sounds")]
    [SerializeField] private AudioClip HittingAir;
    [SerializeField] private AudioClip HittingWood;
    [SerializeField] private AudioClip TreeFall;
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
        while (footstepSource.isPlaying)
        {
            yield return null;
        }

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
    public void PlayConveerBeltSound()
    {
        if (sfxSource != null && conveerBelt != null)
        {
            sfxSource.PlayOneShot(conveerBelt);
        }
    }
    public void PlayWoodHitSound()
    {
        if (sfxSource != null && jumpClip != null)
        {
            sfxSource.PlayOneShot(HittingWood);
        }
    }
    public void PlayAirHitSound()
    {
        if (sfxSource != null && jumpClip != null)
        {
            sfxSource.PlayOneShot(HittingAir);
        }
    }
    public void PlayTreeFallSound()
    {
        if (sfxSource != null && jumpClip != null)
        {
            sfxSource.PlayOneShot(TreeFall);
        }
    }
    
}
