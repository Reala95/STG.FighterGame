using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;
using Assets.Scripts.ClassLib;

public class OutPutJson : MonoBehaviour
{
    void Start()
    {
        EnemyRouteList.Route[] rl = new EnemyRouteList.Route[3];
        rl[0] = new EnemyRouteList.Route(new Vector3(0, 1, 0), 3, 0.5f, false, false);
        rl[1] = new EnemyRouteList.Route(new Vector3(0, 2, 0), 3, 0.5f, false, false);
        rl[2] = new EnemyRouteList.Route(new Vector3(0, 3, 0), 3, 0.5f, false, false);
        EnemyRouteList erl = new EnemyRouteList(rl);
        Debug.Log(JsonUtility.ToJson(erl));
    }

}
