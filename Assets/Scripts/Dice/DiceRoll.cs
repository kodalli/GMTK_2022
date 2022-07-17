using System;
using System.Collections.Generic;
using System.Linq;
using Card;
using TMPro;
using UnityEngine;
using Weapon;
using Random = UnityEngine.Random;

namespace Dice {
    public class DiceRoll : MonoBehaviour {
        public List<Card.Card> buffs = new List<Card.Card>();
        public List<Card.Card> debuffs = new List<Card.Card>();
        public CardRenderer renderer;
        public TextMeshProUGUI diceText;

        private void Start() {
            GameManager.Instance.activeStatuses.Clear();
            var rolls = GameManager.Instance.rolls;
            var count = GameManager.Instance.diceCount;
            diceText.SetText("ROLLS LEFT: " + (count - rolls)); 
        }

        public void RollDice() {
            var rolls = GameManager.Instance.rolls;
            var count = GameManager.Instance.diceCount;
            if (rolls >= count) {
                gameObject.SetActive(false);
                return;
            }

            rolls++;
            
            diceText.SetText("ROLLS LEFT: " + (count - rolls)); 

            GameManager.Instance.rolls = rolls;
            var card = SelectBuffOrDebuff();
            card.effectStrength = rolls;
            GameManager.Instance.activeStatuses.Add(card);
            renderer.Render(card);

            if (rolls < count) return;
            gameObject.SetActive(false);
        }

        private Card.Card SelectBuffOrDebuff() {
            var joined = buffs.Union(debuffs).ToArray();
            var i = Random.Range(0, joined.Length);
            return joined[i];
        }
    }
}