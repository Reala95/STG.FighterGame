using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;

public class Common_StraightLineBullet : Common_BulletObject
{
    // The bulletObject's MonoBehaviour for the bullets that shot with from spcify angle and speed, and it runs in a straight line

    public float linearVelocity;
    public float shotAngle;

    private void Awake()
    {
        BulletObjectAwake();
        bullet.velocity = linearVelocity * new Vector2(Mathf.Cos(shotAngle * Mathf.Deg2Rad), Mathf.Sin(shotAngle * Mathf.Deg2Rad));
        transform.Rotate(new Vector3(0, 0, shotAngle - 90));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BulletObjectOnCollisionEnter2D(collision, null);
    }

    public void setShotVelocity(float shotAngle)
    {
        bullet.velocity = linearVelocity * new Vector2(Mathf.Cos(shotAngle * Mathf.Deg2Rad), Mathf.Sin(shotAngle * Mathf.Deg2Rad));
    }
}
