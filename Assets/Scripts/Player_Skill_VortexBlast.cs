using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;
using static Assets.Scripts.ClassLib._StaticData;

public class Player_Skill_VortexBlast : Player_SkillObject
{
    public GameObject skillBulletPrefab;
    public TextAsset skillWeaponJson;

    Player_SkillChargeWeapon playerWeapon;
    Weapon skillWeaponData;

    private void Awake()
    {
        SkillObjectAwake();
        skillWeaponData = JsonUtility.FromJson<Weapon>(skillWeaponJson.text);
        skillWeaponData.initialize();
    }

    private void Start()
    {
        SkillObjectStart();
        playerWeapon = GameObject.FindGameObjectWithTag(playerWeaponTag).GetComponent<Player_SkillChargeWeapon>();
    }

    private void FixedUpdate()
    {
        skillData.countSkill(0.0f);
        if (skillData.isOnReady())
        {
            skillData.activateSkill();
            playerWeapon.setWeaponDisabled(true);
            skillWeaponData.initialize();
            skillWeaponData.disabled = false;
        }
        else if (!skillWeaponData.disabled)
        {
            if (!skillWeaponData.isOnFire())
            {
                if (!skillWeaponData.isOnEnd())
                {
                    skillWeaponData.countWeapon();
                }
                else
                {
                    skillData.deactivateSkill();
                    playerWeapon.initializeWeapons();
                    playerWeapon.setWeaponDisabled(false);
                    skillWeaponData.disabled = true;
                }
            }
            else
            {
                Instantiate(skillBulletPrefab, transform.position, transform.rotation);
                skillWeaponData.Fire();
            }
        }
    }
}
