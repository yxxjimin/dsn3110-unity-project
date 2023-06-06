using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Silder class 사용하기 위해 추가합니다.

public class SliderTimer : MonoBehaviour {
    Slider slTimer;
    float fSliderBarTime;
    private FirstStageState firstStage;

    void Start() {
        slTimer = GetComponent<Slider>();
        firstStage = GameObject.Find("FirstStage").GetComponent<FirstStageState>();
    }

    void Update() {
        if (firstStage.startTimer < 0) {
            if (slTimer.value > 0.0f) {
            // 시간이 변경한 만큼 slider Value 변경을 합니다.
                slTimer.value -= Time.deltaTime;
            }
        }
    }
}
