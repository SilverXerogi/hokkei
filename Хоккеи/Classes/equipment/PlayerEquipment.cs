using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Хоккеи.Classes.equipment
{
    public class PlayerEquipment : Equipment
    {
        public GearItem Jersey { get; private set; }
        public GearItem Stick { get; private set; }
        public GearItem Skates { get; private set; }
        public GearItem Gloves { get; private set; }

        public override Int32 TotalAgilityBonus
            => Jersey.AgilityBonus + Stick.AgilityBonus + Skates.AgilityBonus + Gloves.AgilityBonus;

        public override Int32 TotalStrengthBonus
            => Jersey.StrengthBonus + Stick.StrengthBonus + Skates.StrengthBonus + Gloves.StrengthBonus;

        public override Int32 TotalStaminaBonus
            => Jersey.StaminaBonus + Stick.StaminaBonus + Skates.StaminaBonus + Gloves.StaminaBonus;

        public PlayerEquipment(String name, GearItem jersey, GearItem stick, GearItem skates, GearItem gloves)
        {
            Name = name;
            Jersey = jersey ?? throw new ArgumentNullException(nameof(jersey));
            Stick = stick ?? throw new ArgumentNullException(nameof(stick));
            Skates = skates ?? throw new ArgumentNullException(nameof(skates));
            Gloves = gloves ?? throw new ArgumentNullException(nameof(gloves));
        }
    }
}
