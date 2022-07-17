using System;
using MainGame.DialogueGraph;
using UnityEngine;

namespace Ui {
    public class Scene1 : MonoBehaviour {
        public GameObject player;
        public GameObject boss;
        public DialogueReader reader;
        public GameObject buttonHolder;

        private void Start() {
            Toggle(false);
            Invoke(nameof(Activate), 1.2f);
        }

        private void FixedUpdate() {
            if (reader.gameObject.activeSelf) {
                return;
            }

            GameManager.Instance.LoadCasinoScene();
        }

        private void Toggle(bool state) {
            player.SetActive(state);
            boss.SetActive(state);
            buttonHolder.SetActive(state);
        }

        private void Activate() {
            Toggle(true);
            reader.Speaker = boss.GetComponent<Talking>();
        }
    }
}