using MUNCHKIN.Cards.DoorCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN.Factories
{
    internal class MonsterCardFactory
    {
        private static List<string> monsterNames = new List<string>
        {
            "Goblin", "Orc", "Troll", "Dragon", "Vampire", "Zombie", "Giant Spider", "Skeleton", "Werewolf", "Demon"
        };
        private Random rand;

        internal MonsterCardFactory(Random rand)
        {
            this.rand = rand;
        }

        internal MonsterCard CreateRandom()
        {
            MonsterCard monsterCard = new MonsterCard();
            monsterCard.MonsterName = monsterNames[rand.Next(monsterNames.Count)];
            monsterCard.MonsterLevel = rand.Next(1, 21);
            monsterCard.NumberOfTreasures = rand.Next(1, 5);
            monsterCard.NumberOfLevelsToGain = rand.Next(1, 4);
            return monsterCard;
        }
    }
}