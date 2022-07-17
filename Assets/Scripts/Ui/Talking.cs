using System;
using UnityEngine;

namespace Ui {
    public class Talking : MonoBehaviour {
        
        private static readonly int IsTalking = Animator.StringToHash("isTalking");

        public void ToggleTalk(bool state) {
            var animator = GetComponent<Animator>();
            // var isOpen = animator.GetBool(IsTalking);
            animator.SetBool(IsTalking, state);
        }
    }
}