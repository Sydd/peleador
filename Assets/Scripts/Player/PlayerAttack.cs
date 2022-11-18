using Cysharp.Threading.Tasks;
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

    private bool attacking = false;
    private int attackCounter;
    private float _elapsedTime;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
    }

    public async void Attack()
    {
        if (attacking) return;

        attacking = true;

        _elapsedTime = 0;

        BasicAttack();

        int result = await UniTask.WhenAny(UniTask.Delay(500), UniTask.WaitUntil(() => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl)));

        if (result == 0)
        {
            EndAttack();
            return;
        }

        _elapsedTime = 0;

        ComboAttack();

        await UniTask.WaitUntil(() => _elapsedTime > .5f);

        EndAttack();
    }

    private void EndAttack()
    {
        attacking = false;
        OnAttack?.Invoke(AttackType.ATTACK_END);
    }

    private async void BasicAttack()
    {
        await UniTask.WaitUntil(() => _elapsedTime > .25f);

        var enemiesTouched = Physics.OverlapSphere(transform.position + meleePositionOffSet, basicAttackRadius, whatIsEnemy);


        for (int i = 0; i < enemiesTouched.Length; i++)
        {
            enemiesTouched[i].GetComponent<Enemy>().Hurt(AttackType.ATTACK_BASIC);
        }

        await UniTask.WaitUntil(() => _elapsedTime > .25f);

        OnAttack?.Invoke(AttackType.ATTACK_BASIC);
    }

    private void ComboAttack()
    {
        var enemiesTouched = Physics.OverlapSphere(transform.position + meleePositionOffSet, basicAttackRadius * 2, whatIsEnemy);

        for (int i = 0; i < enemiesTouched.Length; i++)
        {
            enemiesTouched[i].GetComponent<Enemy>().Hurt(AttackType.ATTACK_COMBO);
        }

        OnAttack?.Invoke(AttackType.ATTACK_COMBO);
    }
    public void Flip(bool right)
    {
        if (right) meleePositionOffSet.x = 1;
        else meleePositionOffSet.x = -1;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + meleePositionOffSet, basicAttackRadius);
    }
}

public enum AttackType
{
    ATTACK_BASIC,
    ATTACk_ALT,
    ATTACK_COMBO,
    ATTACK_END
}