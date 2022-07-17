using System;
using System.Collections;
using MainGame.DialogueGraph;
using UnityEngine;

namespace Ui {
    public class Scene1 : MonoBehaviour {
        public GameObject player;
        public GameObject boss;
        public DialogueReader reader;
        public GameObject buttonHolder;
        public PanelOpener family;
        public PanelOpener blackScreen;

        private void Start() {
            Toggle(false);
            Invoke(nameof(Activate), 1.2f);
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

        public void FamilyInToScene() {
            family.OpenPanel();
            StartCoroutine(TransitionScene());
        }

        private IEnumerator TransitionScene() {
            yield return new WaitForSeconds(2f);
            blackScreen.OpenPanel();
            yield return new WaitForSeconds(2.5f);
            GameManager.Instance.LoadCasinoScene1();    
        }
    }
}