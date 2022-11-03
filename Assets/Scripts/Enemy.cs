using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int HP { get => hp; set => hp = value; }

    [SerializeField]
    private EnemyAnimationHandler animationHandler;

    [SerializeField]
    private Player player;

    [SerializeField]
    private float speed;

    private EnemyStatus currenStatus;
    private int hp;
    private float elapsedTime;
    private bool _attacking;

    private void Start()
    {
        player = Object.FindObjectOfType<Player>();
    }

    private async void Update()
    {
        elapsedTime += Time.deltaTime;

        if (currenStatus == EnemyStatus.ATTACKING) return;

        if (Vector3.Distance(player.transform.position, transform.position) > 1f)
        {
            Move();
            return;
        }

        if (Vector3.Distance(player.transform.position, transform.position) < 1f)
        {
            Attack();
        }
    }

    private void CheckStatus()
    {
    }

    private void Move()
    {
    }

    private async void Attack()
    {
        ChangeStatus(EnemyStatus.ATTACKING);

        animationHandler.Attack();

        await UniTask.WaitUntil(() => elapsedTime > 1f);

        ChangeStatus(EnemyStatus.MOVING);
    }

    private void ChangeStatus(EnemyStatus newStatus)
    {
        currenStatus = newStatus;
    }

    public void Hurt(AttackType attack)
    {
        if (attack == AttackType.ATTACK_BASIC)
        {
            animationHandler.Hurt();
        }
    }
}

public enum EnemyStatus
{
    IDLE,
    ATTACKING,
    MOVING,
    HURT
}