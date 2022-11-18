using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _movementBehavior;

    [SerializeField]
    private PlayerAttack _attackBehavior;

    private PlayerStatus currentStatus;

    public PlayerStatus CurrentStatus { get => currentStatus; }

    private void Start()
    {
        ChangeStatus(PlayerStatus.WALKING);
        _attackBehavior.OnAttack += ChangeStatusAttack;
        _movementBehavior.OnFlip += OnFlip;
    }

    private void OnFlip(bool flip)
    {
        _attackBehavior.Flip(flip);
    }

    public void ChangeStatus(PlayerStatus a)
    {
        if (currentStatus == a) return;

        currentStatus = a;
    }

    private void ChangeStatusAttack(AttackType attack)
    {
        if (attack == AttackType.ATTACK_END)
        {
            _movementBehavior.CanMove = true;
            ChangeStatus(PlayerStatus.WALKING);
            return;
        }

        _movementBehavior.CanMove = false;
        ChangeStatus(PlayerStatus.ATTACKING);
    }
}

public enum PlayerStatus
{
    ATTACKING,
    WALKING,
    GETTING_HURT
}