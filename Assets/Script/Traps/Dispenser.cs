using System.Diagnostics;
using UnityEngine;

[Tooltip("the class to be used if there is anything to throw(arrows etc.)")]
public class Dispenser : Traps
{
    [SerializeField] private float throwspeed = 10;
    [SerializeField] private GameObject thrownitem;
    public override void WorkTrap(GameObject player)
    {
        base.WorkTrap(player);
        GameObject threwitem = Instantiate(thrownitem, transform.position, Quaternion.identity);
        threwitem.GetComponent<Rigidbody2D>().linearVelocityX = throwspeed;
    }
}
