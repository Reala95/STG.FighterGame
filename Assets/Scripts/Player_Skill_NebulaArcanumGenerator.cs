using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;

public class Player_Skill_NebulaArcanumGenerator : MonoBehaviour
{
    public GameObject bulletPrefab;
    public TextAsset generatorWeaponJson;

    GameObject followingTarget;
    Weapon generatorWeaponData;
    float endingTime = 0.16f;

    private void Awake()
    {
        generatorWeaponData = JsonUtility.FromJson<Weapon>(generatorWeaponJson.text);
        generatorWeaponData.initialize();
    }

    private void FixedUpdate()
    {
        if (followingTarget != null)
        {
            transform.position = followingTarget.transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
        if (!generatorWeaponData.isOnEnd())
        {
            if (!generatorWeaponData.isOnFire())
            {
                generatorWeaponData.countWeapon();
            }
            else
            {
                float bulletAngle = Random.Range(0f, 360f);
                float bulletDistance = Random.Range(2.0f, 3.5f);
                Player_Skill_NebulaArcanumBullet bulletScript = Instantiate(bulletPrefab, transform.position + new Vector3(bulletDistance * Mathf.Cos(bulletAngle * Mathf.Deg2Rad), bulletDistance * Mathf.Sin(bulletAngle * Mathf.Deg2Rad), 0), Quaternion.identity).GetComponent<Player_Skill_NebulaArcanumBullet>();
                bulletScript.setUpBullet(bulletAngle + 180f, bulletDistance);
                bulletScript.putToStart();
                generatorWeaponData.Fire();
            }
        }
        else
        {
            if(endingTime > 0)
            {
                endingTime -= Time.fixedDeltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void setFollowingTarget(GameObject followingTarget)
    {
        this.followingTarget = followingTarget;
    }
}
