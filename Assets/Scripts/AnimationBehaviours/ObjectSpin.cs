using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpin : MonoBehaviour
{
    public Vector3 rotating;

    void Update()
    {
        transform.Rotate(rotating);
    }
}
