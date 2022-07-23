using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Card {
    public static class StatusEffects {
        public static void ApplyPlayer(ref int damageBoost, ref int fireRate, ref int durability) {
            ApplyEffect(ref damageBoost, ref fireRate, ref durability, "Timmy");
        }

        public static void ApplyEnemy(ref int damageBoost, ref int fireRate, ref int durability) {
            ApplyEffect(ref damageBoost, ref fireRate, ref durability, "Demon");
        }

        private static void ApplyEffect(ref int damageBoost, ref int fireRate, ref int durability,
            string entity) {
            var effect = new EffectFields(damageBoost, fireRate, durability);
            effect = FromEffects(entity).Aggregate(effect, ApplyEffect);
            damageBoost = effect.damageBoost;
            fireRate = effect.fireRate;
            durability = effect.durability;
        }

        private static IEnumerable<Card> FromEffects(string entity) {
            var effects = GameManager.Instance.activeStatuses;
            return effects.Where(card => card.appliedTo.Equals(entity));
        }

        private static EffectFields ApplyEffect(EffectFields effect, Card card) {
            switch (card.statusName) {
                case "Heavy Fire":
                    effect.damageBoost += card.effectStrength;
                    break;
                case "Quickshot":
                    effect.fireRate += card.effectStrength;
                    break;
                case "Hardened":
                    effect.durability += card.effectStrength;
                    break;
                case "Brittle":
                    effect.durability += -card.effectStrength;
                    break;
                case "Jammed":
                    effect.fireRate += -card.effectStrength;
                    break;
                case "Weak":
                    effect.damageBoost += -card.effectStrength;
                    break;
            }

            return effect;
        }

        public static int GetFactor(float baseD, float field) {
            if (field < 0) {
                var f = 1 + (-field / 5f);
                return Mathf.CeilToInt(baseD * f);
            }
            else {
                return Mathf.CeilToInt(baseD - field / 5);
            }
        }

    }

    public struct EffectFields {
        public int damageBoost;
        public int fireRate;
        public int durability;

        public EffectFields(int damageBoost, int fireRate, int durability) {
            this.durability = durability;
            this.damageBoost = damageBoost;
            this.fireRate = fireRate;
        }
    }
}