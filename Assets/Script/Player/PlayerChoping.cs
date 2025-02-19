using UnityEngine;

public class PlayerChoping : MonoBehaviour
{
    [SerializeField] private LayerMask tree;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private Animator animator;
    
    public int axeID;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && IsAxeActive())
        {
            AttackTree();
        }
    }

    private bool IsAxeActive()
    {
        Inventory inventory = PlayerScript.instance.inventory;
        if (inventory == null) return false;
        
        GameObject activeItem = inventory.GetActiveItem();
        if (activeItem == null) return false;
        
        Key keyComponent = activeItem.GetComponent<Key>();
        return keyComponent != null && keyComponent.keyID == axeID;
    }

    private void AttackTree()
    {
        if (animator != null)
        {
            animator.SetTrigger("Chop"); // Play chop animation if set
        }

        Collider2D attack = Physics2D.OverlapCircle(attackPoint.position, attackRange, tree);
        if (attack != null)
        {
            TreeClass tree = attack.GetComponent<TreeClass>();
            if (tree != null)
            {
                tree.health -= 1;
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
