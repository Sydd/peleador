using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerMovement movementBehavior;
    [SerializeField]
    PlayerAttack playerAttack;
    PlayerStatus currentStatus;
    int lastLean = -1;

    public PlayerStatus CurrentStatus { get => currentStatus; }

    private void Start()
    {
        ChangeStatus(PlayerStatus.WALKING);
    }

    private void ChangeStatus(PlayerStatus a)
    {
        if (currentStatus == a) return;
        
        currentStatus = a;
    }

}


public enum PlayerStatus
{
    ATTACKING,
    WALKING,
    GETTING_HURT
}