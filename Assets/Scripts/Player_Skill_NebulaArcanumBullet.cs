using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;
using static Assets.Scripts.ClassLib._StaticData;

public class Player_Skill_NebulaArcanumBullet : Common_BulletObject
{
    enum BulletState
    {
        onLoad,
        onStart,
        onActive,
        onEnd
    }

    public float maxLinearVelocity;
    public float startLinearAcceleration;
    public float endLinearAcceleration;
    public float shotAngle;
    public float maxFlyingDistance;

    BulletState state = BulletState.onLoad;
    Vector3 startPos;
    Fighter playerFighter;
    float curLinearVelocity = 0;

    public void setUpBullet(float shotAngle, float maxFlyingDistance)
    {
        this.shotAngle = shotAngle;
        this.maxFlyingDistance = maxFlyingDistance;
        transform.Rotate(new Vector3(0, 0, shotAngle - 90));
    }

    private void Awake()
    {
        BulletObjectAwake();
    }

    private void Start()
    {
        startPos = transform.position;
        GameObject playerFighterObject = GameObject.FindGameObjectWithTag(playerFighterTag);
        if (playerFighterObject != null)
        {
            playerFighter = GameObject.FindGameObjectWithTag(playerFighterTag).GetComponent<Player_PlayerFighterObject>().getFighterData();
        }
        else
        {
            putToEnd();
        }
    }

    // Overriding the update method in the parent "Common_BulletObject" class
    private void Update()
    {
        if (transform.position.x >= 14 || transform.position.x <= -14 || transform.position.y >= 10 || transform.position.y <= -10)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case BulletState.onActive:
                curLinearVelocity = Mathf.Min(curLinearVelocity + startLinearAcceleration, maxLinearVelocity);
                bullet.velocity = curLinearVelocity * new Vector2(Mathf.Cos(shotAngle * Mathf.Deg2Rad), Mathf.Sin(shotAngle * Mathf.Deg2Rad));
                if (Vector3.Distance(transform.position, startPos) >= maxFlyingDistance)
                {
                    putToEnd();
                }
                break;
            case BulletState.onEnd:
                curLinearVelocity = Mathf.Max(curLinearVelocity - endLinearAcceleration, 0);
                bullet.velocity = curLinearVelocity * new Vector2(Mathf.Cos(shotAngle * Mathf.Deg2Rad), Mathf.Sin(shotAngle * Mathf.Deg2Rad));
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(state == BulletState.onActive)
        {
            GameObject target = collision.gameObject;
            if (target.tag == hitTarget)
            {
                if(playerFighter != null)
                {
                    playerFighter.heal(Mathf.Min(bulletData.MakeDamage(target) / 2));
                }
                putToEnd();
            }
        }
        
    }

    public bool isOnStart()
    {
        return state == BulletState.onStart;
    }

    public bool isOnEnd()
    {
        return state == BulletState.onEnd;
    }

    public void putToStart()
    {
        state = BulletState.onStart;
    }

    public void putToActive()
    {
        state = BulletState.onActive;
    }

    public void putToEnd()
    {
        state = BulletState.onEnd;
    }
}
