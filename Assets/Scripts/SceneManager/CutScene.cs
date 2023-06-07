using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour {
    [SerializeField] private string nextSceneName;
    [SerializeField] private CutSceneDialogue dialogue;
    [SerializeField] private bool fadeOut;
    [SerializeField] Image blackImage;

    void Start() {
        Color c = blackImage.color;
        c.a = 0;
        blackImage.color = c;
    }

    void Update() {
        if (dialogue.dialogueFinished) {
            if (fadeOut) StartCoroutine(FadeOut());
            else SceneManager.LoadScene(nextSceneName);
        }
    }

    IEnumerator FadeOut() {
        Color c = blackImage.color;
        for (float alpha = 0f; alpha <= 1f; alpha += 0.03f) {
            c.a = alpha > 1 ? 1 : alpha;
            blackImage.color = c;
            yield return null;
        }
        fadeOut = false;
    }
}
