using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN
{
    internal class ClassCardFactory
    {
        private Random rand;

        public ClassCardFactory(Random rand)
        {
            this.rand = rand;
        }

        internal ClassCard CreateRandom()
        {
            ClassCard classCard = new ClassCard();
            int classRandomNumber = rand.Next(1, 5);

            (classCard.ClassName, classCard.ClassAbility) = classRandomNumber switch
            {
                1 => ("Warrior", "ExtraCombatStrength"),
                2 => ("Wizard", "CastSpells"),
                3 => ("Thief", "StealCards"),
                4 => ("Cleric", "TurnUndead"),
                _ => ("Unknown", "None")
            };

            return classCard;
        }
    }
}
