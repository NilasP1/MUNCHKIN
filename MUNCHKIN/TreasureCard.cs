using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN
{
    internal class TreasureCard : Card
    {
        public int NumberOfCardsInTreasureDeck = 50;
        public List<TreasureCard> treasureDeck = new List<TreasureCard>();
        static Random rand = new Random();

        public TreasureCard()
        {
            CreateTreasureDeck();
        }

        public void CreateTreasureDeck()
        {
            for (int i = 0; i < NumberOfCardsInTreasureDeck; i++)
            {
                int randomCardType = rand.Next(1, 5);

                if (randomCardType == 1)
                {
                    treasureDeck.Add(new EquipmentCard());
                }
                else if (randomCardType == 2)
                {
                    treasureDeck.Add(new OneShotCard());
                }
            }
        }
    }
}
