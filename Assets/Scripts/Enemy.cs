using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{

    public int HP { get => hp; set => hp = value; }

    [SerializeField]
    private EnemyAnimationHandler enemyAnimationHandler;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Player player;
    [SerializeField]
    private float speed;
    private EnemyStatus currenStatus;
    private int hp;


    private void Update()
    {
        agent.Move((player.transform.position - transform.position).normalized * speed);
    }

    private void ChangeStatus(EnemyStatus newStatus)
    { 
        currenStatus = newStatus;
    }
    public void Hurt(AttackType attack)
    {
        
    }
}

public enum EnemyStatus
{
    IDLE,
    ATTACKING,
    MOVING,
    HURT
}
