using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimController : MonoBehaviour
{
    private static Animator _animator;
    
    public static void InitAnimator(Animator animator)
    {
        _animator = animator;
    }

    public static void Jump()
    {
        _animator.SetTrigger("Jump");
    }

    public static void Run(bool isRun)
    {
        _animator.SetBool("Run",isRun);
    } 
    
    public static void Die()
    {
        _animator.SetTrigger("Dead");
    }
}
