using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.ClassLib._StaticData;

public class Enemy_AimmingWeaponObject : Common_WeaponObject
{
    public float aimAccuracy;

    GameObject targetPlayer;

    private void Awake()
    {
        WeaponObjectAwake();
    }

    private void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag(playerFighterTag);
    }

    private void FixedUpdate()
    {
        WeaponObjectFixedUpdate();
    }

    private void LateUpdate()
    {
        if (targetPlayer != null)
        {
            Vector3 distanceVector = targetPlayer.transform.position - transform.position;
            Quaternion q = Quaternion.AngleAxis(Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, aimAccuracy);
        }
        else 
        {
            targetPlayer = GameObject.FindGameObjectWithTag(playerFighterTag);
            if (targetPlayer == null)
            {
                Quaternion q = Quaternion.AngleAxis(0, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, aimAccuracy);
            }
        }
    }
}
