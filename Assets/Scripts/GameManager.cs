using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField] private int casinoIndex;
    [SerializeField] private int introIndex;
    [SerializeField] private int menuIndex;
    [SerializeField] private int battle1Index;
    [SerializeField] private int battle2Index;
    [SerializeField] private int battle3Index;
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
        LoadMainMenu();
    }

    public void LoadCasinoScene() {
        SceneManager.LoadScene(casinoIndex);
    }

    public void LoadIntroScene() {
        SceneManager.LoadScene(introIndex);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(menuIndex);
    }

    public void LoadBattleScene1() {
        SceneManager.LoadScene(battle1Index);
    }

    public void LoadBattleScene2() {
        SceneManager.LoadScene(battle2Index);
    }

    public void LoadBattleScene3() {
        SceneManager.LoadScene(battle3Index);
    }
}