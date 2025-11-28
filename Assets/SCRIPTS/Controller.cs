using UnityEngine;

public class Controller : MonoBehaviour
{
    Animator bananaAnimator;
    AnimatorStateInfo animationstate;
    
    void Start()
    {
        
        bananaAnimator = this.GetComponent<Animator>();
        if (bananaAnimator == null)
        {
            Debug.LogError("No animator component found on this GameObject.");
        }
        else
        {
            Debug.Log("Animator component successfully found.");
        }
    }
    void Awake()
    {
        
    }


    void FixedUpdate()
    {
        animationstate = bananaAnimator.GetCurrentAnimatorStateInfo(0);
        if(animationstate.IsName("rolling") || animationstate.IsName("slide"))
        {
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += this.transform.forward * 0.01f;
            bananaAnimator.GetFloat("speed");
            bananaAnimator.SetFloat("speed", 0.6f);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                bananaAnimator.GetFloat("speed");
                bananaAnimator.SetFloat("speed", 1.1f);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    bananaAnimator.SetTrigger("Rolling");
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    bananaAnimator.SetTrigger("slide");
                }

            }
        }
        else
        {
            bananaAnimator.GetFloat("speed");
            bananaAnimator.SetFloat("speed", 0.1f);
        }

    }
}
