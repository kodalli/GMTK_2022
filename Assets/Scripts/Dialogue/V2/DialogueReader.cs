using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Ui;
using UnityEngine;
using UnityEngine.UI;

namespace MainGame.DialogueGraph {
    public class DialogueReader : MonoBehaviour {
        [SerializeField] private DialogueContainer dialogue;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Button choicePrefab;

        [SerializeField] private Transform buttonContainer;

        //[SerializeField] private AudioClip sound;
        [SerializeField] private InputProvider inputReader;
        [SerializeField] private PanelOpener panelOpener;
        [SerializeField] private Talking speaker;

        public DialogueContainer Dialogue {
            set => dialogue = value;
        }
        
        public Talking Speaker {
            set => speaker = value;
        }

        private readonly List<Button> buttonList = new List<Button>();
        private bool isTyping;

        private void Start() {
            inputReader = GameObject.FindGameObjectWithTag("Player")
                .GetComponent<PlayerController>().InputProvider;
            inputReader.DisableInput();
            TogglePanel();
            var narrativeData = dialogue.NodeLinks.First(); //Entrypoint node
            ProceedToNarrative(narrativeData.TargetNodeGUID);
        }

        public void TogglePanel() {
           panelOpener.OpenPanel(); 
        }

        private void ProceedToNarrative(string guid) {
            string text = dialogue.DialogueNodeData.Find(x => x.NodeGUID == guid).DialogueText;
            var choices = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == guid);

            var buttons = buttonContainer.GetComponentsInChildren<Button>();
            foreach (var button in buttons)
                Destroy(button.gameObject);

            buttonList.Clear();
            foreach (var choice in choices) {
                var button = Instantiate(choicePrefab, buttonContainer);
                button.GetComponentInChildren<Text>().text = choice.PortName;
                button.onClick.AddListener(() => ProceedToNarrative(choice.TargetNodeGUID));
                button.onClick.AddListener(() => Debug.Log("click"));
                // button.onClick.AddListener(() => SoundManager.Instance.PlayButtonClickSound());
                buttonList.Add(button);
            }

            if (speaker != null) {
                speaker.ToggleTalk(true);
            }
            StartCoroutine(PlayDialogue(text));
        }

        IEnumerator PlayDialogue(string text) {
            ToggleButton(false);

            var count = 0;
            while (count <= text.Length) {
                yield return new WaitForSeconds(0.04f);
                dialogueText.text = text.Substring(0, count);
                // SoundManager.Instance.PlaySound(sound);
                count++;
            }

            ToggleButton(true);

            speaker.ToggleTalk(false);
            
            if (buttonList.Count < 1) {
                inputReader.EnableInput();
                gameObject.SetActive(false);
            }
        }

        private void ToggleButton(bool state) {
            if (buttonList.Count > 0)
                buttonList[0].enabled = state;
        }
    }
}