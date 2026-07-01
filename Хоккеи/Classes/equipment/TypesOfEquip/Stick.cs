using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Хоккеи.Classes.equipment.TypesOfEquip
{
    public class Stick : GearItem
    {
        public override Int32 AttackBonus { get; }

        public Stick(Int32 agilityBonus, Int32 strengthBonus, Int32 staminaBonus, Int32 weight, Int32 attackBonus)
            : base("Клюшка", agilityBonus, strengthBonus, staminaBonus, weight)
        {
            AttackBonus = attackBonus;
        }
    }
}
