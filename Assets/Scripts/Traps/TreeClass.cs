using UnityEditor;
using UnityEngine;

public class TreeClass : MonoBehaviour
{
    public int health = 3; // Default health value, can be adjusted per tree
    private bool isFalling = false;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip treeFalling;
    [SerializeField] private AnimationClip treeFell;

    private void Update()
    {
        if (health <= 0 && !isFalling)
        {
            ChopDown();
        }
    }

    private void ChopDown()
    {
        SFXManager.Instance.PlayTreeFallSound();
        animator.SetBool("isFalling", true);
        isFalling = true;
    }
}