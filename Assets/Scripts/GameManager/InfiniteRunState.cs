// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class InfiniteRunState : State {
//     private PlayerController playerScript;
//     private GameObject player;
//     public GameManager gameManager;

//     // Temporary start trigger
//     private float startTimer = 3f;
//     private float endTimer = 10000f;

//     public override void Enter() {
//         player = GameObject.Find("Player");
//         playerScript = player.GetComponent<PlayerController>();
//         playerScript.movePermitted = false;
//         playerScript.lrMovePermitted = true;
//         isFinished = false;

//         gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
//         gameManager.itemInfo.Clear();

//         gameManager.fishRatio = 0.6f;
//         gameManager.superFishRatio =  0.3f;
//         gameManager.zLimit = 10000f;

//         Debug.Log("INFINITE: Starting infinite mode");
//     }

//     public override void Tick() {
//         // Starting timer
//         if (startTimer > 0) startTimer -= Time.deltaTime;
//         else playerScript.movePermitted = true;

//         // Game length timer
//         if (endTimer > 0) endTimer -= Time.deltaTime;
//         else Exit();

//         // Finish line
//         if (player.transform.position.z > gameManager.zLimit) {
//             playerScript.movePermitted = false;
//             playerScript.targetPosition = new Vector3(playerScript.targetPosition.x, player.transform.position.y, player.transform.position.z);
//             playerScript.movePermitted = false;
//             playerScript.lrMovePermitted = false;
//         }
//     }

//     public override void Exit() {
//         Debug.Log("INFINITE: Should not be called");
//         playerScript.gameObject.SetActive(false);
//         gameManager.mapGeneratorObject.SetActive(false);
//         isFinished = true;
//     }
// }
