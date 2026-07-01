using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Хоккеи.Classes.equipment.TypesOfEquip
{
    public class Uniform : GearItem
    {
        public Uniform(Int32 agilityBonus, Int32 strengthBonus, Int32 staminaBonus, Int32 weight)
            : base("Форма", agilityBonus, strengthBonus, staminaBonus, weight)
        {
        }
    }
}
