using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterStat : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public float maxHealth;
    public float currentHealth;
    public bool isDead;
    public float damage;
    public event Action<float, float> HealthChanged;

    [SerializeField]
    protected BaseCharacterController characterController;

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
        SetupStats();
    }

    private void SetupStats()
    {
        characterController = GetComponent<BaseCharacterController>();
        damage = characterController.characterData.characterDamage;
        maxHealth = characterController.characterData.characterHealth;
        currentHealth = maxHealth;
    }

    public virtual bool Die()
    {
        return isDead;
    }
}
