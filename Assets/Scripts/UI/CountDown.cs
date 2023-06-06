using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

    Image currentImage;
    public Sprite[] imageArray;
    private FirstStageState firstStage;
    // private float startTimer = 3f;

    void Start() {
        currentImage = GetComponent<Image>();
        firstStage = GameObject.Find("FirstStage").GetComponent<FirstStageState>();
    }

    void Update() {
        if (firstStage.dialogueFinished) {
            if (firstStage.startTimer > 2) ChangeSprite(0);
            else if (firstStage.startTimer > 1) ChangeSprite(1);
            else if (firstStage.startTimer > 0) ChangeSprite(2);
            else this.gameObject.SetActive(false);

            // startTimer -= Time.deltaTime;
        }
    }

    void ChangeSprite(int num) {
        currentImage.sprite = imageArray[num];
    }
}
