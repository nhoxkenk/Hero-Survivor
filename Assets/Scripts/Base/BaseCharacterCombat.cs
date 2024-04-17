using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterCombat : MonoBehaviour
{
    [Header("Variables")]
    public float attackSpeed = 1f;
    [SerializeField] protected float attackCooldown = 0f;
    protected float attackDelay = 0.6f;
    public bool InCombat {  get; protected set; }
    public event Action OnAttack;

    protected BaseCharacterStat characterStat;

    protected virtual void Awake()
    {
        characterStat = GetComponent<BaseCharacterStat>();
    }

    protected virtual void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public abstract void Attack(BaseCharacterStat targetStats);

    protected IEnumerator DoDamage(BaseCharacterStat stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(characterStat.damage);
        if (stats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }

    public void OnAttackTarget()
    {
        OnAttack?.Invoke();
    }
}
