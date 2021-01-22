using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;

public class UI_EnemyHealBar : MonoBehaviour
{
    public Fighter enemyFighterData; 

    private void Update()
    {
        if(enemyFighterData != null)
        {
            transform.localScale = new Vector3(enemyFighterData.getHealPercentage(), 1, 1);
        }
    }

    public void fixedPosWithSprite(SpriteRenderer enemyFighterSprite)
    {
        GameObject parentObject = transform.parent.gameObject;
        parentObject.transform.localPosition = new Vector3(0, (enemyFighterSprite.bounds.size.y / 2.0f), 0);
    }
}
