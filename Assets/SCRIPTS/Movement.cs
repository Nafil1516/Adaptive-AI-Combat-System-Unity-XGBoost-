using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using Mono.Cecil.Cil;
using System;

/*
public class Movement : MonoBehaviour
{

    [Header("References")]
    public Animator animator;
    CharacterController CC;


    [Header("Movement Speeds")]
    public float walk = 2f;
    public float run = 10f;
    public float roll = 5f;
    public float slide = 5f;

    private float gravity = -9.81f;

    private float verticalcelocity = 0;

    [Header("Animation States")]
    public int movespeed = Animator.StringToHash("speed");
    public int rollstate = Animator.StringToHash("Rolling");
    public int slidestate = Animator.StringToHash("slide");
    public int attackstate = Animator.StringToHash("left_click");
    public int attackstate2 = Animator.StringToHash("right_click");

    private AnimatorStateInfo animationstate;

    bool betweenanimation = false;

    bool isSliding = false;

    bool isRolling = false;

    Vector4 movement;

    private Vector3 rollDirection;

    private Vector3 attackForward;

    private PlayerAttackLogger logger;

    public int attackDamage = 10;

    public bool isBlocking = false;
    private bool inHitReaction = false;




    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
        CC = GetComponent<CharacterController>();
        if (CC == null)
        {
            Debug.LogError("CharacterController component not found on " + gameObject.name);
        }
        logger = FindFirstObjectByType<PlayerAttackLogger>();
        if (logger == null)
        {
            Debug.LogError("PlayerAttackLogger not found in scene! Attach it to a GameObject.");
        }
    }

    void Start()
    {
        animator.SetFloat(movespeed, 0f);
        animationstate = animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log("Movement script initialized.");
        Debug.Log("Current Animation Played: IDLE: " + animationstate.IsName("happy idle"));

    }

    // Update is called once per frame
    void Update()
    {
        inHitReaction = animationstate.IsName("gethit3") || animator.GetCurrentAnimatorStateInfo(0).IsTag("HitReaction");
        bool isgrounded = CC.isGrounded;
        animationstate = animator.GetCurrentAnimatorStateInfo(0);
        betweenanimation = animationstate.IsName("slide") || animationstate.IsName("rolling");
        isRolling = animationstate.IsName("rolling");
        isSliding = animationstate.IsName("slide");
        //Debug.Log("Between Animation State: " + betweenanimation);

        // if(isgrounded && verticalcelocity<0)
        // {
        //     verticalcelocity=-2f;
        // }
        // verticalcelocity += gravity * Time.deltaTime;
        // movement.y=verticalcelocity;
        if (!betweenanimation)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            float mousex = Input.GetAxis("Mouse X");
            //Debug.Log("MOUSE X VALUE:" + mousex);

            Vector3 movementLocal = new Vector3(horizontal, 0, vertical).normalized;
            movement = this.transform.TransformDirection(movementLocal) * Time.fixedDeltaTime;

            Vector3 rotation = new Vector3(0, mousex, 0) * Time.fixedDeltaTime;
            this.transform.Rotate(Vector3.up * mousex * 10f);

            animationstate = animator.GetCurrentAnimatorStateInfo(0);
            if (movement.magnitude >= 0.01f && Input.GetKey(KeyCode.W))
            {
                CC.Move(movement * walk);
                animator.SetFloat(movespeed, 0.6f);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    CC.Move(movement * run);
                    animator.SetFloat(movespeed, 1.1f);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        rollDirection = transform.forward;
                        animator.SetTrigger(rollstate);
                    }
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        rollDirection = transform.forward;
                        animator.SetTrigger(slidestate);
                    }
                }
            }

            else
            {
                animator.SetFloat(movespeed, 0.1f);
            }

        }
        if (isRolling && animationstate.normalizedTime < 1f)
        {
            Vector3 forward = transform.forward;
            CC.Move(rollDirection * roll * Time.deltaTime);
        }
        if (isSliding && animationstate.normalizedTime < 1f)
        {
            Vector3 forward = transform.forward;
            CC.Move(rollDirection * slide * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0))
        {
            string movementState = "idle";
            if (animator.GetFloat(movespeed) > 1f)
            {
                movementState = "run";
            }
            else if (animator.GetFloat(movespeed) > 0.2f)
            {
                movementState = "walk";
            }
            attackForward = transform.forward;
            animator.SetTrigger(attackstate);
            //logger.LogAttack("left_click", movementState);

            StartCoroutine(ResetAttackTrigger(attackstate));

        }
        else if (Input.GetMouseButtonDown(1))
        {
            string movementState = "idle";
            if (animator.GetFloat(movespeed) > 1f)
            {
                movementState = "run";
            }
            else if (animator.GetFloat(movespeed) > 0.2f)
            {
                movementState = "walk";
            }

            attackForward = transform.forward;
            animator.SetTrigger(attackstate2);
            logger.LogAttack("right_click", movementState);

            StartCoroutine(ResetAttackTrigger(attackstate2));
        }
        if (!isRolling && !isSliding && Input.GetKeyDown(KeyCode.Q))
        {
            if (!inHitReaction)  
            {
                isBlocking = true;
                animator.SetTrigger("Block");
            }
        }



    }
    IEnumerator ResetAttackTrigger(int attackHash)
    {
        yield return new WaitForSeconds(0.5f); // match your attack animation length
        animator.ResetTrigger(attackHash);
    }
    // IEnumerator  ResetAttack()
    // {
    //     yield return new WaitForSeconds(1f);
    //     animator.ResetTrigger(attackstate);
    // }

    public void TakeDamage(int damage)
    {
        if (isBlocking)
            return; 

        
        isBlocking = false; 
        animator.SetTrigger("GOTHIT");
        Debug.Log("Player got hit for " + damage);
    }

    public void EndBlock()
    {
        isBlocking = false;
    }
}
*/

