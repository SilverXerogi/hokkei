using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Хоккеи.Classes.Teams;

namespace Хоккеи.Classes.Managers
{
    public class EnergyManager
    {
        public const Single BreakEnergyRestore = 40.0f;

        public EnergyManager()
        {
        }

        public void UpdateEnergy(Team team1, Team team2, Boolean isBreak)
        {
            if (isBreak)
            {
                RestoreEnergyOnBreak(team1);
                RestoreEnergyOnBreak(team2);
            }
            else
            {
                TickEnergy(team1);
                TickEnergy(team2);
            }
        }

        public void TickEnergy(Team team)
        {
            team.TickEnergy();
        }

        public void RestoreEnergyOnBreak(Team team)
        {
            team.AddEnergyToAll(BreakEnergyRestore);
        }
    }
}
