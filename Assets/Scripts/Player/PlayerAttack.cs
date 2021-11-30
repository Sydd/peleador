using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Vector3 meleePositionOffSet;
    [SerializeField]
    private float basicAttackRadius;
    [SerializeField]
    private LayerMask whatIsEnemy;

    public Action<AttackType> OnAttack;

    private bool attackEnabled = true;
    private int opAttackLeanID;
    private int attackCounter; 

    public void Attack()
    {
        if (!attackEnabled) return;

        if (attackCounter > 1) ComboAttack();

        else BasicAttack();

        attackCounter++;

        attackEnabled = false;

        LeanTween.delayedCall(Constants.BASIC_ATTACK_COOLDOWN, () => attackEnabled = true);

    }

    private void BasicAttack()
    {
        var enemiesTouched = Physics.OverlapSphere(transform.position + meleePositionOffSet, basicAttackRadius, whatIsEnemy);

        for (int i = 0; i < enemiesTouched.Length; i++)
        {
            enemiesTouched[i].GetComponent<Enemy>().Hurt(AttackType.ATTACK_BASIC);
        }

        LeanTween.cancel(opAttackLeanID);


        opAttackLeanID = LeanTween.delayedCall(Constants.TIME_TO_CHAINATTACKS, () =>
        {
            attackCounter = 0;
        }).id;


        OnAttack?.Invoke(AttackType.ATTACK_BASIC);
    }

    private void ComboAttack()
    {
        var enemiesTouched = Physics.OverlapSphere(transform.position + meleePositionOffSet, basicAttackRadius * 2, whatIsEnemy);

        for (int i = 0; i < enemiesTouched.Length; i++)
        {
            enemiesTouched[i].GetComponent<Enemy>().Hurt(AttackType.ATTACK_COMBO);
        }

        attackCounter = 0;

        OnAttack?.Invoke(AttackType.ATTACK_COMBO);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + meleePositionOffSet, basicAttackRadius);
    }
}


public enum AttackType
{
    ATTACK_BASIC,
    ATTACk_ALT,
    ATTACK_COMBO
}
