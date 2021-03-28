using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarFighter;
using Assets.Scripts.ClassLib;
using static Assets.Scripts.ClassLib._StaticData;

public class System_GameManager : MonoBehaviour
{
    public bool isFieldTesting = false;
    public int selectedFighter = 0;

    enum PlayerState
    {
        dead,
        revive,
        alive
    }

    public GameObject[] fighterList;

    public Sprite[] fighterImgList;

    GameObject playerFighter;
    PlayerState playerState = PlayerState.dead;
    const float reviveWaitTime = 0.1f;
    float curReviveWaitTime = 0f;
    Image lifeCountImage;
    Text lifeCountText;
    int lifeRemain = 3;

    GameObject pauseMenu;
    GameObject lostMenu;
    GameObject winMenu;
    bool isPauseAble = true;



    private void Awake()
    {
        GameObject lifeCountObject = GameObject.FindGameObjectWithTag(uiLifeCount);
        lifeCountImage = lifeCountObject.transform.Find("FighterImage").gameObject.GetComponent<Image>();
        lifeCountImage.sprite = fighterImgList[selectedFighter];
        lifeCountText = lifeCountObject.transform.Find("LifeCountText").gameObject.GetComponent<Text>();
        lifeCountText.text = "x" + lifeRemain;

        pauseMenu = GameObject.FindGameObjectWithTag(uiPauseMenu);
        pauseMenu.SetActive(false);
        lostMenu = GameObject.FindGameObjectWithTag(uiLostMenu);
        lostMenu.SetActive(false);
        winMenu = GameObject.FindGameObjectWithTag(uiWinMenu);
        winMenu.SetActive(false);
    }

    private void Start()
    {
        generatePlayerFighter();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(MouseClick[RightClick]) && isPauseAble)
        {
            pauseMenu.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        switch (playerState)
        {
            case PlayerState.alive:
                if(playerFighter == null)
                {
                    playerState = PlayerState.dead;
                }
                break;
            case PlayerState.dead:
                if(lifeRemain > 0)
                {
                    lifeRemain -= 1;
                    lifeCountText.text = "x" + lifeRemain;
                    curReviveWaitTime = reviveWaitTime;
                    playerState = PlayerState.revive;
                }
                else
                {
                    lostMenu.SetActive(true);
                }
                break;
            case PlayerState.revive:
                if(curReviveWaitTime > 0)
                {
                    curReviveWaitTime -= Time.fixedDeltaTime;
                }
                else
                {
                    generatePlayerFighter();
                }
                break;
        }
    }

    private void generatePlayerFighter()
    {
        if (!isFieldTesting)
        {
            playerFighter = Instantiate(fighterList[_StaticData.selectedFighter]);
        }
        else
        {
            playerFighter = Instantiate(fighterList[selectedFighter]);
        }
        playerState = PlayerState.alive;
    }

    public void setPauseAble(bool isPauseAble)
    {
        this.isPauseAble = isPauseAble;
    }

    public void showWinMenu()
    {
        winMenu.SetActive(true);
    }
}
