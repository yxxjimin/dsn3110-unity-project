using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    // private Transform lookAt;
    // private Vector3 startoffset;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        // lookAt = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        // startoffset = transform.position - lookAt.position;

        playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        transform.position = new Vector3(
            0,
            playerTransform.position.y + 0.5f,
            playerTransform.position.z - 2
        );

    }



    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, playerTransform.position.z - 2);
        // pos.x = 0;
        //transform.position = lookAt.position+startoffset;
        transform.position = pos;
    }
}
