using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private State currentState;
    public GameObject player;
    public GameObject mapGen;
    public State[] states;

    private int i = 0;
    public Dictionary<string, int> itemInfo;

    void Start() {
        currentState = states[i];
        itemInfo = new Dictionary<string, int>();
        currentState.Enter();
    }

    void Update() {
        currentState.Tick();
        if (currentState.isFinished) {
            try {
                currentState = states[++i];
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