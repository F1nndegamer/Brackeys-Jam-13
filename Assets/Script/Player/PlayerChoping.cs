using UnityEngine;

public class PlayerChoping : MonoBehaviour
{
    [SerializeField] private LayerMask tree;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //play attack animation (we don't have now or I can not see)
            Collider2D attack = Physics2D.OverlapCircle(attackPoint.position, attackRange, tree);

            attack.GetComponent<TreeClass>().isChoped = true;
        }
    }
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    */
}
