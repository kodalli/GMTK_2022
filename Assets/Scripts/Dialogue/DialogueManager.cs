using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ui;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue {
    public class DialogueManager : MonoBehaviour {
        [SerializeField] private AudioClip sound;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private PlayerController player;
        [SerializeField] private List<Speaker> speakers = new List<Speaker>();
        [SerializeField] private Button skipButton;
        [SerializeField] private Speaker currentSpeaker;

        [Serializable]
        public struct Speaker {
            public Talking head;
            public DialogueBlock dialogue;
        }

        public void Start() {
            // player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            foreach (var s in speakers) {
                s.dialogue.index = 0;
            }
        }

        // Based on what the player clicks on the speaker will change
        public void SetCurrentSpeaker(int speakerIdx) {
            currentSpeaker = speakers[speakerIdx];
            Debug.Log("current speaker" + speakerIdx);
        }

        // This is terrible but no time lol
        private void updateDialogueTree() {
            switch (currentSpeaker.head.name) {
                case "Player":
                    break;
                case "Enemy1":
                    // Tux guy

                    // Boss interaction in casino -> go to fight scene
                    if (false) {
                        GameManager.Instance.LoadBattleScene1();
                    }

                    // Boss fight beginning

                    // Boss fight defeat
                    if (false) {
                        GameManager.Instance.LoadCasinoScene();
                    }

                    // Boss fight victory 
                    if (false) {
                        GameManager.Instance.LoadCasinoScene();
                    }

                    break;
                case "Enemy2":
                    // Slimy guy

                    // Boss interaction in casino -> go to fight scene
                    if (false) {
                        GameManager.Instance.LoadBattleScene2();
                    }

                    // Boss fight beginning

                    // Boss fight defeat
                    if (false) {
                        GameManager.Instance.LoadCasinoScene();
                    }

                    // Boss fight victory 
                    if (false) {
                        GameManager.Instance.LoadCasinoScene();
                    }

                    break;
                case "Enemy3":
                    // Final boss guy

                    // Intro dialogue
                    if (currentSpeaker.dialogue.index == 4) {
                        // Go to the casino scene
                        GameManager.Instance.LoadBattleScene3();
                    }

                    // Boss interaction in casino -> go to fight scene

                    // Boss fight beginning

                    // Boss fight defeat
                    if (false) {
                        GameManager.Instance.LoadCasinoScene();
                    }

                    // Boss fight victory 
                    if (false) {
                        // End scene
                    }

                    break;
            }
        }

        public void PlayDialogue() {
            currentSpeaker.head.ToggleTalk(true);
            StartCoroutine(PlayDialogue(currentSpeaker.dialogue.GoNextLine(), currentSpeaker.head));

            // I'm lazy so just do a bunch if statements for each dialogue thing
        }

        // public void PlayDialogue(int speakerIdx) {
        //     var speaker = speakers[speakerIdx];
        //     StartCoroutine(PlayDialogue(speaker.dialogue.GoNextLine(), speaker.head));
        // }
        //
        // public void PlayDialogue(int speakerIdx, int lineIdx) {
        //     var speaker = speakers[speakerIdx];
        //     StartCoroutine(PlayDialogue(speaker.dialogue.GoToLine(lineIdx), speaker.head));
        // }

        private void ToggleSkip(bool state) {
            skipButton.enabled = state;
        }

        private IEnumerator PlayDialogue(string text, Talking head) {
            // ToggleSkip(false);


            // player.ToggleInput(false);

            var count = 0;
            while (count <= text.Length) {
                yield return new WaitForSeconds(0.04f);
                Debug.Log(61);
                dialogueText.text = text.Substring(0, count);
                // SoundManager.Instance.PlaySound(sound);
                count++;
            }

            head.ToggleTalk(false);

            // ToggleSkip(true);

            // player.ToggleInput(true);

            //
            // if (buttonList.Count < 1) {
            //     inputReader.EnableInput();
            //     gameObject.SetActive(false);
            // }
        }
    }
}