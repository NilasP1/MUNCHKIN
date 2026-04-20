using MUNCHKIN.Cards;
using MUNCHKIN.Cards.TreasureCards;
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
        public int Level = 1;
        public string Race = "Human";
        public string PlayerClass;
        public int GoldCoins = 0;

        public Dictionary<EquipmentSlot, EquipmentCard?> EquippedItems = new()
        {
            [EquipmentSlot.Accessory] = null,
            [EquipmentSlot.Head] = null,
            [EquipmentSlot.Body] = null,
            [EquipmentSlot.Feet] = null,
            [EquipmentSlot.Hands1] = null,
            [EquipmentSlot.Hands2] = null
        };

        public List<Card> CardsOnHand;
    }
}
