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

        //public EquipmentCard equippedHead;
        //public EquipmentCard equippedBody;
        //public EquipmentCard equippedFeet;
        //public EquipmentCard equippedHand1;
        //public EquipmentCard equippedHand2;
        //public EquipmentCard Accessory;

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
