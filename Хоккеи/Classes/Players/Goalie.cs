using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Хоккеи.Classes.enums;
using Хоккеи.Classes.equipment;

namespace Хоккеи.Classes.Players
{
    public class Goalie : Player
    {
        public Int32 BaseSaveChance { get; private set; }

        public Goalie(Int32 id, String name, Int32 agility, Int32 strength, Int32 stamina, Int32 baseSaveChance, Equipment gear)
            : base(id, name, PlayerRole.Goalie, agility, strength, stamina, gear)
        {
            BaseSaveChance = baseSaveChance;
        }

        public Int32 GetSaveChance()
        {
            Int32 strengthBonus = GetTotalStrength() / 5;
            return (BaseSaveChance + strengthBonus);
        }
    }
}
