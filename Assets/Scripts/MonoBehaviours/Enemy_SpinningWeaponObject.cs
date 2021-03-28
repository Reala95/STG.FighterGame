using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SpinningWeaponObject : Common_WeaponObject
{
    public float rotateRatio;

    float curRotateRatio = 0;

    private void Awake()
    {
        WeaponObjectAwake();
    }

    private void FixedUpdate()
    {
        if (!weaponDatas[weaponLevel].isOnFire())
        {
            weaponDatas[weaponLevel].countWeapon();
        }
        else if (weaponDatas[weaponLevel].isOnFire())
        {
            Quaternion shotAngle = Quaternion.AngleAxis(curRotateRatio, Vector3.forward);
            if (isFireWithRotation)
            {
                Instantiate(bulletPrefabs[weaponLevel], transform.position, transform.rotation * shotAngle);
            }
            else
            {
                Instantiate(bulletPrefabs[weaponLevel], transform.position, Quaternion.identity * shotAngle);
            }
            weaponDatas[weaponLevel].Fire();
            curRotateRatio += rotateRatio;
        } 
        else if (weaponDatas[weaponLevel].isOnReload() && curRotateRatio != 0)
        {
            curRotateRatio = 0;
        }
    }
}
