using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.ClassLib._StaticData;


public class Item_WeaponUp : MonoBehaviour
{
    Rigidbody2D item;

    private void Awake()
    {
        item = GetComponent<Rigidbody2D>();
        item.velocity = itemVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject target = collision.gameObject;
        if (target.tag == playerFighterTag)
        {
            Common_WeaponObject playerWeapon = GameObject.FindGameObjectWithTag(playerWeaponTag).GetComponent<Common_WeaponObject>();
            playerWeapon.weaponUp();
            Destroy(gameObject);
        }
    }
}
