using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour {
    [SerializeField]
    Text timer;
    float time;
    void Update() {
        time += Time.deltaTime;
        timer.text = (time - time%.01).ToString();
    }
    
}
