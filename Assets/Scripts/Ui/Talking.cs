using System;
using UnityEngine;

namespace Ui {
    public class Talking : MonoBehaviour {
        
        private static readonly int IsTalking = Animator.StringToHash("isTalking");

        public void ToggleTalk() {
            var animator = GetComponent<Animator>();
            var isOpen = animator.GetBool(IsTalking);
            animator.SetBool(IsTalking, !isOpen);
        }
    }
}