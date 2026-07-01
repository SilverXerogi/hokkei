using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Хоккеи.Classes.equipment
{
    public class Equipment
    {
        public string Name {  get; private set; }
        public float AttackBonus { get; private set; }
        public float DefenceBonus { get; private set; }
        public Equipment (string name, float attackBonus, float defenceBonus)
        {
            Name = name;
            AttackBonus = attackBonus;
            DefenceBonus = defenceBonus;
        }
    }
}
