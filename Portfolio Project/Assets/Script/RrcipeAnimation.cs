using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RrcipeAnimation : MonoBehaviour
{
    private Animator animator = null;

    private bool doAnimation = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void DoAnimation()
    {
        doAnimation = !doAnimation;
        if (doAnimation)
            animator.SetTrigger("RrcipeUp");
        else
            animator.SetTrigger("RrcipeDown");
    }
}
