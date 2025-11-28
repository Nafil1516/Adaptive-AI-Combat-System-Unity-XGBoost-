using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
/*
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
            animator.SetBool("Isattacking", true);
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
*/

/*
public class EnemyAI : MonoBehaviour
{
    public Animator animator;
    public Transform player;

    private NavMeshAgent agent;

    private string lastPlayerAttack = "none";

    public float attackRange = 2.0f;
    public float attackCooldown = 1.5f;
    private float nextAttackTime = 0f;

    private bool isAttacking = false;
    private bool canCombo = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (!animator) Debug.LogError("Animator not found!");
        if (!agent) Debug.LogError("NavMeshAgent not found!");
    }

    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        // === PLAYER NOT IN RANGE → CHASE ===
        if (distToPlayer > attackRange)
        {
            StopAttackState();
            ChasePlayer();
            return;
        }

        // === PLAYER IN RANGE → ATTACK ===
        agent.isStopped = true;
        animator.SetFloat("SPEED", 0.3f);

        AttackLogic();
    }

    // --------------------------------------
    // MOVEMENT
    // --------------------------------------
    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
        animator.SetFloat("SPEED", 0.7f);
    }

    // --------------------------------------
    // ATTACK SYSTEM (FIXED VERSION)
    // --------------------------------------
    void AttackLogic()
    {
        // If not currently attacking → try starting a new attack
        if (!isAttacking)
        {
            if (Time.time >= nextAttackTime)
            {
                StartNewAttack();
            }
            return;
        }

        // If inside attack state → try extending combo
        TryComboExtend();
    }

    void StartNewAttack()
    {
        isAttacking = true;
        canCombo = true;

        animator.SetBool("Isattacking", true);
        animator.SetTrigger("Attack");

        nextAttackTime = Time.time + attackCooldown;
    }

    void TryComboExtend()
    {
        if (!canCombo) return;

        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger("Attack");
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    // --------------------------------------
    // EXIT STATE (CALLED BY ANIMATION EVENT)
    // --------------------------------------
    public void OnAttackFinish()
    {
        isAttacking = false;
        canCombo = false;

        animator.SetBool("Isattacking", false);
    }

    // --------------------------------------
    // STOP ATTACK (WHEN PLAYER MOVES AWAY)
    // --------------------------------------
    public void StopAttackState()
    {
        if (!isAttacking) return;

        isAttacking = false;
        canCombo = false;

        animator.SetBool("Isattacking", false);
    }

    // --------------------------------------
    // ADAPTIVE BLOCKING SYSTEM
    // --------------------------------------
    public void OnPlayerAttack(string attackType)
    {
        lastPlayerAttack = attackType;
        AdaptToPlayer();
    }

    void AdaptToPlayer()
    {
        if (lastPlayerAttack == "left_click")
        {
            animator.SetTrigger("block_left");
        }
        else if (lastPlayerAttack == "right_click")
        {
            animator.SetTrigger("block_right");
        }
    }
}
*/
public class EnemyAI : MonoBehaviour
{
    public Animator animator;
    public Transform player;

    private NavMeshAgent agent;

    private Animator playeraniamtor;

    private string lastPlayerAttack = "none";

    public float attackRange = 2.0f;
    public float attackCooldown = 1.5f;
    private float nextAttackTime = 0f;

    private bool isAttacking = false;
    private bool canCombo = false;

    public int health = 100;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playeraniamtor = player.GetComponent<Animator>();


        if (!animator) Debug.LogError("Animator not found!");
        if (!agent) Debug.LogError("NavMeshAgent not found!");
    }

    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        // === PLAYER NOT IN RANGE → CHASE ===
        if (distToPlayer > attackRange)
        {
            StopAttackState();
            ChasePlayer();
            return;
        }

        // === PLAYER IN RANGE → ATTACK ===
        agent.isStopped = true;
        animator.SetFloat("SPEED", 0.3f);

        AttackLogic();
    }

    // --------------------------------------
    // MOVEMENT
    // --------------------------------------
    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
        animator.SetFloat("SPEED", 0.7f);
    }

    // --------------------------------------
    // ATTACK SYSTEM (FIXED VERSION)
    // --------------------------------------
    void AttackLogic()
    {
        // If not currently attacking → try starting a new attack
        if (!isAttacking)
        {
            if (Time.time >= nextAttackTime)
            {
                StartNewAttack();
            }
            return;
        }

        // If inside attack state → try extending combo
        TryComboExtend();
    }

    void StartNewAttack()
    {
        isAttacking = true;
        canCombo = true;

        animator.SetBool("Isattacking", true);
        animator.SetTrigger("Attack");

        nextAttackTime = Time.time + attackCooldown;
    }

    void TryComboExtend()
    {
        if (!canCombo) return;

        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger("Attack");
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    // --------------------------------------
    // EXIT STATE (CALLED BY ANIMATION EVENT)
    // --------------------------------------
    public void OnAttackFinish()
    {
        isAttacking = false;
        canCombo = false;

        animator.SetBool("Isattacking", false);
    }

    // --------------------------------------
    // STOP ATTACK (WHEN PLAYER MOVES AWAY)
    // --------------------------------------
    public void StopAttackState()
    {
        if (!isAttacking) return;

        isAttacking = false;
        canCombo = false;

        animator.SetBool("Isattacking", false);
    }

    // --------------------------------------
    // ADAPTIVE BLOCKING SYSTEM
    // --------------------------------------
    public void OnPlayerAttack(string attackType)
    {
        lastPlayerAttack = attackType;
        AdaptToPlayer();
    }

    void AdaptToPlayer()
    {

        if (playeraniamtor.GetCurrentAnimatorStateInfo(0).IsName("light attack 1"))
        {
            animator.SetTrigger("EnemyBlock");
        }
        else if (playeraniamtor.GetCurrentAnimatorStateInfo(0).IsName("Right_Attack"))
        {
            lastPlayerAttack = "right_click";
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took damage: " + damage);

        if (animator != null)
        {
            Debug.Log("Firing PlayerH it trigger!");
            animator.SetTrigger("PlayerHit");
        }

        if (health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        animator.SetTrigger("Die");
        animator.SetBool("IsDead", true);
        agent.isStopped = false;

        // Disable enemy components here



    }
}