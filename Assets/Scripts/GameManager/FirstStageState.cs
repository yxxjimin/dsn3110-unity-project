using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstStageState : State {
    private PlayerController playerScript;
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private string nextSceneName;

    [SerializeField] private CutSceneDialogue dialogue;
    private float endTimer = 45f;

    public override void Enter() {
        startTimer = 3f;

        playerScript = player.GetComponent<PlayerController>();
        playerScript.movePermitted = false;
        playerScript.lrMovePermitted = true;
        isFinished = false;

        DataManager.instance.itemInfoDict["Fish"] = 0;
        DataManager.instance.itemInfoDict["Bomb"] = 0;

        gameManager.fishRatio = 0.5f;
        gameManager.superFishRatio =  0.2f;

        Debug.Log("FIRST_STAGE: Starting Stage 1");
    }

    public override void Tick() {
        if (dialogue.dialogueFinished) {
            // Starting timer
            if (startTimer > 0) startTimer -= Time.deltaTime;
            else playerScript.movePermitted = true;

            // Game length timer
            if (playerScript.movePermitted && endTimer > 0) endTimer -= Time.deltaTime;
            else if (endTimer <= 0) isFinished = true;
        }
        
        // Finish line
        if (player.transform.position.z > gameManager.zLimit) {
            playerScript.movePermitted = false;
            playerScript.lrMovePermitted = false;
            playerScript.targetPosition = new Vector3(playerScript.targetPosition.x, player.transform.position.y, player.transform.position.z);
            endTimer = 0f;
        }

    }

    public override void Exit() {
        Debug.Log("FIRST_STAGE: Moving to next state");
        player.SetActive(false);
        gameManager.mapGeneratorObject.SetActive(false);
        SceneManager.LoadScene(nextSceneName);
    }
}
