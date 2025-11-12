using UnityEngine;
using UnityEngine.UIElements;

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
        animationstate = animator.GetCurrentAnimatorStateInfo(0);
        betweenanimation = animationstate.IsName("slide") || animationstate.IsName("rolling");
        isRolling = animationstate.IsName("rolling");
        isSliding = animationstate.IsName("slide");
        Debug.Log("Between Animation State: " + betweenanimation);
        if (!betweenanimation)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            float mousex = Input.GetAxis("Mouse X");
            Debug.Log("MOUSE X VALUE:" + mousex);

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
            attackForward = transform.forward;
            animator.SetTrigger(attackstate);
            Debug.Log("Left Click Detected - Attack Triggered");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger(attackstate2);
            Debug.Log("Right Click Detected - Attack Triggered");
        }

    }

    // IEnumerator  ResetAttack()
    // {
    //     yield return new WaitForSeconds(1f);
    //     animator.ResetTrigger(attackstate);
    // }



}
