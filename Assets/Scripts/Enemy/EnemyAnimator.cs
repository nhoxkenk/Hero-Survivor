using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [Header("Component References")]
    public Animator enemyAnimator;

    private int enemyMovementAnimationID;
    private int enemyAttackAnimationID;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        SetupAnimationIDs();
    }

    private void SetupAnimationIDs()
    {
        enemyMovementAnimationID = Animator.StringToHash("Movement");
        enemyAttackAnimationID = Animator.StringToHash("Attack");
    }

    public void UpdateMovementAnimation(float movementBlendValue)
    {
        enemyAnimator.SetFloat(enemyMovementAnimationID, movementBlendValue, 0.1f, Time.deltaTime);
    }

    public void SetAttackAnimation(bool isAttacking)
    {
        enemyAnimator.SetBool(enemyAttackAnimationID, isAttacking);
    }
}
