using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : BaseWeapon
{
    [SerializeField] private bool homing;
    [SerializeField] private float aliveTimer = 3.0f;

    private void Awake()
    {
        weaponData = GetComponent<WeaponData>();
    }

    private void Update()
    {
        WeaponMoving();
    }

    public override void WeaponMoving()
    {
            switch (weaponData.weaponType)
            {
                case WeaponType.Continues:
                {
                    if (homing && targetTransform != null)
                    {
                        Vector3 moveDirection = (targetTransform.transform.position - gameObject.transform.position).normalized;
                        transform.position += moveDirection * weaponData.weaponSpeed * Time.deltaTime;
                        transform.LookAt(targetTransform);
                    }
                    break;
                }

            case WeaponType.Around:
                    {
                        //Logic for weapon here
                        if(!homing) 
                            return;
                        Vector3 center = characterController.transform.position;
                        float angle = Time.deltaTime * weaponData.weaponSpeed;
                        
                        transform.RotateAround(center, Vector3.up * 5, angle);

                        if (Mathf.Abs(transform.eulerAngles.y) >= 360.0f)
                        {
                            Destroy(gameObject);
                        }
                        break;
                    }
            }
    }

    public void Fire(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }
}
