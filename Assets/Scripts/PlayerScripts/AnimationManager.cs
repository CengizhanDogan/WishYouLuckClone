using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetRunAnimation(bool _isRunning)
    {
        animator.SetBool("isRunning", _isRunning);
    }
}
