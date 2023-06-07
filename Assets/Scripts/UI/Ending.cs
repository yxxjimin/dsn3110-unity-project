using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour {

    Image currentImage;
    public Sprite[] imageArray;
    [SerializeField] private State stage;

    void Start() {
        currentImage = GetComponent<Image>();
    }

    void Update() {
        if (stage.startTimer > 2) ChangeSprite(0);
        else if (stage.startTimer > 1) ChangeSprite(1);
        else if (stage.startTimer > 0) ChangeSprite(2);
        else this.gameObject.SetActive(false);
    }

    void ChangeSprite(int num) {
        currentImage.sprite = imageArray[num];
    }
}
