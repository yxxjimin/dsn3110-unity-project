using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Dialogue {
    [TextArea]
    public string dialogue;
    public Sprite cg;

}
public class FirstStageDialogue : MonoBehaviour {
    [SerializeField] private Image sprite_StandingCG;
    [SerializeField] private Image sprite_DialogueBox;
    [SerializeField] private TextMeshProUGUI txt_Dialogue;
    [SerializeField] private TextMeshProUGUI txt_Name;

    private bool isDialogue = false; //대화가 진행중인지 알려줄 변수
    private int count = 0; //대사가 얼마나 진행됐는지 알려줄 변수

    private FirstStageState firstStage;
    private PlayerController player;
    private float timePassed = 0f;

    [SerializeField] private Dialogue[] dialogue;

   
    public void ShowDialogue()
    {
        ONOFF(true); //대화가 시작됨
        count = 0;
        NextDialogue(); //호출되자마자 대사가 진행될 수 있도록 
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
        //첫번째 대사와 첫번째 cg부터 계속 다음 cg로 진행되면서 화면에 보이게 된다. 
        txt_Dialogue.text = dialogue[count].dialogue;
        sprite_StandingCG.sprite = dialogue[count].cg;
        count++; //다음 대사와 cg가 나오도록 
    
    }
 
    void OnEnable() {
        isDialogue = true;
        ShowDialogue();
        firstStage = GameObject.Find("FirstStage").GetComponent<FirstStageState>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update() {
        if (isDialogue) {
            // Arduino mode
            if (player.joystick.IsOpen && timePassed > 0.2f) {
                player.joystick.Write("0");
                string readVal = player.joystick.ReadLine();
                if (readVal.Length > 0 && int.Parse(readVal) == 1) {
                    if (count < dialogue.Length) NextDialogue(); //다음 대사가 진행됨
                    else {
                        ONOFF(false); //대사가 끝남
                        firstStage.dialogueFinished = true;
                    }
                    timePassed = 0;
                }
            } else {
                if (Input.GetKeyDown(KeyCode.Space)) {
                //대화의 끝을 알아야함.
                    if (count < dialogue.Length) NextDialogue(); //다음 대사가 진행됨
                    else {
                        ONOFF(false); //대사가 끝남
                        firstStage.dialogueFinished = true;
                    }
                }
            }
            timePassed += Time.deltaTime;         
        }
    }
}
