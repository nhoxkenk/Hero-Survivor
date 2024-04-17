using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : BaseCharacterCombat
{
    public override void Attack(BaseCharacterStat targetStats)
    {
        if(attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            OnAttackTarget();

            attackCooldown = 1f / attackSpeed;
            InCombat = true;
        }
    }
}
