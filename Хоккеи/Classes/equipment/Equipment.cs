using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Хоккеи.Classes.equipment
{
    public class Equipment
    {
        public String Name { get; private set; }
        private List<GearItem> Items { get; set; }

        public Int32 TotalAgilityBonus => Items.Sum(item => item.AgilityBonus);
        public Int32 TotalStrengthBonus => Items.Sum(item => item.StrengthBonus);
        public Int32 TotalStaminaBonus => Items.Sum(item => item.StaminaBonus);
        public Int32 TotalWeight => Items.Sum(item => item.Weight);


        public Int32 TotalAttackBonus => Items.Sum(item => item.AttackBonus);
        public Int32 TotalDefenseBonus => Items.Sum(item => item.DefenseBonus);
        public Single TotalStaminaEfficiency => Items.Sum(item => item.StaminaEfficiency);

        public Equipment(String name, List<GearItem> items)
        {
            Name = name;
            Items = items;
        }

        public void AddItem(GearItem item)
        {
            Items.Add(item);
        }
    }
}
