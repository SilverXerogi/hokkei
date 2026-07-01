using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Хоккеи.Classes.equipment
{
    public abstract class Equipment
    {
        public String Name { get; protected set; }

        public abstract Int32 TotalAgilityBonus { get; }
        public abstract Int32 TotalStrengthBonus { get; }
        public abstract Int32 TotalStaminaBonus { get; }
    }
}
