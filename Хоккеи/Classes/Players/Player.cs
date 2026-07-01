using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Хоккеи.Classes.enums;
using Хоккеи.Classes.equipment;

namespace Хоккеи
{
    abstract public class Player
    {
        public int id { get; private set; }
        public string Name { get; private set; }
        public PlayerRole Role { get; private set; }
        public int Agility { get; private set; }
        public int Strength { get; private set; }
        public int Stamina { get; private set; }

        public float maxEnergy { get; private set; }
        public float Energy { get; set; }
        public Equipment Gear { get; set; }
        public bool IsOnIce { get; set; }

        public bool IsGoalie { get { return (Role == PlayerRole.Goalie); } }

        protected Player(int id, string name, PlayerRole role, int agility, int strength, int stamina, float maxEnergy, float energy, Equipment gear, bool isOnIce)
        {
            this.id = id;
            Name = name;
            Role = role;
            Agility = agility;
            Strength = strength;
            Stamina = stamina;
            this.maxEnergy = maxEnergy;
            Energy = energy;
        }
    }
 }
