using UnityEngine;

namespace Ui {
    public class PanelOpener : MonoBehaviour {
        public GameObject panel;
        private static readonly int Open = Animator.StringToHash("open");

        public void OpenPanel() {
            if (panel == null) return;
            var animator = panel.GetComponent<Animator>();
            var isOpen = animator.GetBool(Open);
            animator.SetBool(Open, !isOpen);
        }
    }
}