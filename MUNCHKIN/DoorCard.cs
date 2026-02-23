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
        // Base class for all Door-type cards:
        // MonsterCard, CurseCard, RaceCard, ClassCard

        public string Name { get; protected set; }
    }
}
