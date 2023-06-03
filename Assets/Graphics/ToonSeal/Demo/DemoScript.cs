using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DemoScript : MonoBehaviour {

    public Camera cam;
    public Animator anim;
    public SkinnedMeshRenderer shape;
    public Renderer body;
    public Renderer eyes;
    public Material[] bodyMaterials;
    public Material[] eyesMaterials;
    public ToggleGroup textureToggleGroup;
    public ToggleGroup animationToggleGroup;
    public ToggleGroup eyesToggleGroup;
    public Toggle shapeKeyToggle;


    private Toggle textureToggle;
    private Toggle animationToggle;
    private Toggle eyesToggle;
    private string currentTextureName;
    private string currentAnimationName;
    private string currentEyesPattern;

    void Start() {

        currentTextureName = "white";
        currentAnimationName = "isIdle";
        currentEyesPattern = "pattern1";
        cam.transform.localPosition = new Vector3 (0f, 0.5f, 1.75f);
        cam.transform.localEulerAngles = new Vector3 (0, 180, 0);
    }

    public void OnTextureButton(string textureName) {

        textureToggle = textureToggleGroup.ActiveToggles().FirstOrDefault();
        if (currentTextureName != textureName) {
            switch (textureName) {
                case "white":
                    body.material = bodyMaterials[0];
                    break;
                case "gray":
                    body.material = bodyMaterials[1];
                    break;
                case "black":
                    body.material = bodyMaterials[2];
                    break;
                default:
                    body.material = bodyMaterials[0];
                    break;
            }
            currentTextureName = textureName;
        }
    }

    public void OnAnimationButton(string animationName) {

        animationToggle = animationToggleGroup.ActiveToggles().FirstOrDefault();
        if (currentAnimationName != animationName) {
            anim.Play(animationName, 0, 0.0f);
            currentAnimationName = animationName;
        }
    }

    public void OnShapeKeyButton() {

        if (shapeKeyToggle.isOn) {
            shape.SetBlendShapeWeight(0, 100);
        } else {
            shape.SetBlendShapeWeight(0, 0);
        }
    }

    public void OnEyesButton(string eyesPattern) {

        eyesToggle = eyesToggleGroup.ActiveToggles().FirstOrDefault();
        if (currentEyesPattern != eyesPattern) {
            switch(eyesPattern) {
                case "pattern1":
                    eyes.material = eyesMaterials[0];
                    break;
                case "pattern2":
                    eyes.material = eyesMaterials[1];
                    break;
                case "pattern3":
                    eyes.material = eyesMaterials[2];
                    break;
                case "pattern4":
                    eyes.material = eyesMaterials[3];
                    break;
                case "pattern5":
                    eyes.material = eyesMaterials[4];
                    break;
                case "pattern6":
                    eyes.material = eyesMaterials[5];
                    break;
                case "pattern7":
                    eyes.material = eyesMaterials[6];
                    break;
                case "pattern8":
                    eyes.material = eyesMaterials[7];
                    break;
                default:
                    eyes.material = eyesMaterials[0];
                    break;
            }
            currentEyesPattern = eyesPattern;
        }
    }

    public void OnFrontCameraButton() {

        cam.transform.localPosition = new Vector3 (0f, 0.5f, 1.75f);
        cam.transform.localEulerAngles = new Vector3 (0, 180, 0);
    }

    public void OnLeftCameraButton() {

        cam.transform.localPosition = new Vector3 (-1.45f, 0.9f, 1.2f);
        cam.transform.localEulerAngles = new Vector3 (15, 125, 0);
    }
}
