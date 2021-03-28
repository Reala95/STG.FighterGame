using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;
using static Assets.Scripts.ClassLib._StaticData;

public class Player_SkillChargeWeapon : Common_WeaponObject
{
    // Class for player weaponObjects who firing skillChargeBullets

    Player_SkillObject playerSkill;

    // Awake method for loading each level's weapon json file
    private void Awake()
    {
        WeaponObjectAwake();
        playerSkill = GameObject.FindGameObjectWithTag(playerSkillTag).GetComponent<Player_SkillObject>();
    }

    // Because this class has some different than the original parent class, so it doesn't use the protected fixedUpdate method
    private void FixedUpdate()
    {
        
        if (!weaponDatas[weaponLevel].isOnFire())
        {
            weaponDatas[weaponLevel].countWeapon();   
        }
        else
        {
            // Setup the fired bullet set
            GameObject bulletSetObject;
            if (isFireWithRotation)
            {
                bulletSetObject = Instantiate(bulletPrefabs[weaponLevel], transform.position, transform.rotation);
            }
            else
            {
                bulletSetObject = Instantiate(bulletPrefabs[weaponLevel], transform.position, Quaternion.identity);
            }
            // Assign the skillObject script to each bulletObject in the bullet set
            for (int i = 0; i < bulletSetObject.transform.childCount; i++)
            {
                bulletSetObject.transform.GetChild(i).GetComponent<Player_SkillChargeBullet>().setPlayerSkill(playerSkill);
            }
            weaponDatas[weaponLevel].Fire();
        }
    }
}
