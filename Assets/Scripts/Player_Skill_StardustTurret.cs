using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;
using static Assets.Scripts.ClassLib._StaticData;

public class Player_Skill_StardustTurret : MonoBehaviour
{
    enum TurretState
    {
        onNormal,
        onOverload,
    }

    public GameObject[] bulletPrefabs;
    public TextAsset normalWeaponJson;
    public TextAsset overloadWeaponJson;
    public float aimAccuracy;

    TurretState state = TurretState.onNormal;
    GameObject lockedTarget;
    Weapon normalWeapon;
    Weapon overloadWeapon;

    GameObject playerFighter;
    Vector3 fixedFollowingVector3;
    Skill playerSkillData;
    int playerWeaponLevel = 0;

    private void Awake()
    {
        normalWeapon = JsonUtility.FromJson<Weapon>(normalWeaponJson.text);
        overloadWeapon = JsonUtility.FromJson<Weapon>(overloadWeaponJson.text);
        normalWeapon.initialize();
        normalWeapon.disabled = false;
        overloadWeapon.initialize();
    }

    private void Update()
    {
        if(playerFighter != null)
        {
            playerWeaponLevel = GameObject.FindGameObjectWithTag(playerWeaponTag).GetComponent<Common_WeaponObject>().getWeaponLevel();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        // Following playerFighter
        if(playerFighter != null)
        {
            transform.position = playerFighter.transform.position + fixedFollowingVector3;
        }

        // part of turret target search and turn to target
        GameObject[] targetList = GameObject.FindGameObjectsWithTag(enemyFighterTag);
        if(targetList.Length != 0)
        {
            lockedTarget = getNearestObject(targetList);
        }
        if(lockedTarget != null)
        {
            Vector3 distanceVector = lockedTarget.transform.position - transform.position;
            Quaternion q = Quaternion.AngleAxis(Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, aimAccuracy);
        }
        else
        {
            Quaternion q = Quaternion.AngleAxis(0, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, aimAccuracy);
        }

        // part of counting down weapon datas and firing
        switch (state)
        {
            case TurretState.onNormal:
                if (!normalWeapon.isOnFire())
                {
                    normalWeapon.countWeapon();
                }
                else
                {
                    GameObject bulletSetObject;
                    bulletSetObject = Instantiate(bulletPrefabs[playerWeaponLevel], transform.position, transform.rotation);
                    // Assign the skillObject script to each bulletObject in the bullet set
                    for (int i = 0; i < bulletSetObject.transform.childCount; i++)
                    {
                        bulletSetObject.transform.GetChild(i).GetComponent<Player_SkillChargeBullet>().setPlayerSkill(playerSkillData);
                    }
                    normalWeapon.Fire();
                }
                break;
            case TurretState.onOverload:
                if (!overloadWeapon.isOnFire())
                {
                    overloadWeapon.countWeapon();
                }
                else
                {
                    Instantiate(bulletPrefabs[playerWeaponLevel], transform.position, transform.rotation);
                    overloadWeapon.Fire();
                }
                if (overloadWeapon.isOnEnd())
                {
                    
                    playerSkillData.deactivateSkill();
                    normalWeapon.initialize();
                    overloadWeapon.initialize();
                    state = TurretState.onNormal;
                }
                break;
        }
    }

    public void setPlayer(GameObject playerFighter, Vector3 fixedFollowingVector3, Skill playerSkillData)
    {
        this.playerFighter = playerFighter;
        this.fixedFollowingVector3 = fixedFollowingVector3;
        this.playerSkillData = playerSkillData;
    }

    public void putToOverload()
    {
        state = TurretState.onOverload;
    }

    private GameObject getNearestObject(GameObject[] goList)
    {
        if (goList.Length == 0)
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
