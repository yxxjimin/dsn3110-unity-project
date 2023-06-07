using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCam : MonoBehaviour {
    [SerializeField] private CutSceneDialogue dialogue;
    private float walkTime = 3f;
    public PlayerController player;
    private Vector3 playerPosition;
    private float initX;
    private float offsetZ = -17f;

    void Start() {
        playerPosition = player.transform.position;
        initX = playerPosition.x;
        // transform.position = new Vector3(initX, playerPosition.y + offsetY, playerPosition.z + offsetZ);
        transform.position = new Vector3(initX, 4f, playerPosition.z + offsetZ);
        // Vector3 rotation = new Vector3(19.1f, 1.3f, 0f);
        // transform.Rotate(rotation);
    }

    void Update() {
        if (dialogue.dialogueFinished) {
            if (walkTime > 0) walkTime -= Time.deltaTime;
            else StartCoroutine(RotateCamera());
        } else {
            playerPosition = player.transform.position;
            transform.position = new Vector3(initX, 4f, playerPosition.z + offsetZ);
        }
    }

    IEnumerator RotateCamera() {
        Quaternion rot = transform.rotation;
        for (float angle = 0f; angle >= -20f; angle -= 0.0003f) {
            rot.x = angle;
            transform.rotation = rot;
            yield return null;
        }
    }
}
