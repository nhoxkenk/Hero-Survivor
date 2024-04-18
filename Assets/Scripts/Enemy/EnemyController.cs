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
    [SerializeField] private Transform targetTransform;

    [Header("Collectable drop")]
    [SerializeField] private Collectable collectableExperience;

    private EnemyAnimator enemyAnimator;
    private EnemyCombat enemyCombat;
    private EnemyStat enemyStat;

    protected override void Awake()
    {
        base.Awake();
        SetupCharacter();
    }

    private void Update()
    {
        CharacterMovement();
    }

    private Vector3 offsetVector()
    {
        int x = Random.Range(0, 3);
        int z = Random.Range(0, 3);
        return new Vector3(x, 2, z);
    }

    private int calculatedCurrencyNumber()
    {
        return Random.Range(1, 3) * (int)enemyStat.maxHealth/10;
    }
    private void spawnCurrency()
    {
        int amount = calculatedCurrencyNumber();
        for (int i = 0; i < amount; i++)
        {
            var bouncyForce = Random.Range(0, 2.5f);
            var collectable = Instantiate(collectableExperience, transform.position + offsetVector(), Quaternion.identity);
            collectable.GetComponent<Rigidbody>().AddForce(Vector3.up * bouncyForce, ForceMode.Impulse);
            collectable.OnCollectable += UIManager.Instance.HandleOnCollect;
        }
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
        enemyStat = GetComponent<EnemyStat>();
        targetTransform = FindObjectOfType<PlayerController>().transform;

        enemyStat.OnDie += spawnCurrency;
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
