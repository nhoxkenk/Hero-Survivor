using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : BaseCharacterController
{
    [Header("Components")]
    public NavMeshAgent agent;

    [Header("Variables")]
    [SerializeField] private bool hasCollided;
    [SerializeField] private float miniumDistance = 2.5f;
    [SerializeField] private float countDownTime = 1f;
    [SerializeField] private bool isCounting;

    [Header("Target")]
    [SerializeField] Transform targetTransform;

    private EnemyAnimator enemyAnimator;
    private EnemyCombat enemyCombat;

    protected override void Awake()
    {
        base.Awake();
        SetupCharacter();
    }

    private void Update()
    {
        CharacterMovement();
    }

    public override void CharacterMovement()
    {
        if(targetTransform == null)
        {
            agent.destination = transform.position;
            enemyAnimator.SetAttackAnimation(false);
            return;  
        }

        float distance = Vector3.Distance(transform.position, targetTransform.position);
        if (distance > miniumDistance)
        {
            hasCollided = false;
            ChaseTarget();
            if (distance <= agent.stoppingDistance)
            {
                //Do Damage Here
                BaseCharacterStat targetStat = targetTransform.GetComponent<BaseCharacterStat>();

                if (targetStat != null)
                {
                    agent.destination = transform.position;
                    enemyAnimator.SetAttackAnimation(true);
                    enemyCombat.Attack(targetStat);
                }

                FaceTarget();
            }
            else
            {
                enemyAnimator.SetAttackAnimation(false);
            }
        }
    }

    public override void SetupCharacter()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<EnemyAnimator>();
        enemyCombat = GetComponent<EnemyCombat>();
        targetTransform = FindObjectOfType<PlayerController>().transform;
    }

    private void ChaseTarget()
    {
        agent.destination = targetTransform.position;
        float speedPercent = agent.velocity.magnitude / agent.speed;
        enemyAnimator.UpdateMovementAnimation(speedPercent);
    }

    private void FaceTarget()
    {
        Vector3 direction = (targetTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
