using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class PlayerController : MonoBehaviour {
    [SerializeField] private ControllerManager controller;
    // Movements
    public Vector3 targetPosition;
    private int currentLane;
    private const float laneWidth = 1.8f;
    public float laneChangeSpeed;
    public float speed;
    private float timePassed = 0f;
    public bool movePermitted;
    public bool lrMovePermitted;
    public float timeOffBoard = 0f;

    // Arduino serial port
    public SerialPort joystick;
    private SerialPort balanceBall;

    // Player item info
    public bool isReversed;
    
    void OnEnable() {
        // Arduino port initialization
        joystick = controller.joystick;
        balanceBall = controller.balanceBall;

        // Initial location
        transform.position = new Vector3(0, -0.570f, 0);
        targetPosition = new Vector3(0, -0.570f, 0);
        currentLane = 0;
    }
    
    void Update() {
        if (IsOnBoard()) {
            /*
             * ARDUINO MODE
             */
            if (joystick.IsOpen) {
                joystick.Write("1");
                string readVal = joystick.ReadLine();
                int inputVal;
                if (readVal.Length > 0 && timePassed > 0.2f) {
                    inputVal = int.Parse(readVal);

                    if ((inputVal > 712 && !isReversed) || (inputVal < 312 && isReversed)) ChangeLane(1);
                    else if ((inputVal < 312 && !isReversed) || (inputVal > 712 && isReversed)) ChangeLane(-1);
                    
                    timePassed = 0;
                }
            }

            /*
             * KEYBOARD DEBUG
             */
            else {
                if (timePassed > 0.2f) {
                    if ((Input.GetAxisRaw("Horizontal") < 0 && !isReversed) || (Input.GetAxisRaw("Horizontal") > 0 && isReversed)) ChangeLane(-1);
                    else if ((Input.GetAxisRaw("Horizontal") > 0 && !isReversed) || (Input.GetAxisRaw("Horizontal") < 0 && isReversed)) ChangeLane(1);

                    timePassed = 0;
                }
            }
            
            // Forward move permitted from GameManager states
            if (movePermitted) {
                targetPosition = new Vector3(targetPosition.x, transform.position.y, transform.position.z + speed);
            }
            
            // Move Character
            if (lrMovePermitted) {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
            }

        } else {
            // TBA: 보드에서 떨어질 시 실행할 내용
            Debug.Log("PLAYER_CONTROLLER: Off board");
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        timePassed += Time.deltaTime;
    }

    void OnTriggerEnter(Collider item) {
        string tag = item.tag;
        switch(tag) {
            case "Super Fish": DataManager.instance.itemInfoDict["Fish"] += 2; break;
            case "Fish": DataManager.instance.itemInfoDict["Fish"] += 1; break;
            case "Bomb": DataManager.instance.itemInfoDict["Bomb"] += 1; break;
            default: break;
        }

        item.gameObject.SetActive(false);
    }

    void ChangeLane(int direction) {
        int targetLane = currentLane + direction;

        if (targetLane < 0 || targetLane > 2) return;

        currentLane = targetLane;
        targetPosition = new Vector3((currentLane - 1) * laneWidth, transform.position.y, 0);
    }

    bool IsOnBoard() {
        if (balanceBall.IsOpen) {
            balanceBall.Write("c");
            string readVal = balanceBall.ReadLine();
            if (readVal.Length > 0) {
                int velostatVal = int.Parse(readVal);
                if (velostatVal < 700) timeOffBoard += Time.deltaTime;
                else timeOffBoard = 0f;
            }
        } else {
            timeOffBoard = 0f;
        }
        
        if (timeOffBoard > 0.3f) return false;
        return true;
    }
}
