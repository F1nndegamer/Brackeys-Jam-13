using UnityEngine;

public class PlayerChoping : MonoBehaviour
{
    [SerializeField] private LayerMask tree;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private Animator animator;

    public static PlayerChoping instance;
    public int[] axeCode = new int[1];
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && IsAxeActive())
        {
            AttackTree();
        }
    }
    void Awake()
    {
        instance = this;  
        animator = GetComponent<Animator>(); 
    }
    private bool IsAxeActive()
    {
        Inventory inventory = PlayerScript.instance.inventory;
        if (inventory == null) return false;
        
        GameObject activeItem = inventory.GetActiveItem();
        if (activeItem == null) return false;
        foreach(int axe in axeCode)
        {
            if(inventory.FindItemByCode(axe)) return true;
            else return false;
        }
        return false;
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
            SFXManager.Instance.PlayWoodHitSound();
            TreeClass tree = attack.GetComponent<TreeClass>();
            if (tree != null)
            {
                tree.health -= 1;
            }
        }
        else{
            SFXManager.Instance.PlayAirHitSound();
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
