using MUNCHKIN.Cards.DoorCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN.Factories
{
    internal class CurseCardFactory
    {
        private Random rand;

        public CurseCardFactory(Random rand)
        {
            this.rand = rand;
        }

        internal CurseCard CreateRandom()
        {
            CurseCard cursecard = new CurseCard();

            int randomNum = rand.Next(0, 2);
            cursecard.CurseEffect = randomNum switch
            {
                0 => "LoseLevel",
                1 => "DiscardEquipment",
                _ => "Unknown Curse"
            };

            cursecard.CurseName = randomNum switch
            {
                0 => "Lose a Level",
                1 => "Discard Equipment",
                _ => "Unknown Curse"
            };

            return cursecard;
        }
    }
}
