using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    public void CallOutAnimation() {
        animator.SetTrigger("CallOut");
    }

    public void ApprovalAnimation() {
        animator.SetTrigger("Approval");
    }

    public void RejectionAnimation()
    {
        animator.SetTrigger("Rejection");
    }


}
