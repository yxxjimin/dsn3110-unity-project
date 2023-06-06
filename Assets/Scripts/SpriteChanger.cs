using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour {

    Image currentImage;
    public Sprite[] imageArray;
    public GameManager gameManager;
    [SerializeField] private int digit;
    [SerializeField] private bool isFish;
    int itemCount;

    void Start() {
        currentImage = GetComponent<Image>();
        itemCount = 0;
    }

    void Update() {
        string tag = (isFish) ? "Fish" : "Bomb";
        itemCount = gameManager.itemInfo.ContainsKey(tag) ? gameManager.itemInfo[tag] : 0;

        ChangeSprite(itemCount);
    }

    void ChangeSprite(int num) {
        num /= (int) Math.Pow(10, digit);
        num %= 10;

        currentImage.sprite = imageArray[num];
    }
}
