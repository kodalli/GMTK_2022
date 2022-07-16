using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
    [CreateAssetMenu(fileName = "DialogueBlock", menuName = "Dialogue", order = 0)]
    public class DialogueBlock : ScriptableObject {
        [TextArea(15,20)]
        public List<string> voiceLines = new List<string>();

        public List<CheckPoint> checkpoints = new List<CheckPoint>();
        public int index = 0;
        
        [Serializable]
        public struct CheckPoint {
            public string label; 
            public int index;
        }

        public string GoNextLine() {
            index++;
            return index >= voiceLines.Count ? "Out of bounds" : voiceLines[index];
        }

        public string GoToLine(int i) {
            index = i;
            return voiceLines[i];
        }
    }
}