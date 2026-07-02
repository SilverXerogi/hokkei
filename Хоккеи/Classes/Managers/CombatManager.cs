using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Хоккеи.Classes.enums;
using Хоккеи.Classes.Players;
using Хоккеи.Classes.Teams;

namespace Хоккеи.Classes.Managers
{
    public class CombatManager
    {
        private Random RandomGenerator { get; set; }

        public CombatManager()
        {
            RandomGenerator = new Random();
        }

        public CombatManager(Int32 seed)
        {
            RandomGenerator = new Random(seed);
        }

        public AttackResult ResolveAttack(Team attackingTeam, Team defendingTeam)
        {
            Int32 totalAttackChance = 0;
            foreach (Forward forward in attackingTeam.Forwards)
            {
                totalAttackChance += forward.GetAttackChance();
            }

            Int32 totalDefenseChance = 0;
            foreach (Defender defender in defendingTeam.Defenders)
            {
                totalDefenseChance += defender.GetDefenseChance();
            }

            Int32 attackRoll = RandomGenerator.Next(0, Math.Max(totalAttackChance, 1));
            Int32 defenseRoll = RandomGenerator.Next(0, Math.Max(totalDefenseChance, 1));

            if (attackRoll < defenseRoll)
            {
                return AttackResult.Blocked;
            }

            Int32 saveChance = CalculateGoalieSaveChance(defendingTeam.Goalie);
            Int32 saveRoll = RandomGenerator.Next(0, 100);

            if (saveRoll < saveChance)
            {
                return AttackResult.Saved;
            }

            return AttackResult.Goal;
        }

        

        private Int32 CalculateGoalieSaveChance(Goalie goalie)
        {
            Int32 strengthBonus = goalie.GetTotalStrength() / 5;
            Int32 totalSaveChance = goalie.BaseSaveChance + strengthBonus;

            if (totalSaveChance < 0) totalSaveChance = 0;
            if (totalSaveChance > 100) totalSaveChance = 100;

            return totalSaveChance;
        }
    }
}
