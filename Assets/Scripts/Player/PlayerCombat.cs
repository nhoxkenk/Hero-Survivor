using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : BaseCharacterCombat
{
    private PlayerController playerController;

    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<PlayerController>();
    }

    public override void Attack(BaseCharacterStat targetStats)
    {
        var currentBullet = playerController.playerWeaponsActive;
        for (int i = 0; i < currentBullet.Count; i++)
        {
            var bulletData = currentBullet[i].GetComponent<WeaponData>();
            bulletData.weaponCurrentTimer += Time.deltaTime;

            if(bulletData.weaponCurrentTimer >= bulletData.weaponDelayTimer && targetStats != null)
            {
                FireBullet(targetStats.transform, currentBullet[i]);
                bulletData.weaponCurrentTimer = 0f;
            }
        }
    }

    private void FireBullet(Transform target, PlayerWeapon bullet)
    {
        Vector3 distanceOffset = (target.transform.position - gameObject.transform.position).normalized;
        if(bullet.GetComponent<WeaponData>().weaponType == WeaponType.Around)
        {
            distanceOffset *= 2.5f;
        }
        var tempProjectile = Instantiate(bullet, transform.position + distanceOffset, Quaternion.Euler(-90, 0, 0));
        tempProjectile.characterController = this.GetComponent<PlayerController>();
        //playerAudio.PlayOneShot(shooterSound, 1f);
        tempProjectile.GetComponent<PlayerWeapon>().Fire(target);
    }
}
