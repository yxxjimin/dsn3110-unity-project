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
    
    // Item info
    public Dictionary<string, int> itemInfo;

    // Debug Flag
    [SerializeField]
    private bool DEBUG_MODE;

    void Start() {
        itemInfo = new Dictionary<string, int>();

        stateList = new Queue<State>();
        if (!DEBUG_MODE) {
            stateList.Enqueue(tutorialStage);
            stateList.Enqueue(firstStage);
            stateList.Enqueue(secondStage);
        } else {
            stateList.Enqueue(infiniteRun);
        }

        currentState = stateList.Peek();
        currentState.Enter();
    }

    void Update() {
        currentState.Tick();
        if (currentState.isFinished) {
            stateList.Dequeue();
            if (stateList.TryDequeue(out currentState)) {
                playerObject.SetActive(true);
                mapGeneratorObject.SetActive(true);
                currentState.Enter();
            } else {
                Debug.Log("DEBUG: All Cleared");
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