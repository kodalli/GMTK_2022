using System.Linq;

namespace Card {
    
    public class ApplyStatusEffects {
        public void ApplyPlayer(ref int damageBoost, ref int fireRate, ref int durability) {
            var effects = GameManager.Instance.activeStatuses;
            foreach (var card in effects.Where(card => card.appliedTo.Equals("Timmy"))) {
                switch (card.statusName) {
                    case "Heavy Fire":
                        damageBoost += card.effectStrength;
                        break;
                    case "Quickshot":
                        fireRate += card.effectStrength;
                        break;
                    case "Hardened":
                        durability += card.effectStrength;
                        break;
                    case "Brittle":
                        durability += -card.effectStrength;
                        break;
                    case "Jammed":
                        fireRate += -card.effectStrength;
                        break;
                    case "Weak":
                        damageBoost += -card.effectStrength;
                        break;
                }
            }
        }
 
    }
}