using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class PlayerController : MonoBehaviour {
    // Movements
    private Vector3 targetPosition;
    private int currentLane;
    private const float laneWidth = 1.8f;
    public float laneChangeSpeed;
    public float speed;
    private float timePassed = 0f;
    public bool movePermitted;
    public float timeOffBoard = 0f;

    // Arduino serial port
    private static string joystickPortName = "/dev/cu.usbmodem2101";
    private SerialPort joystick = new SerialPort(joystickPortName, 9600);
    private static string balanceBallPortName = "/dev/cu.usbmodem1101";
    private SerialPort balanceBall = new SerialPort(balanceBallPortName, 9600);

    // Player item info
    private GameManager gameManager;
    
    void OnEnable() {
        // Arduino port initialization
        try {
            joystick.Open();
            joystick.ReadTimeout = 16;
            Debug.Log("Joystick Connection Open");

            // balanceBall.Open();
            // Debug.Log("Balance Ball Connection Open");
        } 
        catch {
            Debug.Log("Connection Failed");
        }

        // Initial location
        transform.position = new Vector3(0, -0.570f, 0);
        targetPosition = new Vector3(0, -0.570f, 0);
        currentLane = 0;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    void Update() {
        if (IsOnBoard()) {
            /*
             * ARDUINO MODE
             */
            if (joystick.IsOpen) {
                joystick.Write("c");
                string readVal = joystick.ReadLine();
                int inputVal;
                if (readVal.Length > 0 && timePassed > 0.2f) {
                    inputVal = int.Parse(readVal);

                    if (inputVal > 712) ChangeLane(1);
                    else if (inputVal < 312) ChangeLane(-1);

                    timePassed = 0;
                }
            }

            /*
             * KEYBOARD MODE
             */
            else {
                if (timePassed > 0.2f) {
                    if (Input.GetAxisRaw("Horizontal") < 0) ChangeLane(-1);
                    else if (Input.GetAxisRaw("Horizontal") > 0) ChangeLane(1);

                    timePassed = 0;
                }
            }
            
            // Forward move permitted from GameManager states
            if (movePermitted) {
                targetPosition = new Vector3(targetPosition.x, transform.position.y, transform.position.z + speed);
            }
            
            // Move Character
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            timePassed += Time.deltaTime;
        } else {
            // TBA: 보드에서 떨어질 시 실행할 내용
        }
    }

    void OnDisable() {
        joystick.Close();
        // balanceBall.Close();
    }

    void OnTriggerEnter(Collider item) {
        if (gameManager.itemInfo.ContainsKey(item.tag)) {
            gameManager.itemInfo[item.tag] += 1;
        } else {
            gameManager.itemInfo.Add(item.tag, 1);
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
        // balanceBall.Write("c");
        // int velostatVal = balanceBall.ReadByte();
        
        // if (velostatVal == 0) timeOffBoard += Time.deltaTime;
        // else timeOffBoard = 0f;

        // if (timeOffBoard > 0.5f) return false;
        return true;
    }
}
