using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarFighter;
using static Assets.Scripts.ClassLib._StaticData;

public class Player_Skill_StardustOverload : Player_SkillObject
{
    public GameObject stardustTurretPrefab;

    Player_Skill_StardustTurret stardustTurret_L, stardustTurret_R;
    Text skillChargeCountText;

    private void Awake()
    {
        SkillObjectAwake();
    }

    private void Start()
    {
        skillChargeCountText = SkillObjectStart();
        stardustTurret_L = Instantiate(stardustTurretPrefab, transform.position, Quaternion.identity).GetComponent<Player_Skill_StardustTurret>();
        stardustTurret_L.setPlayer(GameObject.FindGameObjectWithTag(playerFighterTag), new Vector3(-0.7f, 0, 0), skillData);
        stardustTurret_R = Instantiate(stardustTurretPrefab, transform.position, Quaternion.identity).GetComponent<Player_Skill_StardustTurret>();
        stardustTurret_R.setPlayer(GameObject.FindGameObjectWithTag(playerFighterTag), new Vector3(0.7f, 0, 0), skillData);
    }

    private void Update()
    {
        if (skillData.hasAvaliableCharge() && Input.GetMouseButton(MouseClick[LeftClick]) && !skillData.isOnActive())
        {
            stardustTurret_L.putToOverload();
            stardustTurret_R.putToOverload();
            skillData.activateSkill();
        }
    }

    private void FixedUpdate()
    {
        skillData.countSkill(Time.fixedDeltaTime);
        skillChargeCountText.text = "x" + skillData.getCurCharge();
    }
}
