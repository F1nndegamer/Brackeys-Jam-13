using UnityEngine;

public class Trapdoor : Traps
{
    [SerializeField] private bool isWorking = false;
    [SerializeField] private float rotationSpeed = 1;
    private void Update()
    {
        if (isWorking)
        {
            transform.rotation = Quaternion.Euler(0, 0, rotationSpeed * Time.time);
        }
    }
    public override void WorkTrap(GameObject player)
    {
        base.WorkTrap(player);
        isWorking = isWorking != true ? true : false;
    }
}