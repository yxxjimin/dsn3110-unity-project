using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SecondStageState : State {
    private PlayerController playerScript;
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private string nextSceneName;
    [SerializeField] private CutSceneDialogue dialogue;
    [SerializeField] Image blackImage;
    private bool fadeOut;
    private float endTimer = 45f;

    public override void Enter() {
        startTimer = 3f;

        playerScript = player.GetComponent<PlayerController>();
        playerScript.movePermitted = false;
        playerScript.lrMovePermitted = true;
        playerScript.isReversed = true;
        isFinished = false;

        gameManager.fishRatio = 0.5f;
        gameManager.superFishRatio =  0.2f;

        dialogue.gameObject.SetActive(false);

        Debug.Log("SECOND_STAGE: Starting Stage 2");

        StartCoroutine(FadeIn());
        fadeOut = true;
    }

    public override void Tick() {
        if (player.transform.position.z <= 100f && !dialogue.dialogueFinished) {
            // Starting timer
            if (startTimer > 0) startTimer -= Time.deltaTime;
            else playerScript.movePermitted = true;

            // Game length timer
            if (playerScript.movePermitted && endTimer > 0) endTimer -= Time.deltaTime;
            else if (endTimer <= 0 && fadeOut) {
                // isFinished = true;
                StartCoroutine(FadeOut());
            } else if (!fadeOut) {
                isFinished = true;
            }
        }

        // Show dialogue
        else if (player.transform.position.z > 100f && !dialogue.dialogueFinished) {
            dialogue.gameObject.SetActive(true);

            playerScript.movePermitted = false;
        }

        else if (dialogue.dialogueFinished) {
            playerScript.movePermitted = true;
            endTimer -= Time.deltaTime;
            if (endTimer <= 0) isFinished = true;
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
        Debug.Log("SECOND_STAGE: Moving to next state");
        player.SetActive(false);
        gameManager.mapGeneratorObject.SetActive(false);

        if (DataManager.instance.itemInfoDict["Fish"] > DataManager.instance.itemInfoDict["Bomb"]) {
            nextSceneName = "ZzapEnding";
        } else {
            nextSceneName = "JinEnding";
        }
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator FadeIn() {
        Color c = blackImage.color;
        for (float alpha = 1f; alpha >= 0f; alpha -= 0.03f) {
            c.a = alpha < 0 ? 0 : alpha;
            blackImage.color = c;
            yield return null;
        }
    }

    IEnumerator FadeOut() {
        Color c = blackImage.color;
        for (float alpha = 0f; alpha <= 1f; alpha += 0.03f) {
            c.a = alpha > 1 ? 1 : alpha;
            blackImage.color = c;
            yield return null;
        }
        fadeOut = false;
    }
}
