using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ClassLib;

public class Enemy_EnemyFighterObject : Common_FighterObject
{
    public GameObject healBarPrefab;
    public GameObject[] lastwordObjects;
    public bool usingHealBar = true;
    public bool hasLastwordObjects;

    GameObject healBarObject;
    UI_EnemyHealBar enemyHealBar;

    private void Awake()
    {
        FighterObjectAwake();
    }

    private void Start()
    {
        // Set up EnemyHealBar
        if (usingHealBar)
        {
            healBarObject = Instantiate(healBarPrefab, transform.position, Quaternion.identity);
            enemyHealBar = healBarObject.transform.Find("EnemyHealBarContainer").gameObject.GetComponent<UI_EnemyHealBar>();
            enemyHealBar.setEnemyFighterData(gameObject, fighterData);
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
        if (transform.position.x >= 20 || transform.position.x <= -20 || transform.position.y >= 16 || transform.position.y <= -16)
        {
            Destroy(gameObject);
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

    private void OnDestroy()
    {
        if(healBarObject != null)
        {
            healBarObject.SetActive(true);
        }
    }
}
