using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public float degreePerSecond;
    public GameObject player;

    void Start() {
        
    }

    void Update() {
        transform.Rotate(Vector3.up * Time.deltaTime * degreePerSecond);
    }
}
