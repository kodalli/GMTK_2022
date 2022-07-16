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
        
        [Serializable]
        public struct Speaker {
            public Talking head;
            public DialogueBlock dialogue;
        }

        public void Start() {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        public void PlayDialogue(int speakerIdx) {
            var speaker = speakers[speakerIdx];
            StartCoroutine(PlayDialogue(speaker.dialogue.GoNextLine(), speaker.head));
        }
        
        public void PlayDialogue(int speakerIdx, int lineIdx) {
            var speaker = speakers[speakerIdx];
            StartCoroutine(PlayDialogue(speaker.dialogue.GoToLine(lineIdx), speaker.head));
        }

        private void ToggleSkip(bool state) {
            skipButton.enabled = state;
        }

        private IEnumerator PlayDialogue(string text, Talking head) {
            ToggleSkip(false);
            
            head.ToggleTalk();
            
            player.ToggleInput(false);

            var count = 0;
            while (count <= text.Length) {
                yield return new WaitForSeconds(0.04f);
                dialogueText.text = text.Substring(0, count);
                // SoundManager.Instance.PlaySound(sound);
                count++;
            }
            
            head.ToggleTalk();
            
            ToggleSkip(true);
            
            player.ToggleInput(true);
            
            //
            // if (buttonList.Count < 1) {
            //     inputReader.EnableInput();
            //     gameObject.SetActive(false);
            // }
        } 
    }
}