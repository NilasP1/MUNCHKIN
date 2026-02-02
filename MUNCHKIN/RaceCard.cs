using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN
{
    internal class RaceCard : DoorCard
    {
        public string RaceName;
        public string RaceAbility;

        static Random rand = new Random();
        int RaceRandomnumber = rand.Next(1, 4);

        public RaceCard()
        {
            if (RaceRandomnumber == 1)
            {
                RaceName = "Elf";
            }
            else if (RaceRandomnumber == 2)
            {
                RaceName = "Dwarf";
            }
            else if (RaceRandomnumber == 3)
            {
                RaceName = "Halfling";
            }

            if (RaceName == "Elf")
            {
                RaceAbility = "ExtraFightVictoryLevel";
            }
            else if (RaceName == "Dwarf")
            {
                RaceAbility = "ExtraCardsOnHand";
            }
            else if (RaceName == "Halfling")
            {
                RaceAbility = "ExtraRewardForSelling";
            }
        }
    }
}
