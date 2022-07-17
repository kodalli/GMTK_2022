using UnityEngine;

namespace Ui {
    public class PanelOpener : MonoBehaviour {
        public GameObject panel;
        private static readonly int Open = Animator.StringToHash("open");
        public bool isOpen => panel.GetComponent<Animator>().GetBool(Open);

        public void OpenPanel() {
            if (panel == null) return;
            var animator = panel.GetComponent<Animator>();
            animator.SetBool(Open, true);
        }
        
        public void ClosePanel() {
            if (panel == null) return;
            var animator = panel.GetComponent<Animator>();
            animator.SetBool(Open, false);
        }
    }
}