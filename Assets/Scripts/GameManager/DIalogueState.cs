using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueState : State {
    private PlayerController playerScript;
    public GameManager gameManager;

    public override void Enter() {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerScript.movePermitted = false;
        isFinished = false;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.itemInfo.Clear();

        Debug.Log("DEBUG: Starting Dialogue");
    }

    public override void Tick() {
        // Enbale dialogue
    }

    public override void Exit() {
        Debug.Log("DEBUG: Moving to second stage");
        playerScript.gameObject.SetActive(false);
        gameManager.mapGeneratorObject.SetActive(false);
        isFinished = true;
    }
}
