using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNCHKIN
{
    internal class ClassCard : DoorCard
    {
        public string ClassName;
        public string ClassAbility;

        static Random rand = new Random();
        

        public ClassCard()
        {
            int ClassRandomnumber = rand.Next(1, 5);

            if (ClassRandomnumber == 1)
            {
                ClassName = "Warrior";
            }
            else if (ClassRandomnumber == 2)
            {
                ClassName = "Wizard";
            }
            else if (ClassRandomnumber == 3)
            {
                ClassName = "Thief";
            }
            else if (ClassRandomnumber == 4)
            {
                ClassName = "Cleric";
            }
            if (ClassName == "Warrior")
            {
                ClassAbility = "ExtraCombatStrength";
            }
            else if (ClassName == "Wizard")
            {
                ClassAbility = "CastSpells";
            }
            else if (ClassName == "Thief")
            {
                ClassAbility = "StealCards";
            }
            else if (ClassName == "Cleric")
            {
                ClassAbility = "TurnUndead";
            }
        }
    }
}
