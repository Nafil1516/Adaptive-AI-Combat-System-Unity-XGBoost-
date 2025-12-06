using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using Mono.Cecil.Cil;
using System;


// public class Movement : MonoBehaviour
// {
//     [Header("References")]
//     public Animator animator;
//     CharacterController CC;

//     [Header("Movement Speeds")]
//     public float walk = 2f;
//     public float run = 10f;
//     public float roll = 5f;
//     public float slide = 5f;

//     private float gravity = -9.81f;
//     private float verticalVelocity = 0;

//     [Header("Animation States")]
//     public int movespeed = Animator.StringToHash("speed");
//     public int rollstate = Animator.StringToHash("Rolling");
//     public int slidestate = Animator.StringToHash("slide");
//     public int attackstate = Animator.StringToHash("left_click");
//     public int attackstate2 = Animator.StringToHash("right_click");

//     private AnimatorStateInfo animationstate;

//     bool betweenAnimation = false;
//     bool isSliding = false;
//     bool isRolling = false;
//     Vector3 movement;
//     private Vector3 rollDirection;
//     private Vector3 attackForward;

//     private PlayerAttackLogger logger;

//     public int attackDamage = 10;
//     public bool isBlocking = false;
//     private bool inHitReaction = false;

//     void Awake()
//     {
//         animator = GetComponent<Animator>();
//         CC = GetComponent<CharacterController>();
//         logger = FindFirstObjectByType<PlayerAttackLogger>();
//     }

//     void Start()
//     {
//         animator.SetFloat(movespeed, 0f);
//         animationstate = animator.GetCurrentAnimatorStateInfo(0);
//     }

//     void Update()
//     {
//         animationstate = animator.GetCurrentAnimatorStateInfo(0);
//         inHitReaction = animationstate.IsTag("HitReaction");
//         betweenAnimation = animationstate.IsName("slide") || animationstate.IsName("rolling");
//         isRolling = animationstate.IsName("rolling");
//         isSliding = animationstate.IsName("slide");

//         bool isAttacking = animationstate.IsTag("Attack"); // tag all attack animations with "Attack"

//         // Handle block input anytime (even during attack)
//         if (!isRolling && !isSliding && Input.GetKeyDown(KeyCode.Q) && !inHitReaction)
//         {
//             isBlocking = true;
//             animator.SetTrigger("Block");
//         }

//         // Movement logic: only allowed if not rolling/sliding and not attacking
//         if (!betweenAnimation && !isAttacking)
//         {
//             HandleMovement();
//         }

//         // Handle rolls and slides
//         if (isRolling && animationstate.normalizedTime < 1f)
//             CC.Move(rollDirection * roll * Time.deltaTime);

//         if (isSliding && animationstate.normalizedTime < 1f)
//             CC.Move(rollDirection * slide * Time.deltaTime);

//         // Handle attack input
//         HandleAttackInput();
//     }

//     private void HandleMovement()
//     {
//         float vertical = Input.GetAxis("Vertical");
//         float horizontal = Input.GetAxis("Horizontal");
//         float mouseX = Input.GetAxis("Mouse X");

//         Vector3 movementLocal = new Vector3(horizontal, 0, vertical).normalized;
//         movement = transform.TransformDirection(movementLocal) * Time.fixedDeltaTime;

//         // Optional: smooth rotation while moving
//         transform.Rotate(Vector3.up * mouseX * 10f);

//         if (movement.magnitude >= 0.01f && Input.GetKey(KeyCode.W))
//         {
//             CC.Move(movement * walk);
//             animator.SetFloat(movespeed, 0.6f);

//             if (Input.GetKey(KeyCode.LeftShift))
//             {
//                 CC.Move(movement * run);
//                 animator.SetFloat(movespeed, 1.1f);

//                 if (Input.GetKeyDown(KeyCode.Space))
//                 {
//                     rollDirection = transform.forward;
//                     animator.SetTrigger(rollstate);
//                 }

//                 if (Input.GetKeyDown(KeyCode.C))
//                 {
//                     rollDirection = transform.forward;
//                     animator.SetTrigger(slidestate);
//                 }
//             }
//         }
//         else
//         {
//             animator.SetFloat(movespeed, 0.1f);
//         }
//     }

//     private void HandleAttackInput()
//     {
//         if (Input.GetMouseButtonDown(0))
//         {
//             string movementState = "idle";

//             if (animator.GetFloat(movespeed) > 1f)
//                 movementState = "run";
//             else if (animator.GetFloat(movespeed) > 0.2f)
//                 movementState = "walk";

//             attackForward = transform.forward;
//             animator.SetTrigger(attackstate);

//             // LOG THIS ATTACK
//             if (logger != null)
//                 logger.LogAction("left_click", movementState);

