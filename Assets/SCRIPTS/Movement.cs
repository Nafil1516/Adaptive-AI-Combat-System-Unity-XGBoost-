using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("References")]
    public Animator animator;
    CharacterController CC;


    [Header("Movement Speeds")]
    public float walk = 5f;
    public float run = 10f;
    public float roll = 7f;
    public float slide = 9f;

    [Header("Animation States")]
    public int movespeed = Animator.StringToHash("speed");
    public int rollstate = Animator.StringToHash("Rolling");
    public int slidestate = Animator.StringToHash("slide");

    private AnimatorStateInfo animationstate;



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
        animationstate= animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log("Movement script initialized.");
        Debug.Log("Current Animation Played: IDLE: " + animationstate.IsName("happy idle"));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
