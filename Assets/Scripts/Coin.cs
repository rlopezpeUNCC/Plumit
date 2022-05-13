using UnityEngine;

public class Coin : MonoBehaviour {
    GameHandler gameHandler;
    bool collected = false;
    void Start() {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }
    void OnTriggerEnter() {
        if (collected)
            return;
            
        collected = true;
        print("coin collected");
        gameHandler.CoinCollected();

        Destroy(gameObject);
    }
}
