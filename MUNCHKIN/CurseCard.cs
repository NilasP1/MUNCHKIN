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

        static Random rand = new Random();
        int ranodmNumber = rand.Next(0, 2); 

        public CurseCard()
        {
            if (ranodmNumber == 0)
            {
                CurseName = "Lose a Level";
                CurseEffect = "LoseLevel";
            }
            else if (ranodmNumber == 1)
            {
                CurseName = "Discard Equipment";
                CurseEffect = "DiscardEquipment";
            }
        }
    }
}
