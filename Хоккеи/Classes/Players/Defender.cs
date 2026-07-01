using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Хоккеи.Classes.enums;
using Хоккеи.Classes.equipment;

namespace Хоккеи.Classes.Players
{
    public class Defender : Player
    {
        public Defender(Int32 id, String name, Int32 agility, Int32 strength, Int32 stamina, Equipment gear)
            : base(id, name, PlayerRole.Defender, agility, strength, stamina, gear)
        {
        }
    }
}
