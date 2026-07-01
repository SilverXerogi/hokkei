using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Хоккеи.Classes.equipment
{
    public class GearItem
    {
        public String Name { get; private set; }
        public Int32 AgilityBonus { get; private set; }
        public Int32 StrengthBonus { get; private set; }
        public Int32 StaminaBonus { get; private set; }

        public GearItem(String name, Int32 agilityBonus, Int32 strengthBonus, Int32 staminaBonus)
        {
            Name = name;
            AgilityBonus = agilityBonus;
            StrengthBonus = strengthBonus;
            StaminaBonus = staminaBonus;
        }
    }
}
