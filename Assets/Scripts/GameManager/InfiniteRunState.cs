using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRunState : State {
    private GameObject player;
    private PlayerController playerScript;
    public GameManager gameManager;

    // Temporary start trigger
    private float startTimer = 3f;
    [SerializeField] private float endTimer;

    public override void Enter() {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        playerScript.movePermitted = false;
        playerScript.isReversed = false;
        isFinished = false;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.itemInfo.Clear();

        Debug.Log("DEBUG: Entering infinite run mode");
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
            Debug.LogFormat("DEBUG: Player Z = {0}", GameObject.Find("Player").transform.position.z);
            Exit();
        }

        if (player.transform.position.z > gameManager.zLimit) {
            playerScript.movePermitted = false;
            playerScript.targetPosition = new Vector3(playerScript.targetPosition.x, player.transform.position.y, player.transform.position.z);
            endTimer = 0f;
        }
    }

    public override void Exit() {
        Debug.Log("DEBUG: Should never be called");
        playerScript.gameObject.SetActive(false);
        gameManager.mapGeneratorObject.SetActive(false);
        isFinished = true;
    }
}
