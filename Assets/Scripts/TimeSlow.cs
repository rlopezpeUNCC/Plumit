using UnityEngine;
using UnityEngine.UI;

public class TimeSlow : MonoBehaviour {
    [SerializeField]
    Slider slider;
    float charge = 100, drainSpeed = 70f, refillSpeed = 50f;
    bool pressed = false, timeSlowed = false;
    void Update() {
        if (Input.GetKeyDown("q")) {
            if (charge > 15 && !pressed) {
                pressed = true;
                timeSlowed = true;
                Time.timeScale = .5f;
            }
            slider.gameObject.SetActive(true);
        }
        if (pressed && Input.GetKeyUp("q") || charge == 0) {
            pressed = false;
            timeSlowed = false;
            slider.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        if (timeSlowed) {
            Drain();
        } else {
            Refill();
        }

        slider.value = charge/100;
    }

    void Drain() {
        charge -= Time.deltaTime * drainSpeed;
        charge = Mathf.Clamp(charge, 0, 100);
    }

    void Refill() {
        charge += Time.deltaTime * refillSpeed;
        charge = Mathf.Clamp(charge, 0, 100);
    }
}
