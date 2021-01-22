using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;
using static Assets.Scripts.ClassLib._StaticData;

public class Player_PlayerFighterObject : Common_FighterObject
{
    enum FighterStart
    {
        onBegin,
        onAction
    }

    FighterStart startState = FighterStart.onBegin;
    float startMoveVeclocity = 3.0f;

    GameObject safeShield;
    bool isSafeShieldActive = true;
    bool isSolorShieldActive = false;

    Rigidbody2D playerFighter;
    int sensitive = 18;

    UI_HealBar healBar;

    private void Awake()
    {
        FighterObjectAwake();
    }

    private void Start()
    {
        safeShield = GameObject.FindGameObjectWithTag(playerSafeShieldTag);
        playerFighter = GetComponent<Rigidbody2D>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        healBar = GameObject.FindGameObjectWithTag(uiHealBarTag).GetComponent<UI_HealBar>();
        healBar.initialize(gameObject);
    }

    private void FixedUpdate()
    {
        switch (startState)
        {
            case FighterStart.onAction:
                FighterObjectFixedUpdate();
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                playerFighter.velocity = sensitive * (mousePos - (Vector2)transform.position);
                break;
            case FighterStart.onBegin:
                if (transform.position != Vector3.zero)
                {
                    transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, startMoveVeclocity * Time.fixedDeltaTime);
                }
                else
                {
                    safeShield.GetComponent<Player_SafeShield>().enableCountDown();
                    GameObject.FindGameObjectWithTag(playerWeaponTag).GetComponent<Common_WeaponObject>().setWeaponDisabled(false);
                    GameObject.FindGameObjectWithTag(playerSkillTag).GetComponent<Player_SkillObject>().getSkillData().disabled = false;
                    Cursor.lockState = CursorLockMode.Confined;
                    startState = FighterStart.onAction;
                }
                break;
        }
        
    }

    public void activateSafeShield()
    {
        fighterData.isInvincible = true;
        safeShield.SetActive(true);
        setShieldBools(true, 0);
    }

    public void setShieldBools(bool shieldActivive, int shieldType)
    {
        if(shieldType == 0)
        {
            isSafeShieldActive = shieldActivive;
        }
        else
        {
            isSolorShieldActive = shieldActivive;
        }
        fighterData.isInvincible = isSafeShieldActive || isSolorShieldActive;
    }
}
