using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFighter
{
    [System.Serializable]
    public class EnemyRouteList
    {
        [System.Serializable]
        public class Route
        {
            public Vector3 destination;
            public float linearVelocity;
            public float waitTime;
            public bool isWeaponRefresh;
            public bool isFireOnMove;
            public bool isFireOnWait;

            float curWaitTime;

            public Route(Vector3 destination, float linearVelocity, float waitTime, bool isFireOnMove, bool isFireOnWait)
            {
                this.destination = destination;
                this.linearVelocity = linearVelocity;
                this.waitTime = waitTime;
                this.isFireOnMove = isFireOnMove;
                this.isFireOnWait = isFireOnWait;
            }

            public void resetWaitTime()
            {
                curWaitTime = waitTime;
            }

            public void countWaitTime(float deltaTime)
            {
                curWaitTime -= deltaTime;
            }

            public bool isWaitTimeOut()
            {
                return curWaitTime <= 0;
            }
        }

        public Vector3 initPos;
        public Route[] routeList;

        public EnemyRouteList(Route[] routeList)
        {
            this.routeList = routeList;
        }
    }
}
