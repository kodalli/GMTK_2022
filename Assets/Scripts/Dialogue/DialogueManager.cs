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
        [SerializeField] private List<Talking> speakers = new List<Talking>();
        [SerializeField] private Button skipButton;

        public void Start() {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        public void PlayDialogue(int speakerIdx, DialogueBlock speech) {
            var speaker = speakers[speakerIdx];
            StartCoroutine(speech.GoNextLine(), speaker);
        }
        
        public void PlayDialogue(int speakerIdx, DialogueBlock speech, int lineIdx) {
            var speaker = speakers[speakerIdx];
            StartCoroutine(speech.GoToLine(lineIdx), speaker);
        }

        private void ToggleSkip(bool state) {
            skipButton.enabled = state;
        }

        private IEnumerator PlayDialogue(string text, Talking speaker) {
            ToggleSkip(false);
            
            speaker.ToggleTalk();
            
            player.ToggleInput(false);

            var count = 0;
            while (count <= text.Length) {
                yield return new WaitForSeconds(0.04f);
                dialogueText.text = text.Substring(0, count);
                // SoundManager.Instance.PlaySound(sound);
                count++;
            }
            
            speaker.ToggleTalk();
            
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