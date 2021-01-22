using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;

public class Player_SkillChargeBullet : Common_BulletObject
{
    public int linearVelocity;
    public float shotAngle;

    Skill playerSkill;

    private void Awake()
    {
        BulletObjectAwake();
        bullet.velocity = linearVelocity * new Vector2(Mathf.Cos(shotAngle * Mathf.Deg2Rad), Mathf.Sin(shotAngle * Mathf.Deg2Rad));
        transform.Rotate(new Vector3(0, 0, shotAngle - 90));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject target = collision.gameObject;
        if (target.tag == hitTarget)
        {
            if(playerSkill != null)
            {
                playerSkill.countSkill(1.0f);
            }
        }
        BulletObjectOnCollisionEnter2D(collision, target);
    }

    public void setPlayerSkill(Player_SkillObject playerSkill)
    {
        this.playerSkill = playerSkill.getSkillData();
    }

    public void setPlayerSkill(Skill playerSkillData)
    {
        playerSkill = playerSkillData;
    }

    public void setShotVelocity(float shotAngle)
    {
        bullet.velocity = linearVelocity * new Vector2(Mathf.Cos(shotAngle * Mathf.Deg2Rad), Mathf.Sin(shotAngle * Mathf.Deg2Rad));
    }
}
