using MUNCHKIN.Cards.TreasureCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN.Factories
{
    internal class OneShotCardFactory
    {
        static List<OneShotEffectData> presets = new()
        {
            new OneShotEffectData { Name = "Potion of Cowardice", TargetType = OneShotTargetType.Player, ModiferRange = (1, 3) },
            new OneShotEffectData { Name = "Potion of Halitosis", TargetType = OneShotTargetType.Enemy, ModiferRange = (-3, -1) },
            new OneShotEffectData { Name = "Magic Lamp", TargetType = OneShotTargetType.InstantWin, ModiferRange = (0,0) }
        };

        private Random rand;
        public OneShotCardFactory(Random rand)
        {
            this.rand = rand;
        }

        internal OneShotCard CreateRandom()
        {
            OneShotCard oneshotcard = new OneShotCard();

            OneShotEffectData preset = presets[rand.Next(presets.Count)];
            oneshotcard.OneShotName = preset.Name;
            oneshotcard.TargetType = preset.TargetType;
            oneshotcard.Modifier = rand.Next(preset.ModiferRange.Item1, preset.ModiferRange.Item2);
            oneshotcard.GoldValue = rand.Next(50, 100);

            oneshotcard.Description = $"Use this card to apply a modifier of {oneshotcard.Modifier} to a {oneshotcard.TargetType.ToString().ToLower()} for the current combat.";

            return oneshotcard;
        }
    }

    public class OneShotEffectData
    {
        public string Name { get; set; }
        public OneShotTargetType TargetType { get; set; }
        public (int, int) ModiferRange { get; set; }
    }
}