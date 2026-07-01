using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Хоккеи.Classes.equipment.TypesOfEquip
{
    public class Skates : GearItem
    {
        public override Single StaminaEfficiency { get; }

        public Skates(Int32 agilityBonus, Int32 strengthBonus, Int32 staminaBonus, Int32 weight, Single staminaEfficiency)
            : base("Коньки", agilityBonus, strengthBonus, staminaBonus, weight)
        {
            StaminaEfficiency = staminaEfficiency;
        }
    }
}
