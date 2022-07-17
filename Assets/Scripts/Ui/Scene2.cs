using System;
using System.Collections;
using System.Collections.Generic;
using MainGame.DialogueGraph;
using Ui;
using UnityEngine;
using UnityEngine.Serialization;

public class Scene2 : MonoBehaviour {
    // Start is called before the first frame update
    public DialogueReader reader;
    public PanelOpener blackPanel;
    public GameObject canvas;
    public GameObject characterPanel;
    private bool reachedFirstDialogue;
    public GameObject dice;
    private bool justDied;
    public DialogueContainer secondaryDialogue;
    [SerializeField]private int talked = 0;

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
        GameManager.LoadBattleScene1();
    }

    public void Activate() {
        characterPanel.SetActive(true);
        reader.StartDialogue();
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
        characterPanel.SetActive(false);
        reader.Init();
    }

    public void AfterDialogue() {
        canvas.SetActive(false);
        dice.SetActive(true);
        reachedFirstDialogue = true;
        GameManager.Instance.reachedFirstDialogueTuxedo = true;
    }
}