using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject player;
    public GameObject mapGen;
    private Queue<State> stateList;
    [SerializeField]
    private State tutorialStage, firstStage, secondStage;
    private State currentState;

    public Dictionary<string, int> itemInfo;

    void Start() {
        itemInfo = new Dictionary<string, int>();

        stateList = new Queue<State>();
        stateList.Enqueue(tutorialStage);
        stateList.Enqueue(firstStage);
        stateList.Enqueue(secondStage);

        currentState = stateList.Peek();
        currentState.Enter();
    }

    void Update() {
        currentState.Tick();
        if (currentState.isFinished) {
            try {
                stateList.Dequeue();
                currentState = stateList.Peek();
                player.SetActive(true);
                mapGen.SetActive(true);
                currentState.Enter();
            } catch {
                Debug.Log("끝~~~~~~~~~~~");
                GameObject cam = GameObject.Find("Main Camera");
                cam.transform.position = new Vector3(0, 0, 0);
            }
            
        }
        // Debug.LogFormat("Fish, {0}", itemInfo["Fish"]);
    }
}

[HideInInspector]
public abstract class State : MonoBehaviour {
    public abstract void Enter();
    public abstract void Tick();
    public abstract void Exit();

    public bool isFinished;
}