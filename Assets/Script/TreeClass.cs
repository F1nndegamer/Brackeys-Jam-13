using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TreeClass : MonoBehaviour
{
    public int health = 3;
    [SerializeField] private Animator animator;
    [SerializeField] private List<Traps> traps;
    [SerializeField] private List<PlatformClass> platform;
    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (traps.Count != 0 & health <= 0)
        {
            //play tree fall animation
            traps.ForEach(x => x.isActive = false);
        }
        else if (platform.Count != 0 & health <= 0)
        {
            //play tree fall animation
            platform.ForEach(x => x.isMade = true);
        }
        else
        {
            //do nothing, idk
        }
    }

    
}