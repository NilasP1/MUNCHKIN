using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN
{
    internal class MonsterCard : DoorCard
    {
        public string MonsterName;
        public int MonsterLevel;
        public int NumberOfTreasures;
        public int NumberOfLevelsToGain;

        public void MonsterBadStuff(Player player)
        {
            if (player.level > 1)
            {
                player.level -= 1;
            }
        }
    }
}
