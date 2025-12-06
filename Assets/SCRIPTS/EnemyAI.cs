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

/*
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

        if (distToPlayer > attackRange)
        {
            StopAttackState();
            ChasePlayer();
            return;
        }

        agent.isStopped = true;
        animator.SetFloat("SPEED", 0.3f);

        AttackLogic();
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
        animator.SetFloat("SPEED", 0.7f);
    }

      void AttackLogic()
    {
        if (!isAttacking)
        {
            if (Time.time >= nextAttackTime)
            {
                StartNewAttack();
            }
            return;
        }
        TryComboExtend();
    }

    void StartNewAttack()
    {
        agent.isStopped = true;
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

       public void OnAttackFinish()
    {
        isAttacking = false;
        canCombo = false;
        agent.isStopped = false;

        animator.SetBool("Isattacking", false);
    }

    public void StopAttackState()
    {
        if (!isAttacking) return;

        isAttacking = false;
        canCombo = false;
        agent.isStopped = false;

        animator.SetBool("Isattacking", false);
    }

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
        agent.isStopped = true;
    }
}
*/

public class EnemyAI : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Transform player;
    private NavMeshAgent agent;

    private string lastPlayerAttack = "none";

    private Animator playeraniamtor;

    private Movement playerMovement;

    public bool playerblockedtheattack = false;


    [Header("Combat Settings")]
    public float attackRange = 2.0f;
    public float attackCooldown = 1.5f;
    public float attackWindUp = 0.4f;
    private float nextAttackTime = 0f;

    private bool isAttacking = false;
    private bool waitingForExit = false;

    [Header("Enemy Stats")]
    public int health = 100;

    private bool isStunned = false;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playeraniamtor = player.GetComponent<Animator>();
        playerMovement = player.GetComponent<Movement>();
        if (!animator) animator = GetComponent<Animator>();
        if (!agent) Debug.LogError("NavMeshAgent not found!");
    }

    void Update()
    {
        if (animator.GetBool("IsDead")) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > attackRange)
        {
            ExitAttackState();
            ChasePlayer();
            return;
        }
        if (playerblockedtheattack)
        {
            animator.SetBool("isattacking", false);
        }

        agent.isStopped = true;
        animator.SetFloat("SPEED", 0.25f);

        HandleAttackLogic();
    }
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


    void ChasePlayer()
    {
        if (!agent.isActiveAndEnabled) return;

        agent.isStopped = false;
        agent.SetDestination(player.position);
        animator.SetFloat("SPEED", 0.7f);
    }


    void HandleAttackLogic()
    {
        if (!isAttacking)
        {
            if (Time.time >= nextAttackTime)
            {
                StartAttack();
            }
            return;
        }


        if (waitingForExit) return;
    }

    void StartAttack()
    {
        isAttacking = true;
        waitingForExit = true;

        animator.SetBool("Isattacking", true);
        animator.SetTrigger("Attack");

        agent.isStopped = true;

        // next time AI is allowed to re-attack
        nextAttackTime = Time.time + attackCooldown;

        // allow animation to exit after a short delay
        Invoke(nameof(AllowExit), attackWindUp);
    }

    void AllowExit()
    {
        waitingForExit = false;
    }

    // This will be called by animation event (or at end of attack)
    public void OnAttackFinish()
    {
        ExitAttackState();
    }

    // EnemyAI.cs (ExitAttackState)
    void ExitAttackState()
    {
        if (!isAttacking) return;

        isAttacking = false;
        waitingForExit = false;
        animator.SetBool("Isattacking", false);
        if (agent.isActiveAndEnabled)
            agent.isStopped = false;
    }


    public void TakeDamage(int damage, bool playerBlocked = false)
    {
        if (health <= 0) return; 
        if (isStunned) return;   

       
        if (playerBlocked)
        {
            
            animator.SetTrigger("Blocked");

           
            isStunned = true;

          
            agent.isStopped = true;
            isAttacking = false;
            waitingForExit = false;

            Debug.Log("Enemy is stunned by player block!");

           
            float stunDuration = 1.0f; 
            Invoke(nameof(ExitStun), stunDuration);
            return;
        }
        animator.SetBool("isattacking", false);
        Debug.Log("Enemy took damage: " + damage);
        animator.SetTrigger("PlayerHit");
        health -= damage;

        Debug.Log($"Enemy took {damage} damage! Remaining health: {health}");
       
        if (isAttacking)
        {
            ExitAttackState();
        }
        if (health <= 0)
        {
            Die();
        }
    }

    public void disableattack()
    {
        isAttacking = false;
        waitingForExit = false;
        
        animator.SetBool("Isattacking", false);
        if (agent.isActiveAndEnabled)
            agent.isStopped = false;
    }
    void Die()
    {
        animator.SetBool("IsDead", true);
        animator.SetTrigger("Die");
        agent.isStopped = true;
    }

    void ExitStun()
    {
        isStunned = false;
        agent.isStopped = false;
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange)
        {
            Debug.Log("Enemy resumes attack after stun.");
            StartAttack();
        }
        else
        {
            Debug.Log("Enemy chases player after stun.");
            ChasePlayer();
        }
    }

}
