using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStageState : State {
    private PlayerController playerScript;
    public GameManager gameManager;

    // Temporary start trigger
    private float startTimer = 3f;
    private float endTimer = 30f;

    public override void Enter() {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerScript.movePermitted = false;
        isFinished = false;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.itemInfo.Clear();

        Debug.Log("Starting Stage 1");
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
        Debug.Log("First stage Cleared!");
        playerScript.gameObject.SetActive(false);
        gameManager.mapGen.SetActive(false);
        isFinished = true;
    }
}
