using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {
    public PlayerController player;
    private Vector3 playerPosition;
    private float initX;
    // private float offsetY = 2f;
    private float offsetZ = -3f;

    void Start() {
        playerPosition = player.transform.position;
        initX = playerPosition.x;
        // transform.position = new Vector3(initX, playerPosition.y + offsetY, playerPosition.z + offsetZ);
        transform.position = new Vector3(initX, -0.09f, playerPosition.z + offsetZ);
        // Vector3 rotation = new Vector3(19.1f, 1.3f, 0f);
        // transform.Rotate(rotation);
    }

    void Update() {
        playerPosition = player.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, playerPosition.z + offsetZ);
    }
}
