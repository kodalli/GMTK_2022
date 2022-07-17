using System;
using TMPro;
using Ui;
using UnityEngine;

namespace Card {
    public class CardRenderer : MonoBehaviour {

        public GameObject title;
        public GameObject description;
        public PanelOpener opener;
        
        // Put the sprite for the card on the ui after rolling and place its props in text on it
        public void Render(Card card) {

            var titleText = title.GetComponent<TextMeshProUGUI>();
            titleText.SetText("Effect: " + card.statusName);
            
            var descriptionText = description.GetComponent<TextMeshProUGUI>();
            var details = "Description: " + card.effectDescription + "\n\n" + "Strength: " + card.effectStrength + "\n\n" + "Effect applied to: " + card.appliedTo;
            
            descriptionText.SetText(details); 
            
            opener.OpenPanel();
        }

        public bool IsOpen() {
            return opener.isOpen;
        }

        public void Close() {
            opener.ClosePanel();
        }
    }
}