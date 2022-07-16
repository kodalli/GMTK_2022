using System;
using UnityEngine;

namespace Card {
    [CreateAssetMenu(fileName = "Card", menuName = "Cards", order = 0)]
    public class Card : ScriptableObject, IStatusEffect {
        public string statusName;
        public string effectDescription;
        public int effectStrength;
        public string appliedTo;

        // Status Name
        // Description
        // Strength
        // Who's its applied to

        public void ApplyEffect(float strength) {
            // Get the enemy and player 
            throw new System.NotImplementedException();
        }

    }
}