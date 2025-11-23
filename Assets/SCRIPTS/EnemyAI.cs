using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Animator animator;      // Enemy Animator
    public Transform player;       // Optional, for later distance calculation

    NavMeshAgent NMA;

    private string lastPlayerAttack = "none";



    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("ANIMATOR NOT FOUND");    
        }
    
        NMA = GetComponent<NavMeshAgent>();
        if (NMA == null)
        {
            Debug.LogError("NAV MESH AGENT NOT FOUND");
        }
    }

    void Update()
    {
        if (!NMA.pathPending)
        {
            NMA.SetDestination(player.transform.position);
        }

        if (NMA.remainingDistance > NMA.stoppingDistance)
        {
            NMA.isStopped = false;
            animator.SetFloat("SPEED", 0.7f);
        }
        if (NMA.remainingDistance <= NMA.stoppingDistance)
        {
            NMA.isStopped = true;
            animator.SetFloat("SPEED", 0.3f);
            animator.SetTrigger("Attack");

        }
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
