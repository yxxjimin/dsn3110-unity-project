using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Silder class 사용하기 위해 추가합니다.

public class SliderTimer : MonoBehaviour {
    Slider slTimer;
    float fSliderBarTime;
    float startTimer = 3f;

    void Start() {
        slTimer = GetComponent<Slider>();
    }

    void Update() {
        if (startTimer > 0f) {
            startTimer -= Time.deltaTime;
        } else {
            if (slTimer.value > 0.0f) {
            // 시간이 변경한 만큼 slider Value 변경을 합니다.
                slTimer.value -= Time.deltaTime;
            } else {
                Debug.Log("Time is Zero.");
            }
        }
    }
}
