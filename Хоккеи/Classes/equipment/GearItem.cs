using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Хоккеи.Classes.equipment
{
    public abstract class GearItem
    {
        public String Name { get; private set; }
        public Int32 AgilityBonus { get; private set; }
        public Int32 StrengthBonus { get; private set; }
        public Int32 StaminaBonus { get; private set; }
        public Int32 Weight { get; private set; }

        public virtual Int32 AttackBonus => 0;
        public virtual Int32 DefenseBonus => 0;
        public virtual Single StaminaEfficiency => 0.0f;


        public GearItem(String name, Int32 agilityBonus, Int32 strengthBonus, Int32 staminaBonus, Int32 weight)
        {
            Name = name;
            AgilityBonus = agilityBonus;
            StrengthBonus = strengthBonus;
            StaminaBonus = staminaBonus;
            Weight = weight;
        }
    }
}
