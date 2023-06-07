using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialState : State {
    [SerializeField] private PlayerController player;
    [SerializeField] private GameManager gameManager; 
    [SerializeField] private string nextSceneName;
    public bool startTask;

    // Balancing
    public bool isBalanced = false;
    private float balancedTime = 0f;

    // Controlling
    public bool isControlled = false;
    public bool left = false;
    public bool right = false;

    // Consuming Item
    public bool isItemConsumed = false;

    public override void Enter() {
        startTimer = 3f;
        player.movePermitted = false;
        player.lrMovePermitted = false;
        isFinished = false;

        gameManager.fishRatio = 1;
        gameManager.superFishRatio = 0;

        startTask = false;
        Debug.Log("TUTORIAL: Starting Tutorial");
    }

    public override void Tick() {
        // 보수볼 균형잡기 5초
        if (!isBalanced) {
            if (startTask) {
                if (player.timeOffBoard <= 0.5f) balancedTime += Time.deltaTime;
                else balancedTime = 0;

                if (balancedTime > 5f) {
                    isBalanced = true;
                    startTask = false;
                }
                Debug.Log(balancedTime);
            }
        }

        // 좌우 컨트롤
        else if (!isControlled) {
            if (startTask) {
                player.lrMovePermitted = true;
                if (player.gameObject.transform.position.x > 1) right = true;
                else if (player.gameObject.transform.position.x < -1) left = true;
            }

            if (right && left) {
                isControlled = true;
                startTask = false;
            }
        }

        // 아이템 먹기
        else if (!isItemConsumed) {
            if (startTask) {
                if (startTimer > 0) startTimer -= Time.deltaTime;
                else {
                    player.movePermitted = true;
                    if (DataManager.instance.itemInfoDict["Fish"] > 0) isItemConsumed = true;
                    Debug.Log(DataManager.instance.itemInfoDict["Fish"]);
                }
            }
        }

        // 튜토리얼 완료
        else {
            player.movePermitted = false;
            player.lrMovePermitted = false;
        }
    }

    public override void Exit() {
        Debug.Log("TUTORIAL: Moving to next state");
        player.gameObject.SetActive(false);
        gameManager.mapGeneratorObject.SetActive(false);
        SceneManager.LoadScene(nextSceneName);
    }
}
