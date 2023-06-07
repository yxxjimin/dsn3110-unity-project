using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // Silder class 사용하기 위해 추가합니다.

public class SliderTimer : MonoBehaviour {
    Slider slTimer;
    float fSliderBarTime;
    [SerializeField] private State stage;

    void Start() {
        slTimer = GetComponent<Slider>();
    }

    void Update() {
        if (stage.startTimer < 0) {
            if (slTimer.value > 0.0f) {
            // 시간이 변경한 만큼 slider Value 변경을 합니다.
                slTimer.value -= Time.deltaTime;
            }
        }
    }
}
