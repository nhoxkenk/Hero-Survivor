using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterStat : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    protected float maxHealth;
    protected float currentHealth;
    protected bool isDead;
    public event Action<float, float> HealthChanged;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        HealthChanged?.Invoke(currentHealth, maxHealth);
        if(currentHealth <= 0)
        {
            isDead = true;
            currentHealth = 0;
            Destroy(gameObject);
        }
    }

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual bool Die()
    {
        return isDead;
    }
}
