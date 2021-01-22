using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarFighter;
using static Assets.Scripts.ClassLib._StaticData;

public class Player_SkillObject : MonoBehaviour
{
    public TextAsset skillJson;

    protected Skill skillData = new Skill();

    protected UI_SPBar spBar;

    protected void SkillObjectAwake()
    {
        skillData = JsonUtility.FromJson<Skill>(skillJson.text);
        skillData.initialize();
    }

    protected Text SkillObjectStart()
    {
        spBar = GameObject.FindGameObjectWithTag(uiSPBarTag).GetComponent<UI_SPBar>();
        return spBar.initialize(gameObject);
    }

    public Skill getSkillData()
    {
        return skillData;
    }

    private void OnDestroy()
    {
        if(spBar != null)
        {
            spBar.terminate();
        }
    }
}
