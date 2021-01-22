using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarFighter;
using static Assets.Scripts.ClassLib._StaticData;

public class UI_GameManager : MonoBehaviour
{
    enum PlayerState
    {
        dead,
        revive,
        alive
    }

    public GameObject solorFighter;
    public GameObject vortexFighter;
    public GameObject nebulaFighter;
    public GameObject stardustFighter;

    GameObject playerFighter;
    PlayerState playerState = PlayerState.dead;
    const float reviveWaitTime = 0.1f;
    float curReviveWaitTime = 0f;

    private void Awake()
    {

    }

    private void Start()
    {
        generatePlayerFighter();
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
                curReviveWaitTime = reviveWaitTime;
                playerState = PlayerState.revive;
                break;
            case PlayerState.revive:
                if(curReviveWaitTime <= 0)
                {
                    generatePlayerFighter();
                }
                else
                {
                    curReviveWaitTime -= Time.fixedDeltaTime;
                }
                break;
        }
    }

    private void generatePlayerFighter()
    {
        switch (selectedFighter)
        {
            case solor:
                playerFighter = Instantiate(solorFighter);
                break;
            case vortex:
                playerFighter = Instantiate(vortexFighter);
                break;
            case nebula:
                playerFighter = Instantiate(nebulaFighter);
                break;
            case stardust:
                playerFighter = Instantiate(stardustFighter);
                break;
        }
        playerState = PlayerState.alive;
    }
}
