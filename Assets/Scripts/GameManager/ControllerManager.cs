using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ControllerManager : MonoBehaviour {
    private static string joystickPortName = "/dev/cu.usbmodem1101";
    public SerialPort joystick = new SerialPort(joystickPortName, 9600);
    private static string balanceBallPortName = "/dev/cu.usbmodem2101";
    public SerialPort balanceBall = new SerialPort(balanceBallPortName, 9600);
    
    void Awake() {
        try {
            joystick.Open();
            joystick.ReadTimeout = 16;
            Debug.Log("SYSTEM: Joystick connected");

            balanceBall.Open();
            balanceBall.ReadTimeout = 16;
            Debug.Log("SYSTEM: Balanceball connected");
        } catch {
            Debug.Log("SYSTEM: Arduino connection failed");
        }
    }

    void OnDisable() {
        joystick.Close();
        balanceBall.Close();
    }
}
