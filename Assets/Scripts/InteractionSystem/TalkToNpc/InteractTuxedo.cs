using System;
using UnityEngine;

namespace InteractionSystem.TalkToNpc {
    public class InteractTuxedo : Interactable {
        public Scene2 casino1;
        public PlayerController playerController;
        public bool interact;
        public void OnInteract() {
            // base.OnInteract();
            // Activate dialogue
            casino1.StartDialogue();
        }

        private void OnCollisionEnter2D(Collision2D col) {
            if (col.gameObject.CompareTag("Player")) {
               OnInteract(); 
            }

            Debug.Log(col.gameObject.name);
        }
    }
}