using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAppearing : MonoBehaviour
{
    public float startAlpha;
    public float endAlpha;
    public float increasingRatio;

    SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        Color temp = sprite.color;
        temp.a = startAlpha;
        sprite.color = temp;
    }

    void Update()
    {
        if (sprite.color.a < endAlpha)
        {
            Color temp = sprite.color;
            temp.a = Mathf.Min(temp.a + increasingRatio, endAlpha);
            sprite.color = temp;
            if(sprite.color.a == endAlpha)
            {
                this.enabled = false;
            }
        }
    }
}
