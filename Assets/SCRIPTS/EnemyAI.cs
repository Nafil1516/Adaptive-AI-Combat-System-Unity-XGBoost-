using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator animator;      // Enemy Animator
    public Transform player;       // Optional, for later distance calculation

    private string lastPlayerAttack = "none";

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Optional: idle or move logic can be added later
    }

    // Called by PlayerAttackLogger whenever player attacks
    public void OnPlayerAttack(string attackType)
    {
        lastPlayerAttack = attackType;
        AdaptToPlayer();
    }

    void AdaptToPlayer()
    {
        // Simple adaptive logic
        if (lastPlayerAttack == "left_click")
        {
            Debug.Log("Enemy blocks left attack!");
            animator.SetTrigger("block_left");  // Make sure you have this trigger in Animator
        }
        else if (lastPlayerAttack == "right_click")
        {
            Debug.Log("Enemy blocks right attack!");
            animator.SetTrigger("block_right"); // Make sure you have this trigger in Animator
        }
        else
        {
            animator.SetFloat("speed", 0f); // idle
        }
    }
}
