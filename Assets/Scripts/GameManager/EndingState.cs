using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingState : State {
    private PlayerController playerScript;
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CutSceneDialogue dialogue;
    [SerializeField] private string nextSceneName;
    [SerializeField] public AudioClip bgMusic;

    public override void Enter() {
        DataManager.instance.GetComponent<AudioSource>().Stop();
        DataManager.instance.GetComponent<AudioSource>().clip = bgMusic;
        DataManager.instance.GetComponent<AudioSource>().Play();
        startTimer = 3f;

        playerScript = player.GetComponent<PlayerController>();
        playerScript.movePermitted = false;
        playerScript.lrMovePermitted = true;
        isFinished = false;

        gameManager.fishRatio = 0.5f;
        gameManager.superFishRatio =  0.2f;
    }

    public override void Tick() {
        if (startTimer > 0) startTimer -= Time.deltaTime;
        else dialogue.gameObject.SetActive(true);

        if (dialogue.dialogueFinished) isFinished = true;
    }

    public override void Exit() {
        SceneManager.LoadScene(nextSceneName);
    }
}
