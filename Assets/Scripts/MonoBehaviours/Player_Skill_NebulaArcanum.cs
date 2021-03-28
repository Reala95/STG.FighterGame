using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;
using static Assets.Scripts.ClassLib._StaticData;

public class Player_Skill_NebulaArcanum : Player_SkillObject
{
    public GameObject skillGenerator;

    GameObject curSkillGenerator;

    private void Awake()
    {
        SkillObjectAwake();
    }

    private void Start()
    {
        SkillObjectStart();
        
    }

    private void FixedUpdate()
    {
        skillData.countSkill(Time.fixedDeltaTime);
        if (skillData.isOnInfinite())
        {
            if(curSkillGenerator == null)
            {
                GameObject targetList = getNearestObject(GameObject.FindGameObjectsWithTag(enemyFighterTag));
                if(targetList != null)
                {
                    curSkillGenerator = Instantiate(skillGenerator, targetList.transform.position, Quaternion.identity);
                    curSkillGenerator.GetComponent<Player_Skill_NebulaArcanumGenerator>().setFollowingTarget(targetList);
                }
            }
        }
        else if (skillData.isOnReady())
        {
            skillData.activateSkill();
        }
    }

    private void OnDestroy()
    {
        Destroy(curSkillGenerator);
    }

    private GameObject getNearestObject(GameObject[] goList)
    {
        if(goList.Length == 0)
        {
            return null;
        }
        else
        {
            GameObject nearestGO = null;
            float minDistance = Mathf.Infinity;
            foreach (GameObject go in goList)
            {
                float curDistance = Vector3.Distance(go.transform.position, transform.position);
                if (curDistance < minDistance)
                {
                    nearestGO = go;
                    minDistance = curDistance;
                }
            }
            return nearestGO;
        }
    }
}
