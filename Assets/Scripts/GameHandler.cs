using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameHandler : MonoBehaviour {
    [SerializeField]
    Text coins;
    [SerializeField]
    GameObject levelCompleteMenu;
    Button mainMenu, cont, restart;
    public static GameHandler instance;
    int totalCoinsCollected, coinsCollectedThisLVL;
    int[] levelCoins = new int[5];
    void Awake() {
        if (instance == null) 
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Update() {
        if (Input.GetKeyDown("r")) {
            Restart();
        }

        if (coins == null || levelCompleteMenu == null)
            FindObjects();
    }
    public void CoinCollected() {
        coinsCollectedThisLVL++;

        coins = GameObject.Find("Coins text").GetComponent<Text>();
        coins.text = coinsCollectedThisLVL.ToString();
    }
    public void LevelComplete() {
        if (coinsCollectedThisLVL > levelCoins[SceneManager.GetActiveScene().buildIndex]) {
            levelCoins[SceneManager.GetActiveScene().buildIndex] = coinsCollectedThisLVL;
        }
        coinsCollectedThisLVL = 0;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        levelCompleteMenu.SetActive(true);
        GameObject panel = levelCompleteMenu.transform.Find("Panel").gameObject;
        cont = panel.transform.Find("Continue").GetComponent<Button>();
        restart = panel.transform.Find("Restart").GetComponent<Button>();
        print(restart.name);
        cont.onClick.AddListener(NextLevel);
        restart.onClick.AddListener(Restart);
    }
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        coinsCollectedThisLVL = 0;
    }
    public void NextLevel() {
        coinsCollectedThisLVL = 0;
        print("next level");
        totalCoinsCollected += levelCoins[SceneManager.GetActiveScene().buildIndex];
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        coins.text = totalCoinsCollected.ToString();
    }
    public void MainMenu() {
        print("returning to main menu");
    }

    void FindObjects() {
        coins = GameObject.Find("Coins text").GetComponent<Text>();
        foreach (GameObject g in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) {
            if (g.name.Contains("Level Complete Menu")) {
                levelCompleteMenu = g;
                break;
            }
        }
    }
}
