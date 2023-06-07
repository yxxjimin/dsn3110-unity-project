using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour {
    [SerializeField] private string nextSceneName;
    [SerializeField] private CutSceneDialogue dialogue;

    void Update() {
        if (dialogue.dialogueFinished) SceneManager.LoadScene(nextSceneName);
    }
}