public class Movement : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    CharacterController CC;

    [Header("Movement Speeds")]
    public float walk = 2f;
    public float run = 10f;
    public float roll = 5f;
    public float slide = 5f;

    private float gravity = -9.81f;
    private float verticalVelocity = 0;

    [Header("Animation States")]
    public int movespeed = Animator.StringToHash("speed");
    public int rollstate = Animator.StringToHash("Rolling");
    public int slidestate = Animator.StringToHash("slide");
    public int attackstate = Animator.StringToHash("left_click");
    public int attackstate2 = Animator.StringToHash("right_click");

    private AnimatorStateInfo animationstate;

    bool betweenAnimation = false;
    bool isSliding = false;
    bool isRolling = false;
    Vector3 movement;
    private Vector3 rollDirection;
    private Vector3 attackForward;

    private PlayerAttackLogger logger;

    public int attackDamage = 10;
    public bool isBlocking = false;
    private bool inHitReaction = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        CC = GetComponent<CharacterController>();
        logger = FindFirstObjectByType<PlayerAttackLogger>();
    }

    void Start()
    {
        animator.SetFloat(movespeed, 0f);
        animationstate = animator.GetCurrentAnimatorStateInfo(0);
    }

    void Update()
    {
        animationstate = animator.GetCurrentAnimatorStateInfo(0);
        inHitReaction = animationstate.IsTag("HitReaction");
        betweenAnimation = animationstate.IsName("slide") || animationstate.IsName("rolling");
        isRolling = animationstate.IsName("rolling");
        isSliding = animationstate.IsName("slide");

        bool isAttacking = animationstate.IsTag("Attack"); // tag all attack animations with "Attack"

        // Handle block input anytime (even during attack)
        if (!isRolling && !isSliding && Input.GetKeyDown(KeyCode.Q) && !inHitReaction)
        {
            isBlocking = true;
            animator.SetTrigger("Block");
        }

        // Movement logic: only allowed if not rolling/sliding and not attacking
        if (!betweenAnimation && !isAttacking)
        {
            HandleMovement();
        }

        // Handle rolls and slides
        if (isRolling && animationstate.normalizedTime < 1f)
            CC.Move(rollDirection * roll * Time.deltaTime);

        if (isSliding && animationstate.normalizedTime < 1f)
            CC.Move(rollDirection * slide * Time.deltaTime);

        // Handle attack input
        HandleAttackInput();
    }

    private void HandleMovement()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float mouseX = Input.GetAxis("Mouse X");

        Vector3 movementLocal = new Vector3(horizontal, 0, vertical).normalized;
        movement = transform.TransformDirection(movementLocal) * Time.fixedDeltaTime;

        // Optional: smooth rotation while moving
        transform.Rotate(Vector3.up * mouseX * 10f);

        if (movement.magnitude >= 0.01f && Input.GetKey(KeyCode.W))
        {
            CC.Move(movement * walk);
            animator.SetFloat(movespeed, 0.6f);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                CC.Move(movement * run);
                animator.SetFloat(movespeed, 1.1f);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rollDirection = transform.forward;
                    animator.SetTrigger(rollstate);
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    rollDirection = transform.forward;
                    animator.SetTrigger(slidestate);
                }
            }
        }
        else
        {
            animator.SetFloat(movespeed, 0.1f);
        }
    }

    private void HandleAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attackForward = transform.forward;
            animator.SetTrigger(attackstate);
            StartCoroutine(ResetAttackTrigger(attackstate));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            attackForward = transform.forward;
            animator.SetTrigger(attackstate2);
            StartCoroutine(ResetAttackTrigger(attackstate2));
        }
    }

    IEnumerator ResetAttackTrigger(int attackHash)
    {
        yield return new WaitForSeconds(0.5f); // match your attack animation length
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

