using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;

public class System_Spawner : MonoBehaviour
{
    [System.Serializable]
    public class Spawn
    {
        public int enemy;
        public int route;
        public float spawnInterval;

        float curSpawnInterval = 0;

        public void initialize()
        {
            curSpawnInterval = spawnInterval;
        }

        public void countSpawnInterval(float deltaTime)
        {
            curSpawnInterval -= deltaTime;
        }

        public bool isSpawnIntervalOut()
        {
            return curSpawnInterval <= 0;
        }
    }

    enum SpawnerState
    {
        onWait,
        onActive,
        onEnd
    }

    public GameObject[] enemyPool;
    public TextAsset[] enemyRouteJsonPool;
    public Spawn[] enemySpawnList;
    public int keyEnemyIndex = -1;

    SpawnerState state = SpawnerState.onWait;
    GameObject keyEnemy;
    bool isKeyEnemySpawned = false;
    bool isFirstTime = true;
    int curIndex = 0;

    private void Awake()
    {
        foreach(Spawn sp in enemySpawnList)
        {
            sp.initialize();
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case SpawnerState.onActive:
                if (!enemySpawnList[curIndex].isSpawnIntervalOut())
                {
                    enemySpawnList[curIndex].countSpawnInterval(Time.fixedDeltaTime);
                }
                else
                {
                    if (curIndex != keyEnemyIndex)
                    {
                        Instantiate(enemyPool[enemySpawnList[curIndex].enemy])
                            .GetComponent<Enemy_EnemyStraightLineRouter>().setRoute(enemyRouteJsonPool[enemySpawnList[curIndex].route]); ;
                    }
                    else
                    {
                        keyEnemy = Instantiate(enemyPool[enemySpawnList[curIndex].enemy]);
                        keyEnemy.GetComponent<Enemy_EnemyStraightLineRouter>().setRoute(enemyRouteJsonPool[enemySpawnList[curIndex].route]);
                        isKeyEnemySpawned = true;
                    }
                    curIndex += 1;
                    if (curIndex >= enemySpawnList.Length)
                    {
                        state = SpawnerState.onEnd;
                    }
                }
                break;
            case SpawnerState.onEnd:
                break;
        }
        
    }

    private void LateUpdate()
    {
        if(keyEnemy == null & isKeyEnemySpawned)
        {
            state = SpawnerState.onEnd;
        }
    }

    public bool isOnWait()
    {
        return state == SpawnerState.onWait;
    }

    public bool isOnActive()
    {
        return state == SpawnerState.onActive;
    }

    public bool isOnEnd()
    {
        return state == SpawnerState.onEnd;
    }
    
    public void activate()
    {
        state = SpawnerState.onActive;
    }

    public void refresh()
    {
        if (!isFirstTime)
        {
            foreach (Spawn sp in enemySpawnList)
            {
                sp.initialize();
            }
            keyEnemy = null;
            isKeyEnemySpawned = false;
            curIndex = 0;
        }
        else
        {
            isFirstTime = false;
        }
    }
}
