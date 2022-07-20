using System.Collections.Generic;
using System.Linq;

namespace Card {
    public static class ApplyStatusEffects {
        private struct EffectFields {
            public int damageBoost;
            public int fireRate;
            public int durability;

            public EffectFields(int damageBoost, int fireRate, int durability) {
                this.durability = durability;
                this.damageBoost = damageBoost;
                this.fireRate = fireRate;
            }
        }

        public static void ApplyPlayer(ref int damageBoost, ref int fireRate, ref int durability) {
            ApplyEffect(ref damageBoost, ref fireRate, ref durability, "Timmy");
        }

        public static void ApplyDemon(ref int damageBoost, ref int fireRate, ref int durability) {
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
    }
}