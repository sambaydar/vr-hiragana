using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator animator;
    public Vector3 LeePosition;
    public Quaternion LeeRotation;
    public GameObject Lee;
    void Start()
    {
        animator = GetComponent<Animator>(); 
        LeePosition = Lee.transform.position;
        LeeRotation = Lee.transform.rotation;
    }

    public void CallOutAnimation() {
        animator.SetTrigger("CallOut");
        Lee.transform.position = LeePosition;
        Lee.transform.rotation = LeeRotation;
    }

    public void ApprovalAnimation() {
        animator.SetTrigger("Approval");
        Lee.transform.position = LeePosition;
        Lee.transform.rotation = LeeRotation;
    }

    public void RejectionAnimation()
    {
        animator.SetTrigger("Rejection");
        Lee.transform.position = LeePosition;
        Lee.transform.rotation = LeeRotation;
    }


}
