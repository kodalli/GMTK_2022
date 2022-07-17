using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private const int CasinoIndex = 2;
    private const int IntroIndex = 1;
    private const int MenuIndex = 0;
    private const int Battle1Index = 3;
    private const int Battle2Index = 3;
    private const int Battle3Index = 3;

    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        // LoadMainMenu();
    }

    public void LoadCasinoScene() {
        SceneManager.LoadScene(CasinoIndex);
    }

    public void LoadIntroScene() {
        SceneManager.LoadScene(IntroIndex);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(MenuIndex);
    }

    public void LoadBattleScene1() {
        SceneManager.LoadScene(Battle1Index);
    }

    public void LoadBattleScene2() {
        SceneManager.LoadScene(Battle2Index);
    }

    public void LoadBattleScene3() {
        SceneManager.LoadScene(Battle3Index);
    }
}