using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common_BulletSetObject : MonoBehaviour
{
    // A detector class for a bulletSet parent object to destroyed it self if all child object are destroyed.

    private void Update()
    {
        if(transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
