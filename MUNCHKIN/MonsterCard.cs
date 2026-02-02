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

        static Random random = new Random();
        static List<string> monsterNames = new List<string>
        {
            "Goblin", "Orc", "Troll", "Dragon", "Vampire", "Zombie", "Giant Spider", "Skeleton", "Werewolf", "Demon"
        };
        public MonsterCard()
        {
            MonsterName = monsterNames[random.Next(monsterNames.Count)];
            MonsterLevel = random.Next(1, 21); 
            NumberOfTreasures = random.Next(1, 5); 
            NumberOfLevelsToGain = random.Next(1, 4); 
        }

        public void MonsterBadStuff(Player player)
        {
            if (player.level > 1)
            {
                player.level -= 1;
            }
        }
    }
}
