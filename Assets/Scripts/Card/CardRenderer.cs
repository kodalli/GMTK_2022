using System;
using TMPro;
using UnityEngine;

namespace Card {
    public class CardRenderer : MonoBehaviour {

        public GameObject title;
        public GameObject description;
        
        // Put the sprite for the card on the ui after rolling and place its props in text on it
        public void Render(Card card) {
            var titleText = title.GetComponent<TextMeshPro>();
            titleText.SetText(card.statusName);
            
            var descriptionText = title.GetComponent<TextMeshPro>();
            var details = card.effectDescription + "\n" + card.effectStrength + "\n" + card.appliedTo;
        }
    }
}