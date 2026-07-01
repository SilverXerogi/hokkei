using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Хоккеи.Classes.equipment.TypesOfEquip
{
    public class Gloves : GearItem
    {
        public override Int32 DefenseBonus { get; }

        public Gloves(Int32 agilityBonus, Int32 strengthBonus, Int32 staminaBonus, Int32 weight, Int32 defenseBonus)
            : base("Перчатки", agilityBonus, strengthBonus, staminaBonus, weight)
        {
            DefenseBonus = defenseBonus;
        }
    }
}
