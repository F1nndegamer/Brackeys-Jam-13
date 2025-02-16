using System.Collections;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;  // Singleton instance

    [SerializeField] private AudioSource footstepSource; // Assign in Inspector
    [SerializeField] private AudioClip[] footstepClips;  // Array of footstep sounds
    [SerializeField] private float minStepInterval = 0.3f; // Minimum delay between steps
    [SerializeField] private float maxStepInterval = 0.5f; // Max delay for variation

    private bool isPlayingFootsteps = false;

    private void Awake()
    {
        // Singleton pattern
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
            yield return new WaitForSeconds(Random.Range(minStepInterval, maxStepInterval));
        }
    }
}
