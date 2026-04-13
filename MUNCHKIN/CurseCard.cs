using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN
{
    internal class CurseCard : DoorCard
    {
        public string CurseName;
        public string CurseEffect;

        

        internal void ApplyCurseEffect(Player targetPlayer)
        {
            if (CurseEffect == "LoseLevel")
            {
                targetPlayer.level = Math.Max(1, targetPlayer.level - 1);
            }
            else if (CurseEffect == "DiscardEquipment")
            {
                EquipmentSlot DiscardInput;
                Console.WriteLine("Which equipment slot to discard? (Head, Body, Feet, Hands1, Hands2, Accessory)");
                if (Enum.TryParse(Console.ReadLine(), true, out DiscardInput))
                {
                    targetPlayer.EquippedItems[DiscardInput] = null;
                }
            }
        }
    }
}
