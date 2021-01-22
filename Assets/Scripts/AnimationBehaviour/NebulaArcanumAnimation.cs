using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulaArcanumAnimation : MonoBehaviour
{
    // Appearing Part
    public float appearingStartAlpha;
    public float appearingEndAlpha;
    public float increasingAlphaRatio;

    // Disappearing Part
    public float decreasingAlphaRatio;

    SpriteRenderer sprite;
    Player_Skill_NebulaArcanumBullet nebulaArcanumBullet;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        nebulaArcanumBullet = GetComponent<Player_Skill_NebulaArcanumBullet>();

        Color temp = sprite.color;
        temp.a = appearingStartAlpha;
        sprite.color = temp;
    }

    void Update()
    {
        if (nebulaArcanumBullet.isOnStart())
        {
            Color temp = sprite.color;
            temp.a = Mathf.Min(temp.a + increasingAlphaRatio, appearingEndAlpha);
            sprite.color = temp;
            if(sprite.color.a == appearingEndAlpha)
            {
                nebulaArcanumBullet.putToActive();
            }
        }
        else if (nebulaArcanumBullet.isOnEnd())
        {
            Color temp = sprite.color;
            temp.a = Mathf.Max(temp.a - decreasingAlphaRatio, 0);
            sprite.color = temp;
            if (sprite.color.a == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
