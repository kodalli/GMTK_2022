using System;
using System.Collections;
using System.Collections.Generic;
using MainGame.DialogueGraph;
using Ui;
using UnityEngine;

public class Scene2 : MonoBehaviour {
    // Start is called before the first frame update
    public DialogueReader reader;
    public PanelOpener blackPanel;
    public GameObject canvas;
    private bool reachedFirstDialogue;
    public GameObject dice;
    private bool justDied;
    public DialogueContainer secondaryDialogue;
    private int talked = 0;

    private void Start() {
        reachedFirstDialogue = GameManager.Instance.reachedFirstDialogueTuxedo;
        justDied = GameManager.Instance.justDied;
        // justDied = true;
        if (justDied) {
            GameManager.Instance.diceCount++;
        }
        GameManager.Instance.rolls = 0;
    }

    private void ChangeToBattle() {
        blackPanel.OpenPanel();
        StartCoroutine(NextScene());
    }

    private IEnumerator NextScene() {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.LoadBattleScene1();
    }
    
    public void StartDialogue() {
        talked++;
        if (talked > 1) {
            ChangeToBattle();
            return;
        }
        if (justDied) {
           //  set the new dialogue; 
           reader.Dialogue = secondaryDialogue;
        } else if (reachedFirstDialogue) {
            ChangeToBattle(); 
            return;
        }
        canvas.SetActive(true);
        reader.panelContainer.SetActive(true);
        reader.Init();
    }

    public void AfterDialogue() {
        canvas.SetActive(false);
        dice.SetActive(true);
        reachedFirstDialogue = true;
        GameManager.Instance.reachedFirstDialogueTuxedo = true;
    }
}