using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;
using static Assets.Scripts.ClassLib._StaticData;

public class Player_Skill_SolarShieldObject : MonoBehaviour
{
    enum ShieldState
    {
        onActive,
        onEnd
    }

    GameObject solarFighter;
    Fighter solarFighterData;
    ShieldState state = ShieldState.onActive;

    const int solorShieldType = 1;

    private void Start()
    {
        solarFighter = GameObject.FindGameObjectWithTag(playerFighterTag);
        solarFighterData = solarFighter.GetComponent<Common_FighterObject>().getFighterData();
        solarFighter.GetComponent<Player_PlayerFighterObject>().setShieldBools(true, solorShieldType);
    }

    private void Update()
    {
        if(transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position = solarFighter.transform.position;
        if(state == ShieldState.onActive)
        {
            solarFighterData.heal(1);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject target = collision.gameObject;
        if(target.tag == bulletEnemyTag)
        {
            Destroy(target);
        }
    }

    public void putToEnd()
    {
        solarFighter.GetComponent<Player_PlayerFighterObject>().setShieldBools(false, solorShieldType);
        state = ShieldState.onEnd;
    }

    public bool isOnEnd()
    { 
        return state == ShieldState.onEnd;
    }
}
