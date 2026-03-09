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

                DoorCard card = randomCardType switch
                {
                    1 => new MonsterCard(),
                    2 => new CurseCard(),
                    3 => new RaceCard(),
                    _ => new ClassCard()
                };

                cards.Add(card);
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

                TreasureCard card = randomCardType switch
                {
                    1 => new OneShotCard(),
                    _ => new EquipmentCard()
                };

                cards.Add(card);
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
