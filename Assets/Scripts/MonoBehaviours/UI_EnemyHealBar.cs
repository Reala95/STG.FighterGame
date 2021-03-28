using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;

public class UI_EnemyHealBar : MonoBehaviour
{
    GameObject parentObject;
    GameObject enemyFighterObject;
    Vector3 fixedHealBarPos;
    Fighter enemyFighterData;

    private void Awake()
    {
        parentObject = transform.parent.gameObject;
    }

    private void Update()
    {
        if(enemyFighterObject != null)
        {
            parentObject.transform.position = enemyFighterObject.transform.position + fixedHealBarPos;
            if (enemyFighterData != null)
            {
                transform.localScale = new Vector3(enemyFighterData.getHealPercentage(), 1, 1);
            }
        }
        else 
        {
            Destroy(parentObject);
        }
    }

    private void OnEnable()
    {
        if(enemyFighterObject == null)
        {
            Destroy(parentObject);
        }
    }

    public void fixedPosWithSprite(SpriteRenderer enemyFighterSprite)
    {
        fixedHealBarPos = new Vector3(0, (enemyFighterSprite.bounds.size.y / 2.5f), 0);
    }

    public void setEnemyFighterData(GameObject enemyFighterObject, Fighter enemyFighterData)
    {
        this.enemyFighterObject = enemyFighterObject;
        parentObject.transform.localScale = new Vector3(enemyFighterObject.transform.localScale.x, 1, 1);
        this.enemyFighterData = enemyFighterData;
    }
}
