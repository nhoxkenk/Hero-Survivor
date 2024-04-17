using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform targetTransform;
    [SerializeField] protected LayerMask activeEffect;
    [SerializeField] protected float radiusEffect;

    [Header("References")]
    public BaseCharacterController characterController;

    public abstract void WeaponMoving();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (damageable != null)
            {
                float damage = characterController.characterData.characterDamage;
                damageable.TakeDamage(damage);
                if (weaponData.weaponType != WeaponType.Around)
                {
                    Destroy(gameObject);
                }
            }
        }
        if(weaponData.weaponType != WeaponType.Around)
        {
            Destroy(gameObject);
        }   
    }

}
