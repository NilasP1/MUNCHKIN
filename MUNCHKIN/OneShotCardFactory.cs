using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN
{
    internal class OneShotCardFactory
    {
        static List<string> oneShotNames = new List<string>
        {
            "Potion of Strength", "Scroll of Fireball", "Elixir of Speed", "Bomb of Confusion", "Trap of Binding",
            "Smoke Bomb", "Healing Salve", "Invisibility Potion", "Magic Missile Scroll", "Thunderstone"
        };

        private Random rand;
        public OneShotCardFactory(Random rand)
        {
            this.rand = rand;
        }

        internal OneShotCard CreateRandom()
        {
            OneShotCard oneshotcard = new OneShotCard();

            oneshotcard.OneShotName = oneShotNames[rand.Next(oneShotNames.Count)];
            oneshotcard.OneShotEffect = GenerateEffect(oneshotcard.OneShotName);
            oneshotcard.GoldValue = rand.Next(50, 501);

            return oneshotcard;
        }

        private string GenerateEffect(string name)
        {
            return name switch
            {
                "Potion of Strength" => "Increase your strength by 5 for one battle.",
                "Scroll of Fireball" => "Deal 10 damage to all monsters in the room.",
                "Elixir of Speed" => "Take an extra turn immediately.",
                "Bomb of Confusion" => "Confuse a monster, causing it to skip its next attack.",
                "Trap of Binding" => "Immobilize a monster for one turn.",
                "Smoke Bomb" => "Escape from any battle without penalty.",
                "Healing Salve" => "Restore 10 health points.",
                "Invisibility Potion" => "Avoid all monster attacks for one turn.",
                "Magic Missile Scroll" => "Deal 15 damage to a single monster.",
                "Thunderstone" => "Stun a monster, preventing it from attacking this turn.",
                _ => "No effect."
            };
        }
    }
}
