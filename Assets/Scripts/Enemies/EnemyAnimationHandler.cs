using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Attack()
    {
        animator.SetTrigger("ATTACK");
    }
    public void Hurt()
    {
        animator.SetTrigger("HURT");
    } 
}