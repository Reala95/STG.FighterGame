using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_EnemyFighterObject : Common_FighterObject
{
    public GameObject[] lastwordObjects;
    public bool hasLastwordObjects;

    GameObject healBarObject = null;
    UI_EnemyHealBar enemyHealBar = null;

    private void Awake()
    {
        FighterObjectAwake();

        healBarObject = transform.Find("Enemy_UI_EnemyHealBar").gameObject;
        if(healBarObject != null)
        {
            enemyHealBar = healBarObject.transform.Find("EnemyHealBarContainer").gameObject.GetComponent<UI_EnemyHealBar>();
            enemyHealBar.enemyFighterData = fighterData;
            enemyHealBar.fixedPosWithSprite(GetComponent<SpriteRenderer>());
            healBarObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (fighterData.getHealPercentage() < 1 && healBarObject != null && !healBarObject.activeSelf)
        {
            healBarObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if(!fighterData.isAlive() && hasLastwordObjects)
        {
            foreach(GameObject go in lastwordObjects)
            {
                Instantiate(go, transform.position, Quaternion.identity);
            }
        }
        FighterObjectFixedUpdate();
    }
}
