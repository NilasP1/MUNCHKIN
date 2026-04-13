using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN
{
    internal class RaceCardFactory
    {
        private Random rand;

        public RaceCardFactory(Random rand)
        {
            this.rand = rand;
        }

        internal RaceCard CreateRandom()
        {
            RaceCard raceCard = new RaceCard();
            int raceRandomNumber = rand.Next(1, 4);

            raceCard.RaceName = raceRandomNumber switch
            {
                1 => "Elf",
                2 => "Dwarf",
                3 => "Halfling",
                _ => "Unknown"
            };

            raceCard.RaceAbility = raceCard.RaceName switch
            {
                "Elf" => "ExtraFightVictoryLevel",
                "Dwarf" => "ExtraCardsOnHand",
                "Halfling" => "ExtraRewardForSelling",
                _ => "None"
            };

            return raceCard;
        }
    }
}
