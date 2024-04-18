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
    public event Action OnDie;

    [SerializeField]
    protected BaseCharacterController characterController;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if(currentHealth <= 0)
        {
            isDead = true;
            currentHealth = 0;
            OnDie?.Invoke();
            gameObject.SetActive(false);
            Destroy(gameObject,1);  
        }

        HealthChanged?.Invoke(currentHealth, maxHealth);
    }

    protected virtual void Awake()
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
