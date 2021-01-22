using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;

public class OutPutJson : MonoBehaviour
{
    void Start()
    {
        Skill sk = new Skill();
        sk.skillCD = 8.0f;
        sk.skillDuration = 4.0f;


        Debug.Log(JsonUtility.ToJson(sk));
    }

}
