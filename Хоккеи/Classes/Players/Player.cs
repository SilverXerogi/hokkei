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
        public int Id { get; private set; }
        public string Name { get; private set; }
        public PlayerRole Role { get; private set; }
        public int Agility { get; private set; }
        public int Strength { get; private set; }
        public int Stamina { get; private set; }

        public float MaxEnergy { get; private set; }
        public float Energy { get; private set; }

        public Equipment Gear { get; private set; }

        public bool IsOnIce { get; set; }
        public bool IsGoalie { get { return (Role == PlayerRole.Goalie); } }

        protected Player(Int32 id, String name, PlayerRole role, Int32 agility, Int32 strength, Int32 stamina, Equipment gear)
        {
            Id = id;
            Name = name;
            Role = role;
            Agility = agility;
            Strength = strength;
            Stamina = stamina;
            MaxEnergy = 50 + stamina;
            Energy = MaxEnergy;
            Gear = gear;
            IsOnIce = false;
        }
        public void SetGear(Equipment gear)
        {
            Gear = gear;
        }

        public void SetOnIce(Boolean isOnIce)
        {
            IsOnIce = isOnIce;
        }

        public void AddEnergy(Single amount)
        {
            Energy = Energy + amount;

            if (Energy < 0)
            {
                Energy = 0;
            }

            if (Energy > MaxEnergy)
            {
                Energy = MaxEnergy;
            }
        }

        public void DrainEnergy(Single amount)
        {
            Energy = Math.Max(Energy - amount, 0);
        }

        public Int32 GetTotalAgility() => Agility + Gear.TotalAgilityBonus;
        public Int32 GetTotalStrength() => Strength + Gear.TotalStrengthBonus;
        public Int32 GetTotalStamina() => Stamina + Gear.TotalStaminaBonus;

        public Int32 GetAttackChance()
        {
            Int32 baseAgility = Agility + Gear.TotalAgilityBonus;
            Int32 attackBonus = Gear.TotalAttackBonus;
            Int32 total = baseAgility + attackBonus;

            if (total < 0) total = 0;
            if (total > 100) total = 100;
            return total;
        }
        public Int32 GetDefenseChance()
        {
            Int32 baseStrength = Strength + Gear.TotalStrengthBonus;
            Int32 defenseBonus = Gear.TotalDefenseBonus;
            Int32 total = baseStrength + defenseBonus;

            if (total < 0) total = 0;
            if (total > 100) total = 100;
            return total;
        }

        public virtual void TickEnergy()
        {
            if (IsOnIce && Energy > 0)
            {
                Single baseDrain = 1.0f;
                Single weightPenalty = Gear.TotalWeight * 0.01f;
                Single staminaEfficiency = Gear.TotalStaminaEfficiency;
                Single totalDrain = (baseDrain + weightPenalty) * (1.0f - staminaEfficiency);

                totalDrain = Math.Max(totalDrain, 0.1f);

                DrainEnergy(totalDrain);
            }
            else if (!IsOnIce && Energy < MaxEnergy)
            {
                AddEnergy(MaxEnergy * 0.01f);
            }
        }
    }
 }
