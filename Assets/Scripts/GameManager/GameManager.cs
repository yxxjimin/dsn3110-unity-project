using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Objects
    public GameObject playerObject;
    public GameObject mapGeneratorObject;

    // State settings
    [SerializeField] private State currentState;
    private bool isCleared;
    
    // Map configs
    public float zLimit;
    public float fishRatio;
    public float superFishRatio;

    // Debug Flag
    // [SerializeField] private bool DEBUG_MODE;

    void Start() {
        currentState.Enter();
    }

    void Update() {
        currentState.Tick();
        isCleared = (playerObject.transform.position.z > zLimit) ? true : false;
        if (currentState.isFinished) {
            Debug.LogFormat("GAME_MANAGER: Cleared - {0}", isCleared);
            currentState.Exit();
        }
    }
}

[HideInInspector]
public abstract class State : MonoBehaviour {
    public abstract void Enter();
    public abstract void Tick();
    public abstract void Exit();

    public bool isFinished;
    public float startTimer;
}