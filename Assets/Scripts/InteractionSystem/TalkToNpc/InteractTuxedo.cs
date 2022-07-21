using System;
using UnityEngine;

namespace InteractionSystem.TalkToNpc {
    public class InteractTuxedo : Interactable {
        public Scene2 casino1;
        public PlayerController playerController;
        public bool interact;
        public override void OnInteract() {
            base.OnInteract();
            casino1.StartDialogue();
        }

    }
}