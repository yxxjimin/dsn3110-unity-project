using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstStageState : State {
    private PlayerController playerScript;
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private string nextSceneName;
    [SerializeField] Image blackImage;
    private bool fadeOut;
    [SerializeField] private CutSceneDialogue dialogue;
    private float endTimer = 45f;
    private bool audioPlayed = false;
    [SerializeField] public AudioClip bgMusic;

    public override void Enter() {
        startTimer = 3f;

        playerScript = player.GetComponent<PlayerController>();
        playerScript.movePermitted = false;
        playerScript.lrMovePermitted = true;
        isFinished = false;

        DataManager.instance.itemInfoDict["Fish"] = 0;
        DataManager.instance.itemInfoDict["Bomb"] = 0;
        DataManager.instance.GetComponent<AudioSource>().clip = bgMusic;
        DataManager.instance.GetComponent<AudioSource>().Play();

        gameManager.fishRatio = 0.5f;
        gameManager.superFishRatio =  0.2f;

        Debug.Log("FIRST_STAGE: Starting Stage 1");

        StartCoroutine(FadeIn());
        fadeOut = true;
    }

    public override void Tick() {
        if (dialogue.dialogueFinished) {
            if (!audioPlayed) {
                GetComponent<AudioSource>().Play();
                audioPlayed = true;
            }
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
