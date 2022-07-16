using System;
using System.Collections.Generic;
using Card;
using UnityEngine;
using Weapon;
using Random = UnityEngine.Random;

namespace Dice {
    public class DiceRoll : MonoBehaviour {
        private PlayerData playerData;
        public List<IStatusEffect> buffs = new List<IStatusEffect>();
        public List<IStatusEffect> debuffs = new List<IStatusEffect>();

        private void Start() {
            playerData = GameObject.FindWithTag("Player").GetComponent<PlayerData>();
            
            // Load all the buffs and debuffs cards into the lists.
        }
        
        // 50 50 chance to apply status to player or enemy

        private IWeapon RandomSelectWeapon() {
            var i = Random.Range(0, playerData.weapons.Count);
            return playerData.weapons[i];
        }

        private IStatusEffect SelectBuff(float strength) {
            var i = Random.Range(0, buffs.Count);
            return buffs[i];
        }

        private IStatusEffect SelectDebuff(float strength) {
            var i = Random.Range(0, debuffs.Count);
            return debuffs[i];
        }

        private IStatusEffect SelectBuffOrDebuff(float buffOdds, float strength) {
            var val = Random.value;
            return val < buffOdds ? SelectBuff(strength) : SelectDebuff(strength);
        }
        
#if UNITY_EDITOR
        private readonly Rect buttonRect = new Rect(10, 10, 200, 100);
        private void OnGUI() {
            if (GUI.Button(buttonRect, "Roll Dice")) {
                Debug.Log("dice roll!");
                playerData.DiceCount++;
                Debug.Log(playerData.DiceCount);
            }
        }
#endif
    }
}