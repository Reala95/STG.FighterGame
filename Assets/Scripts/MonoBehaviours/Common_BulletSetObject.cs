using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.ClassLib._StaticData;

public class Common_BulletSetObject : MonoBehaviour
{
    // A class for a bulletSet parent object to player launch SE or to be destroyed itself if all child object are destroyed.

    public int soundIndex = -1;

    private void Start()
    {
        if(soundIndex != -1)
        {
            GameObject.FindGameObjectWithTag(uiSEManager).GetComponents<AudioSource>()[soundIndex].Play();
        }
    }

    private void Update()
    {
        if(transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
