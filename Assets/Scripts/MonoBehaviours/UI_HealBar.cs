using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarFighter;

public class UI_HealBar : MonoBehaviour
{
    public bool isInOpt = false;

    Image healBarSR;
    Fighter linkedFighter;

    private void Awake()
    {
        healBarSR = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        if (isInOpt)
        {
            transform.localScale = new Vector3(linkedFighter.getHealPercentage(), 1, 1);
            changeBarColor();
        }
    }

    public void changeBarColor()
    {
        healBarSR.color = new Color(
            Mathf.Max(0, 1 - 2 * linkedFighter.getHealPercentage()),
            Mathf.Max(1, 1 + 2 * linkedFighter.getHealPercentage()),
            0
            );
    }

    //public void initialize(Fighter linkedData)
    public void initialize(GameObject linkedObject)
    {
        linkedFighter = linkedObject.GetComponent<Player_PlayerFighterObject>().getFighterData();
        transform.localScale = new Vector3(linkedFighter.getHealPercentage(), 1, 1);
        changeBarColor();
        isInOpt = true;
    }

    public void terminate()
    {
        linkedFighter = null;
        transform.localScale = new Vector3(0, 1, 1);
        healBarSR.color = new Color(1, 0, 0);
        isInOpt = false;
    }
}
