using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN.Cards.DoorCards
{
    internal class MonsterCard : DoorCard
    {
        public string MonsterName;
        public int MonsterLevel;
        public int NumberOfTreasures;
        public int NumberOfLevelsToGain;

        public void MonsterBadStuff(Player player)
        {
            if (player.Level > 1)
            {
                player.Level -= 1;
            }
        }
    }
}
