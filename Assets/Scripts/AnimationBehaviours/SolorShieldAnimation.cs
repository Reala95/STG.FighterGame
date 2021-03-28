using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolorShieldAnimation : MonoBehaviour
{
    // Appearing Part
    public float appearingStartAlpha;
    public float appearingEndAlpha;
    public float increasingAlphaRatio;

    public Vector3 appearingStartScale;
    public Vector3 appearingEndScale;
    public Vector3 appearingScaleChangeRatio;
    public bool isScaleDecreasing;

    // Disappearing Part
    public float decreasingAlphaRatio;

    public Vector3 disappearingEndScale;
    public Vector3 disappearingScaleChangeRatio;

    // Common Part
    SpriteRenderer sprite;
    Player_Skill_SolarShieldObject solorShieldParent;
    bool isAppearingFinish = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        solorShieldParent = transform.parent.GetComponent<Player_Skill_SolarShieldObject>();

        Color temp = sprite.color;
        temp.a = appearingStartAlpha;
        sprite.color = temp;

        transform.localScale = appearingStartScale;
    }

    void Update()
    {
        if (!isAppearingFinish)
        {
            Color temp = sprite.color;
            temp.a = Mathf.Min(temp.a + increasingAlphaRatio, appearingEndAlpha);
            sprite.color = temp;
            if (isScaleDecreasing)
            {
                transform.localScale = new Vector3(
                    Mathf.Max(transform.localScale.x - appearingScaleChangeRatio.x, appearingEndScale.x),
                    Mathf.Max(transform.localScale.y - appearingScaleChangeRatio.y, appearingEndScale.y),
                    Mathf.Max(transform.localScale.z - appearingScaleChangeRatio.z, appearingEndScale.z)
                    );
            }
            else
            {
                transform.localScale = new Vector3(
                    Mathf.Min(transform.localScale.x + appearingScaleChangeRatio.x, appearingEndScale.x),
                    Mathf.Min(transform.localScale.y + appearingScaleChangeRatio.y, appearingEndScale.y),
                    Mathf.Min(transform.localScale.z + appearingScaleChangeRatio.z, appearingEndScale.z)
                    );
            }
            if (sprite.color.a == appearingEndAlpha && transform.localScale.Equals(appearingEndScale))
            {
                isAppearingFinish = true;
            }
        }
        else if (solorShieldParent.isOnEnd())
        {
            Color temp = sprite.color;
            temp.a = Mathf.Max(temp.a - decreasingAlphaRatio, 0);
            sprite.color = temp;
            transform.localScale = new Vector3(
                Mathf.Min(transform.localScale.x + disappearingScaleChangeRatio.x, disappearingEndScale.x),
                Mathf.Min(transform.localScale.y + disappearingScaleChangeRatio.y, disappearingEndScale.y),
                Mathf.Min(transform.localScale.z + disappearingScaleChangeRatio.z, disappearingEndScale.z)
                );
            if (sprite.color.a == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
