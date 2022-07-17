﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private const int CasinoIndex1 = 2;
    private const int CasinoIndex2 = 4;
    private const int CasinoIndex3 = 5;
    private const int IntroIndex = 1;
    private const int MenuIndex = 0;
    private const int Battle1Index = 6;
    private const int Battle2Index = 3;
    private const int Battle3Index = 3;

    public int rolls = 0;
    public int diceCount = 1;
    public List<Card.Card> activeStatuses = new List<Card.Card>();
    public bool reachedFirstDialogueTuxedo = false;
    public bool justDied = false;

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

    public void LoadCasinoScene1() {
        SceneManager.LoadScene(CasinoIndex1);
    }

    public void LoadCasinoScene2() {
        SceneManager.LoadScene(CasinoIndex2);
    }

    public void LoadCasinoScene3() {
        SceneManager.LoadScene(CasinoIndex3);
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