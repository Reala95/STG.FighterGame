using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;

public class Enemy_EnemyStraightLineRouter : MonoBehaviour
{
    enum RouteState
    {
        onStart,
        onMove,
        onFire,
        onWait,
        onEnd
    }

    public TextAsset enemyRouteJson;
    public int loopStartIndex;
    public bool isHeadToDestination;
    public bool isLoopingRoute;
    public bool isFireOnEnd;
    public bool isWeaponRefreshOnEnd;
    public bool isDestroyOnEnd;

    RouteState state = RouteState.onStart;
    EnemyRouteList enemyRoute;
    Common_WeaponObject enemyWeapon;
    int curRouteIndex = 0;

    private void Awake()
    {
        if(enemyRouteJson != null)
        {
            enemyRoute = JsonUtility.FromJson<EnemyRouteList>(enemyRouteJson.text);
            transform.position = enemyRoute.initPos;
        }
    }

    private void Start()
    {
        enemyWeapon = GetComponent<Common_WeaponObject>();
        if(enemyWeapon != null)
        {
            enemyWeapon.setWeaponDisabled(true);
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case RouteState.onMove:
                if(transform.position != enemyRoute.routeList[curRouteIndex].destination)
                {
                    transform.position = Vector3.MoveTowards(
                    transform.position,
                    enemyRoute.routeList[curRouteIndex].destination,
                    enemyRoute.routeList[curRouteIndex].linearVelocity * Time.fixedDeltaTime);
                }
                else if(enemyWeapon != null && enemyRoute.routeList[curRouteIndex].isFireOnWait)
                {
                    if (enemyRoute.routeList[curRouteIndex].isWeaponRefresh)
                    {
                        enemyWeapon.initializeWeapons();
                    }
                    enemyWeapon.setWeaponDisabled(!enemyRoute.routeList[curRouteIndex].isFireOnWait);
                    state = RouteState.onFire;
                }
                else if(enemyRoute.routeList[curRouteIndex].waitTime >= 0.02f)
                {
                    setToWait();
                }
                else
                {
                    setToNext();
                }
                break;
            case RouteState.onFire:
                if (enemyWeapon.getCurWeaponData().isOnEnd())
                {
                    enemyWeapon.setWeaponDisabled(true);
                    setToWait();
                }
                break;
            case RouteState.onWait:
                if (!enemyRoute.routeList[curRouteIndex].isWaitTimeOut())
                {
                    enemyRoute.routeList[curRouteIndex].countWaitTime(Time.fixedDeltaTime);
                }
                else
                {
                    setToNext();
                }
                break;
            case RouteState.onStart:
                curRouteIndex = 0;
                setToMove();
                break;
            case RouteState.onEnd:
                if (isDestroyOnEnd)
                {
                    Destroy(gameObject);
                }
                else if (isFireOnEnd)
                {
                    if (enemyWeapon != null)
                    {
                        if (isWeaponRefreshOnEnd)
                        {
                            enemyWeapon.initializeWeapons();
                        }
                        enemyWeapon.setWeaponDisabled(false);
                    }
                }
                break;
        }
    }

    public void setRoute(TextAsset enemyRouteJson)
    {
        enemyRoute = JsonUtility.FromJson<EnemyRouteList>(enemyRouteJson.text);
        transform.position = enemyRoute.initPos;
    }

    private void setToMove()
    {
        if (enemyWeapon != null)
        {
            if (enemyRoute.routeList[curRouteIndex].isWeaponRefresh)
            {
                enemyWeapon.initializeWeapons();
            }
            enemyWeapon.setWeaponDisabled(!enemyRoute.routeList[curRouteIndex].isFireOnMove);
        }
        headToDestination();
        state = RouteState.onMove;
    }

    private void headToDestination()
    {
        if (isHeadToDestination)
        {
            Vector3 distinateVector = enemyRoute.routeList[curRouteIndex].destination - transform.position;
            Quaternion q = Quaternion.AngleAxis(Mathf.Atan2(distinateVector.y, distinateVector.x) * Mathf.Rad2Deg - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);
        }
    }

    private void setToWait()
    {
        enemyRoute.routeList[curRouteIndex].resetWaitTime();
        state = RouteState.onWait;
    }

    private void setToNext()
    {
        curRouteIndex += 1;
        if (curRouteIndex < enemyRoute.routeList.Length)
        {
            setToMove();
        }
        else if (isLoopingRoute)
        {
            curRouteIndex = loopStartIndex;
            setToMove();
        }
        else
        {
            state = RouteState.onEnd;
        }
    }
}
