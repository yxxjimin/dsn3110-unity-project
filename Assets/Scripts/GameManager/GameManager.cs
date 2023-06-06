using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Objects
    public GameObject playerObject;
    public GameObject mapGeneratorObject;

    // State settings
    [SerializeField]
    private State tutorialStage, firstStage, secondStage, infiniteRun;
    private State currentState;
    private Queue<State> stateList;
    private bool isCleared;
    
    // Item configs
    public Dictionary<string, int> itemInfo;

    // Map configs
    public float zLimit;
    public float fishRatio;
    public float superFishRatio;

    // Debug Flag
    [SerializeField] private bool DEBUG_MODE;

    void Start() {
        itemInfo = new Dictionary<string, int>();

        stateList = new Queue<State>();
        if (!DEBUG_MODE) {
            // stateList.Enqueue(tutorialStage);
            stateList.Enqueue(firstStage);
            // stateList.Enqueue(secondStage);
        } else {
            stateList.Enqueue(infiniteRun);
        }

        currentState = stateList.Peek();
        currentState.Enter();
    }

    void Update() {
        currentState.Tick();
        isCleared = (playerObject.transform.position.z > zLimit) ? true : false;
        if (currentState.isFinished) {
            Debug.LogFormat("GAME_MANAGER: Cleared - {0}", isCleared);

            // Move to next stage
            stateList.Dequeue();
            if (stateList.TryDequeue(out currentState)) {
                playerObject.SetActive(true);
                mapGeneratorObject.SetActive(true);
                currentState.Enter();
            } else {
                int fishCount = itemInfo.ContainsKey("Fish") ? itemInfo["Fish"] : 0
                                + (itemInfo.ContainsKey("Super Fish") ? itemInfo["Super Fish"] : 0);
                int bombCount = itemInfo.ContainsKey("Bomb") ? itemInfo["Bomb"] : 0;
                Debug.LogFormat("GAME_MANAGER: More fish - {0}", (fishCount > bombCount) ? true : false);
                GameObject cam = GameObject.Find("Main Camera");
                cam.transform.position = new Vector3(0, 0, 0);
            }
        }
    }
}

[HideInInspector]
public abstract class State : MonoBehaviour {
    public abstract void Enter();
    public abstract void Tick();
    public abstract void Exit();

    public bool isFinished;
}