//             StartCoroutine(ResetAttackTrigger(attackstate));
//         }
//     }

//     IEnumerator ResetAttackTrigger(int attackHash)
//     {
//         yield return new WaitForSeconds(0.5f); // match your attack animation length
//         animator.ResetTrigger(attackHash);
//     }

//     public void TakeDamage(int damage)
//     {
//         if (isBlocking) return;
//         isBlocking = false;
//         animator.SetTrigger("GOTHIT");
//         Debug.Log("Player got hit for " + damage);
//     }

//     public void EndBlock()
//     {
//         isBlocking = false;
//     }
// }






public class Movement : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    private CharacterController CC;

    [Header("Movement Speeds")]
    public float walk = 2f;
    public float run = 10f;
    public float roll = 5f;
    public float slide = 5f;

    [Header("Animation States")]
    public int movespeed = Animator.StringToHash("speed");
    public int rollstate = Animator.StringToHash("Rolling");
    public int slidestate = Animator.StringToHash("slide");
    public int attackstate = Animator.StringToHash("left_click");
    public int attackstate2 = Animator.StringToHash("right_click");

    private AnimatorStateInfo animationstate;
    private bool isRolling = false;
    private bool isSliding = false;

    private Vector3 movement;
    private Vector3 rollSlideDirection;

    private PlayerAttackLogger logger;

    public bool isBlocking = false;
    private bool inHitReaction = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        CC = GetComponent<CharacterController>();
        logger = FindFirstObjectByType<PlayerAttackLogger>();
    }

    void Update()
    {
        animationstate = animator.GetCurrentAnimatorStateInfo(0);
        inHitReaction = animationstate.IsTag("HitReaction");
        isRolling = animationstate.IsName("rolling");
        isSliding = animationstate.IsName("slide");

        // Roll / Slide Input (independent of movement keys)
        if (!isRolling && !isSliding && !inHitReaction)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rollSlideDirection = transform.forward;
                animator.SetTrigger(rollstate);
                logger?.LogAction("roll", GetMoveState());
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                rollSlideDirection = transform.forward;
                animator.SetTrigger(slidestate);
                logger?.LogAction("slide", GetMoveState());
            }
        }

        // Block Input
        if (!isRolling && !isSliding && Input.GetKeyDown(KeyCode.Q) && !inHitReaction)
        {
            isBlocking = true;
            animator.SetTrigger("Block");
            logger?.LogAction("block", GetMoveState());
        }

        // Attack Input
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger(attackstate);
            logger?.LogAction("attack", GetMoveState());
            StartCoroutine(ResetAttackTrigger(attackstate));
        }
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger(attackstate2);
            logger?.LogAction("right_click", GetMoveState());
            StartCoroutine(ResetAttackTrigger(attackstate2));
        }

        // Handle normal movement only if not rolling/sliding or attacking
        bool isAttacking = animationstate.IsTag("Attack");
        if (!isRolling && !isSliding && !isAttacking)
            HandleMovement();

        // Move while rolling/sliding
        if (isRolling && animationstate.normalizedTime < 1f)
            CC.Move(rollSlideDirection * roll * Time.deltaTime);
        if (isSliding && animationstate.normalizedTime < 1f)
            CC.Move(rollSlideDirection * slide * Time.deltaTime);
    }

    private void HandleMovement()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float mouseX = Input.GetAxis("Mouse X");

        Vector3 moveLocal = new Vector3(horizontal, 0, vertical).normalized;
        movement = transform.TransformDirection(moveLocal) * Time.fixedDeltaTime;

        transform.Rotate(Vector3.up * mouseX * 10f);

        if (movement.magnitude >= 0.01f && Input.GetKey(KeyCode.W))
        {
            float speed = Input.GetKey(KeyCode.LeftShift) ? run : walk;
            CC.Move(movement * speed);
            animator.SetFloat(movespeed, speed > walk ? 1.1f : 0.6f);
        }
        else
        {
            animator.SetFloat(movespeed, 0.1f);
        }
    }

    private string GetMoveState()
    {
        float speed = animator.GetFloat(movespeed);
        if (speed > 1f) return "run";
        if (speed > 0.2f) return "walk";
        return "idle";
    }

    IEnumerator ResetAttackTrigger(int attackHash)
    {
        yield return new WaitForSeconds(0.5f);
        animator.ResetTrigger(attackHash);
    }

    public void TakeDamage(int damage)
    {
        if (isBlocking) return;
        isBlocking = false;
        animator.SetTrigger("GOTHIT");
        Debug.Log("Player got hit for " + damage);
    }

    public void EndBlock()
    {
        isBlocking = false;
    }
}





