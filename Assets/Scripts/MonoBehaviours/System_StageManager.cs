using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_StageManager : MonoBehaviour
{
    public int[] spawnerOrder;

    System_Spawner[] spawnerList;
    int curIndex = 0;

    void Start()
    {
        spawnerList = new System_Spawner[transform.childCount];
        for(int i = 0; i < spawnerList.Length; i++)
        {
            spawnerList[i] = transform.GetChild(i).GetComponent<System_Spawner>();
        }
    }

    private void FixedUpdate()
    {
        if (spawnerList[spawnerOrder[curIndex]].isOnWait())
        {
            spawnerList[spawnerOrder[curIndex]].refresh();
            spawnerList[spawnerOrder[curIndex]].activate();
        }
        else if (spawnerList[spawnerOrder[curIndex]].isOnEnd())
        {
            curIndex += 1;
            if(curIndex >= spawnerOrder.Length)
            {
                Destroy(gameObject);
            }
        }
    }
}
