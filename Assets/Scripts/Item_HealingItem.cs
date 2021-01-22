using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.ClassLib._StaticData;

public class Item_HealingItem : MonoBehaviour
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
        if(target.tag == playerFighterTag)
        {
            target.GetComponent<Player_PlayerFighterObject>().getFighterData().heal();
            target.GetComponent<Player_PlayerFighterObject>().activateSafeShield();
            Destroy(gameObject);
        }
    }
}
