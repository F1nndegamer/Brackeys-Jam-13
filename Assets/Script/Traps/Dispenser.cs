using System.Diagnostics;
using UnityEngine;

[Tooltip("the class to be used if there is anything to throw(arrows etc.)")]
public class Dispenser : Traps
{
    [SerializeField] private GameObject thrownitem;

    private void Update()
    {
        if (isActive && GameManager.instance.isTakenTreasure)
        {
            if (trapActivator != null)
            {
                GameObject threwitem = Instantiate(thrownitem, Vector3.right, Quaternion.identity);
                threwitem.GetComponent<Rigidbody2D>().linearVelocity = Vector2.right;
            }
        }
    }
}
