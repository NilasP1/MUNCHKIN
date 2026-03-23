using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN
{
    internal class EquipmentCard : TreasureCard
    {
        public string EqupipmentName;
        public int BattleBonus;

        public EquipmentSlot Slot;
        public string specialEffect;

        static Random random = new Random();
        static List<string> equipmentNames = new List<string>
        {
            "Sword of Destiny", "Shield of Valor", "Helmet of Wisdom", "Boots of Speed", "Armor of Fortitude",
            "Ring of Invisibility", "Amulet of Power", "Bow of Accuracy", "Dagger of Stealth", "Staff of Magic", 
            "Cloak of Shadows", "Gauntlets of Strength", "Belt of Giant's Might", "Bracers of Defense", "Lantern of Revealing"
        };

        public EquipmentCard()
        {
            EqupipmentName = equipmentNames[random.Next(equipmentNames.Count)];
            Slot = GetSlotFromName(EqupipmentName);

            BattleBonus = random.Next(1, 11);
            GoldValue = random.Next(100, 1001);
            specialEffect = "No special effect";
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

public enum EquipmentSlot
{
    Head,
    Body,
    Feet,
    Hands1,
    Hands2,
    Accessory
}