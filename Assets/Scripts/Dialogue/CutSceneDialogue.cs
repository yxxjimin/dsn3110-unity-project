using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class Dialogue {
    [TextArea]
    public string dialogue;
    public string name;
    public Sprite cg;

}
public class CutSceneDialogue : MonoBehaviour {
    [SerializeField] private Image sprite_StandingCG;
    [SerializeField] private Image sprite_DialogueBox;
    [SerializeField] private TextMeshProUGUI txt_Dialogue;
    [SerializeField] private TextMeshProUGUI txt_Name;
    [SerializeField] private ControllerManager controller;

    private bool isDialogue = false;
    private int count = 0;
    private float timePassed = 0f;
    private SerialPort joystick;
    public bool dialogueFinished;

    [SerializeField] private Dialogue[] dialogue;

   
    public void ShowDialogue()
    {
        ONOFF(true);
        count = 0;
        NextDialogue();
    }

    private void ONOFF(bool _flag)
    {
        sprite_DialogueBox.gameObject.SetActive(_flag);
        sprite_StandingCG.gameObject.SetActive(_flag);
        txt_Dialogue.gameObject.SetActive(_flag);
        txt_Name.gameObject.SetActive(_flag);
        isDialogue = _flag;
    }

    private void NextDialogue() { 
        txt_Dialogue.text = dialogue[count].dialogue;
        txt_Name.text = dialogue[count].name;
        sprite_StandingCG.sprite = dialogue[count].cg;
        count++;
    
    }
 
    void OnEnable() {
        isDialogue = true;
        ShowDialogue();

        joystick = controller.joystick;
        dialogueFinished = false;
    }

    void Update() {
        if (isDialogue) {
            // Arduino mode
            if (joystick.IsOpen && timePassed > 0.2f) {
                joystick.Write("0");
                string readVal = joystick.ReadLine();
                if (readVal.Length > 0 && int.Parse(readVal) == 1) {
                    if (count < dialogue.Length) NextDialogue();
                    else {
                        ONOFF(false);
                        dialogueFinished = true;
                    }
                    timePassed = 0;
                }
            } else {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    if (count < dialogue.Length) NextDialogue();
                    else {
                        ONOFF(false);
                        dialogueFinished = true;
                    }
                }
            }
            timePassed += Time.deltaTime;         
        }
    }
}
