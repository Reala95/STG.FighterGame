using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;
using static Assets.Scripts.ClassLib._StaticData;

public class Player_SafeShield : MonoBehaviour
{
    Player_PlayerFighterObject playerFighter;
    const float safeShieldDuration = 3.0f;
    float curSafeShieldDuration = 3.0f;
    bool shieldCountDownEnable = false;

    const int safeShieldType = 0;

    private void Start()
    {
        playerFighter = GameObject.FindGameObjectWithTag(playerFighterTag).GetComponent<Player_PlayerFighterObject>();
    }

    private void FixedUpdate()
    {
        if (shieldCountDownEnable)
        {
            if (curSafeShieldDuration > 0)
            {
                curSafeShieldDuration -= Time.fixedDeltaTime;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        curSafeShieldDuration = safeShieldDuration;
    }

    private void OnDisable()
    {
        playerFighter.setShieldBools(false, safeShieldType);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject target = collision.gameObject;
        if(target.tag == bulletEnemyTag)
        {
            Destroy(target);
        }
    }

    public void enableCountDown()
    {
        shieldCountDownEnable = true;
    }
}
