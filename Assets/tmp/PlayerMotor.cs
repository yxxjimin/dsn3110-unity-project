using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    LEFT,
    CENTER,
    RIGHT
};

public class PlayerMotor : MonoBehaviour
{
    private float speed = 10.0f;
    private Vector3 moveVector;
    private float timePassed = 0.0f;

    // CharacterController
    private CharacterController controller;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;

    // Direction
    private int MAX_WIDTH = 2;
    private Direction dir;

    void Start()
    {
        gameObject.transform.position = new Vector3(-1.02f, -1.288f, 0);
        controller = GetComponent<CharacterController>();
        dir = Direction.CENTER;
    }

    void Update()
    {
        moveVector = Vector3.zero;

        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // Direction update within 3-Lane positioning
        if (timePassed > 0.1)
        {
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                // Right
                dir = (dir < Direction.CENTER) ? Direction.CENTER : Direction.RIGHT;
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                // Left
                dir = (dir > Direction.CENTER) ? Direction.CENTER : Direction.LEFT;
            }
            timePassed = 0;
        }

        // moveVector update upon this.dir
        Vector3 playerPos = this.transform.position;
        switch (dir)
        {
            case Direction.LEFT: moveVector.x = (playerPos.x > -MAX_WIDTH) ? -speed : 0; break;
            case Direction.CENTER:
                if (playerPos.x < 0) moveVector.x = speed;
                else if (playerPos.x > 0) moveVector.x = -speed;
                else moveVector.x = 0;
                break;
            case Direction.RIGHT: moveVector.x = (playerPos.x < MAX_WIDTH) ? speed : 0; break;
        }
        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);
        timePassed += Time.deltaTime;
    }
}
