using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.ClassLib._StaticData;

public class Player_Skill_SolarShield : Player_SkillObject
{
    public GameObject solarShield;

    Common_WeaponObject playerWeapon;
    Player_Skill_SolarShieldObject solarShieldOnActive;
    bool isShieldOnActive = false;

    private void Awake()
    {
        SkillObjectAwake();
    }

    private void Start()
    {
        SkillObjectStart();
        playerWeapon = GameObject.FindGameObjectWithTag(playerWeaponTag).GetComponent<Common_WeaponObject>();
    }

    private void FixedUpdate()
    {
        skillData.countSkill(Time.fixedDeltaTime);
        if(skillData.isOnReady() && Input.GetMouseButtonDown(MouseClick[LeftClick]))
        {
            skillData.activateSkill();
            playerWeapon.setWeaponDisabled(true);
            solarShieldOnActive = Instantiate(solarShield, transform.position, Quaternion.identity).GetComponent<Player_Skill_SolarShieldObject>();
            isShieldOnActive = true;
        }
        else if(!skillData.isOnActive() && isShieldOnActive)
        {
            solarShieldOnActive.putToEnd();
            playerWeapon.setWeaponDisabled(false);
            isShieldOnActive = false;
        }
    }
}
