using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialSprite : MonoBehaviour {

    Image currentImage;
    public Sprite[] imageArray;
    [SerializeField] private TutorialState tutorialStage;
    [SerializeField] private float fadeTime;
    [SerializeField] private CutSceneDialogue balanceDialogue;
    [SerializeField] private CutSceneDialogue controlDialogue;
    [SerializeField] private CutSceneDialogue itemConsumeDialogue;
    [SerializeField] private CutSceneDialogue tutorialEndDialogue;
    [SerializeField] private ControllerManager controller;
    private Dictionary<Sprite, bool> displayHistory;
    private bool finishedCoroutine;
    private SerialPort joystick;
    private bool fadeOut;

    void Start() {
        currentImage = GetComponent<Image>();
        displayHistory = new Dictionary<Sprite, bool>();

        for (int i = 0; i < imageArray.Length; i++) {
            displayHistory.Add(imageArray[i], false);
        }
        currentImage.sprite = imageArray[0];

        balanceDialogue.gameObject.SetActive(false);
        controlDialogue.gameObject.SetActive(false);
        itemConsumeDialogue.gameObject.SetActive(false);

        joystick = controller.joystick;

        StartCoroutine(FadeIn());
        fadeOut = true;
    }

    void Update() {
        if (!displayHistory[imageArray[0]]) {
            StartCoroutine(Fade(currentImage));
            displayHistory[imageArray[0]] = true;
        }

        if (!tutorialStage.isBalanced && !displayHistory[imageArray[1]] && finishedCoroutine) {
            balanceDialogue.gameObject.SetActive(true);
            if (balanceDialogue.dialogueFinished) {
                currentImage.sprite = imageArray[1];
                StartCoroutine(Fade(currentImage));
                displayHistory[imageArray[1]] = true;
                tutorialStage.startTask = true;
            }
        } else if (tutorialStage.isBalanced && !tutorialStage.isControlled && !displayHistory[imageArray[2]] && finishedCoroutine) {
            controlDialogue.gameObject.SetActive(true);
            if (controlDialogue.dialogueFinished) {
                currentImage.sprite = imageArray[2];
                StartCoroutine(Fade(currentImage));
                displayHistory[imageArray[2]] = true;
                tutorialStage.startTask = true;
            }
        } else if (tutorialStage.isControlled && !tutorialStage.isItemConsumed && !displayHistory[imageArray[3]] && finishedCoroutine) {
            itemConsumeDialogue.gameObject.SetActive(true);
            if (itemConsumeDialogue.dialogueFinished) {
                currentImage.sprite = imageArray[3];
                StartCoroutine(Fade(currentImage));
                displayHistory[imageArray[3]] = true;
                tutorialStage.startTask = true;
            }
        } 

        if (tutorialStage.isItemConsumed) {
            tutorialEndDialogue.gameObject.SetActive(true);
            if (tutorialEndDialogue.dialogueFinished && fadeOut) {
                StartCoroutine(FadeOut());
                // tutorialStage.isFinished = true;
            } else if (!fadeOut) {
                tutorialStage.isFinished = true;
            }
        }
    }

    IEnumerator Fade(Image img) {
        finishedCoroutine = false;
        Color c = img.color;
        for (float alpha = 0; alpha <= fadeTime; alpha += 0.2f) {
            c.a = alpha / fadeTime;
            img.color = c;
            yield return null;
        }
        for (int i = 0; i < 100; i++) {
            yield return null;
        }
        for (float alpha = fadeTime; alpha >= 0; alpha -= 0.2f) {
            c.a = alpha / fadeTime;
            img.color = c;
            yield return null;
        }
        finishedCoroutine = true;
    }

    IEnumerator FadeIn() {
        Color c = currentImage.color;
        for (float alpha = 1f; alpha >= 0f; alpha -= 0.03f) {
            c.a = alpha < 0 ? 0 : alpha;
            currentImage.color = c;
            yield return null;
        }
    }

    IEnumerator FadeOut() {
        Color c = currentImage.color;
        for (float alpha = 0f; alpha <= 1f; alpha += 0.03f) {
            c.a = alpha > 1 ? 1 : alpha;
            currentImage.color = c;
            yield return null;
        }
        fadeOut = false;
    }
}
