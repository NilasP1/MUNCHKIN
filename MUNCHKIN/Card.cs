using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN
{
    internal class Card
    {
    }

    class DoorDeck
    {
        public List<DoorCard> cards = new List<DoorCard>();
        private static Random rand = new Random();

        public void CreateDeck(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                int randomCardType = rand.Next(1, 5);

                if (randomCardType == 1)
                    cards.Add(new MonsterCard());
                else if (randomCardType == 2)
                    cards.Add(new CurseCard());
                else if (randomCardType == 3)
                    cards.Add(new RaceCard());
                else
                    cards.Add(new ClassCard());
            }
        }

        public DoorCard Draw()
        {
            if (cards.Count == 0)
                return null;

            DoorCard card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }

    class TreasureDeck
    {
        public List<TreasureCard> cards = new List<TreasureCard>();
        private static Random rand = new Random();

        public void CreateDeck(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                int randomCardType = rand.Next(1, 3);

                if (randomCardType == 1)
                    cards.Add(new EquipmentCard());
                else
                    cards.Add(new OneShotCard());
            }
        }

        public TreasureCard Draw()
        {
            if (cards.Count == 0)
                return null;

            TreasureCard card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }
}
