using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    public PlayerMovement movement;
    public PlayerAttack attackbehavior;
    public Player player;
    public Transform transformSpine;
    public Animator animator;

    private Vector3 originalScale;
    private Vector3 flipped;
    

    private void Start()
    {
        movement.OnFlip += Flip;
        
        attackbehavior.OnAttack += HandleAttack;

        originalScale = transformSpine.localScale;

        flipped = new Vector3(originalScale.x * -1, originalScale.y, originalScale.z);

    }


    private void Update()
    {
        if (player.CurrentStatus == PlayerStatus.WALKING)
        {
            animator.SetFloat("WALK", Mathf.Clamp01(movement.Speed.magnitude));
        };
    }

    private void  Flip(bool flip)
    {
        if (flip)
            transformSpine.localScale = originalScale;
        else
            transformSpine.localScale = flipped;
    }



    private void HandleAttack(AttackType attack) 
    {
        if (attack == AttackType.ATTACK_END) return;    
        animator.SetTrigger(attack.ToString());
    }

}