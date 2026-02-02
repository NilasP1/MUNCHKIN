using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN
{
    internal class DoorCard : Card
    {
        public int NumberOfCardsInDorrDeck = 50;
        public List<DoorCard> doorDeck = new List<DoorCard>();
        static Random rand = new Random();

        public DoorCard()
        {
            CreateDoorDeck();
        }

        public void CreateDoorDeck()
        {
            for (int i = 0; i < NumberOfCardsInDorrDeck; i++)
            {
                int randomCardType = rand.Next(1, 5);

                if (randomCardType == 1)
                {
                    doorDeck.Add(new MonsterCard());
                }
                else if (randomCardType == 2)
                {
                    doorDeck.Add(new CurseCard());
                }
                else if (randomCardType == 3)
                {
                    doorDeck.Add(new RaceCard());
                }
                else if (randomCardType == 4)
                {
                    doorDeck.Add(new ClassCard());
                }
            }
        }
    }
}
