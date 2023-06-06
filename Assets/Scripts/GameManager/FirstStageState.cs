using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStageState : State {
    private PlayerController playerScript;
    private GameObject player;
    public GameManager gameManager;

    // Temporary start trigger
    public bool dialogueFinished;
    private float startTimer = 3f;
    private float endTimer = 45f;

    public override void Enter() {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        playerScript.movePermitted = false;
        playerScript.lrMovePermitted = true;
        isFinished = false;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.itemInfo.Clear();

        gameManager.fishRatio = 0.6f;
        gameManager.superFishRatio =  0.3f;

        dialogueFinished = false;

        Debug.Log("FIRST_STAGE: Starting Stage 1");
    }

    public override void Tick() {
        if (dialogueFinished) {
            // Starting timer
            if (startTimer > 0) startTimer -= Time.deltaTime;
            else playerScript.movePermitted = true;

            // Game length timer
            if (playerScript.movePermitted && endTimer > 0) endTimer -= Time.deltaTime;
            else if (endTimer <= 0) Exit();
        }
        
        // Finish line
        if (player.transform.position.z > gameManager.zLimit) {
            playerScript.movePermitted = false;
            playerScript.targetPosition = new Vector3(playerScript.targetPosition.x, player.transform.position.y, player.transform.position.z);
            endTimer = 0f;
        }
    }

    public override void Exit() {
        Debug.Log("FIRST_STAGE: Moving to next state");
        playerScript.gameObject.SetActive(false);
        gameManager.mapGeneratorObject.SetActive(false);
        isFinished = true;
    }
}
