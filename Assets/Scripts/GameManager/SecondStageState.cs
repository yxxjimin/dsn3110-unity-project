using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondStageState : State {
    private PlayerController playerScript;
    public GameManager gameManager;

    // Temporary start trigger
    private float startTimer = 3f;
    private float endTimer = 48f;

    public override void Enter() {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerScript.movePermitted = false;
        playerScript.isReversed = true;
        isFinished = false;
        gameManager.zLimit = 780f;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // gameManager.itemInfo.Clear();

        Debug.Log("SECOND_STAGE: Starting Stage 2");
    }

    public override void Tick() {
        if (startTimer > 0) {
            startTimer -= Time.deltaTime;
        } else {
            playerScript.movePermitted = true;
        }

        if (endTimer > 0) {
            endTimer -= Time.deltaTime;
        } else {
            Exit();
        }
    }

    public override void Exit() {
        Debug.Log("SECOND_STAGE: Moving to next state");
        playerScript.gameObject.SetActive(false);
        gameManager.mapGeneratorObject.SetActive(false);
        isFinished = true;
    }
}
