using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialState : State {
    private PlayerController playerScript;
    public GameManager gameManager; 

    // Balancing
    private bool isBalanced = true;
    private float balancedTime = 0f;

    // Controlling
    private bool isControlled = false;
    public bool left = false;
    public bool right = false;

    // Consuming Item
    private bool isItemConsumed = false;

    // Temporary start trigger
    private float startTimer = 3f;

    public override void Enter() {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerScript.movePermitted = false;
        isFinished = false;

        gameManager.itemInfo.Clear();

        Debug.Log("DEBUG: Starting Tutorial");
    }

    public override void Tick() {
        GameObject player = playerScript.gameObject;
        
        // 보수볼 균형잡기 5초
        if (!isBalanced) {
            if (playerScript.timeOffBoard <= 0.5f) balancedTime += Time.deltaTime;
            else balancedTime = 0;

            if (balancedTime > 5f) isBalanced = true;
        }

        // 좌우 컨트롤
        else if (!isControlled) {
            if (player.transform.position.x > 1) right = true;
            else if (player.transform.position.x < -1) left = true;

            if (right && left) isControlled = true;
        }

        // 아이템 먹기
        else if (!isItemConsumed) {
            if (startTimer > 0) startTimer -= Time.deltaTime;
            else {
                playerScript.movePermitted = true;
                if (gameManager.itemInfo.ContainsKey("Fish")) isItemConsumed = true;
            }
        }

        // 튜토리얼 완료
        else {
            Exit();
        }
    }

    public override void Exit() {
        Debug.Log("DEBUG: Tutorial Cleared");
        playerScript.gameObject.SetActive(false);
        gameManager.mapGeneratorObject.SetActive(false);
        isFinished = true;
    }
}
