using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN
{
    internal class Player
    {
        public string Name;
        public int level = 1;
        public string race = "Human";
        public string playerClass;
        public int goldCoins = 0;

        public List<EquipmentCard> equipedEquipmentCards;
        public List<Card> cardsOnHand;
    }
}
