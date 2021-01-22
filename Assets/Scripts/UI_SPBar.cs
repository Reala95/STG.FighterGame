using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarFighter;

public class UI_SPBar : MonoBehaviour
{
    public bool isInOpt = false;

    GameObject skillReadyText;
    GameObject skillInfiniteText;
    GameObject skillChargeCount;
    Text skillChargeCountText;
    Image SPBarSR;
    Skill linkedSkill;

    Color inCDColor = new Color(0, 0.5f, 1);
    Color inActiveColor = new Color(1, 0.8f, 0);

    private void Awake()
    {
        SPBarSR = transform.GetChild(0).GetComponent<Image>();
        skillReadyText = GameObject.Find("SkillReadyText");
        skillInfiniteText = GameObject.Find("SkillInfiniteText");
        skillChargeCount = GameObject.Find("SkillChargeCountText");
        skillChargeCountText = skillChargeCount.GetComponent<Text>();
        skillReadyText.SetActive(false);
        skillInfiniteText.SetActive(false);
        skillChargeCount.SetActive(false);
    }

    private void Update()
    {
        if (isInOpt)
        {
            if (linkedSkill.isOnCD())
            {
                transform.localScale = new Vector3(linkedSkill.getCDPercentage(), 1, 1);
                SPBarSR.color = inCDColor;
            }
            else if (linkedSkill.isOnActive())
            {
                transform.localScale = new Vector3(linkedSkill.getDurationPercentage(), 1, 1);
                SPBarSR.color = inActiveColor;
            }
            else if (linkedSkill.isOnInfinite())
            {
                transform.localScale = new Vector3(1, 1, 1);
                SPBarSR.color = inActiveColor;
                skillInfiniteText.SetActive(true);
            }
            else if (linkedSkill.isOnReady())
            {
                transform.localScale = new Vector3(1, 1, 1);
                SPBarSR.color = inCDColor;
            }
            if (linkedSkill.isNeedReadyText)
            {
                skillReadyText.SetActive(linkedSkill.isOnReady());
            }
        }
    }

    //public void initialize(Skill linkedData)
    public Text initialize(GameObject linkedObject)
    {
        linkedSkill = linkedObject.GetComponent<Player_SkillObject>().getSkillData();
        transform.localScale = new Vector3(0, 1, 1);
        SPBarSR.color = linkedSkill.isOnActive()? inActiveColor : inCDColor;
        skillReadyText.SetActive(linkedSkill.isOnReady());
        skillInfiniteText.SetActive(linkedSkill.isOnInfinite());
        skillChargeCount.SetActive(linkedSkill.charge > 1);
        isInOpt = true;
        return skillChargeCountText;
    }

    public void terminate()
    {
        linkedSkill = null;
        transform.localScale = new Vector3(0, 1, 1);
        SPBarSR.color = inCDColor;
        skillReadyText.SetActive(false);
        skillInfiniteText.SetActive(false);
        skillChargeCountText.text = "x0";
        skillChargeCount.SetActive(false);
        isInOpt = false;
    }
}
