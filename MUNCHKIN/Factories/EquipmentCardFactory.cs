using MUNCHKIN.Cards.TreasureCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN.Factories
{
    internal class EquipmentCardFactory
    {
        static List<string> equipmentNames = new List<string>
        {
            "Sword of Destiny", "Shield of Valor", "Helmet of Wisdom", "Boots of Speed", "Armor of Fortitude",
            "Ring of Invisibility", "Amulet of Power", "Bow of Accuracy", "Dagger of Stealth", "Staff of Magic",
            "Cloak of Shadows", "Gauntlets of Strength", "Belt of Giant's Might", "Bracers of Defense", "Lantern of Revealing"
        };

        private Random rand;

        internal EquipmentCardFactory(Random rand)
        {
            this.rand = rand;
        }

        internal EquipmentCard CreateRandom()
        {
            EquipmentCard equipmentcard = new EquipmentCard();

            equipmentcard.EqupipmentName = equipmentNames[rand.Next(equipmentNames.Count)];
            equipmentcard.Slot = GetSlotFromName(equipmentcard.EqupipmentName);

            equipmentcard.BattleBonus = rand.Next(1, 11);
            equipmentcard.GoldValue = rand.Next(100, 1001);
            equipmentcard.specialEffect = "No special effect";
            return equipmentcard;
        }

        private EquipmentSlot GetSlotFromName(string name)
        {
            if (name.Contains("Sword") || name.Contains("Bow") || name.Contains("Dagger") || name.Contains("Staff"))
                return EquipmentSlot.Hands1;

            if (name.Contains("Helmet"))
                return EquipmentSlot.Head;

            if (name.Contains("Boots"))
                return EquipmentSlot.Feet;

            if (name.Contains("Armor"))
                return EquipmentSlot.Body;

            if (name.Contains("Shield"))
                return EquipmentSlot.Hands2;

            // Rings, Amulets, Cloaks, etc.
            return EquipmentSlot.Accessory;
        }
    }
}
