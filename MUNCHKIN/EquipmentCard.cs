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