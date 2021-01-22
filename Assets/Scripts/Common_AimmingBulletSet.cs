using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common_AimmingBulletSet : Common_BulletSetObject
{
    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.GetComponent<Common_StraightLineBullet>() != null)
            {
                Common_StraightLineBullet bulletScript = transform.GetChild(i).gameObject.GetComponent<Common_StraightLineBullet>();
                bulletScript.setShotVelocity(transform.eulerAngles.z + bulletScript.shotAngle);
            }
            else
            {
                Player_SkillChargeBullet bulletScript = transform.GetChild(i).gameObject.GetComponent<Player_SkillChargeBullet>();
                bulletScript.setShotVelocity(transform.eulerAngles.z + bulletScript.shotAngle);
            }
        }
    }


}
