using MUNCHKIN.Cards.DoorCards;
using MUNCHKIN.Cards.TreasureCards;
using MUNCHKIN.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN.Cards
{
    public class Card
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    internal class DoorDeck
    {
        public List<DoorCard> Cards { get; } = new List<DoorCard>();
        private Random rand;
        private MonsterCardFactory monsterFactory;
        private CurseCardFactory curseFactory;
        private RaceCardFactory raceFactory;
        private ClassCardFactory classFactory;

        internal DoorDeck(Random rand)
        {
            this.rand = rand;
            monsterFactory = new MonsterCardFactory(rand);
            curseFactory = new CurseCardFactory(rand);
            raceFactory = new RaceCardFactory(rand);
            classFactory = new ClassCardFactory(rand);
        }

        public void CreateDeck(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                int randomCardType = rand.Next(1, 5);

                DoorCard card = randomCardType switch
                {
                    1 => monsterFactory.CreateRandom(),
                    2 => curseFactory.CreateRandom(),
                    3 => raceFactory.CreateRandom(),
                    _ => classFactory.CreateRandom()
                };

                Cards.Add(card);
            }
        }

        public DoorCard Draw()
        {
            if (Cards.Count == 0)
                return null;

            DoorCard card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }
    }

    class TreasureDeck
    {
        public List<TreasureCard> Cards = new List<TreasureCard>();
        private Random rand;
        private EquipmentCardFactory equipmentFactory;
        private OneShotCardFactory oneShotFactory;

        internal TreasureDeck(Random rand)
        {
            this.rand = rand;
            equipmentFactory = new EquipmentCardFactory(rand);
            oneShotFactory = new OneShotCardFactory(rand);
        }

        public void CreateDeck(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                int randomCardType = rand.Next(1, 3);

                TreasureCard card = randomCardType switch
                {
                    1 => oneShotFactory.CreateRandom(),
                    _ => equipmentFactory.CreateRandom()
                };

                Cards.Add(card);
            }
        }


        public TreasureCard Draw()
        {
            if (Cards.Count == 0)
                return null;

            TreasureCard card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }
    }
}
