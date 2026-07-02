using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Хоккеи.Classes.Strategies;
using Хоккеи.Classes.Teams;

namespace Хоккеи.Classes.Managers
{
    public class ZamenaManager
    {
        public IZamenaStrategy Strategy { get; set; }

        public ZamenaManager(IZamenaStrategy strategy)
        {
            Strategy = strategy;
        }

        public void UpdateSubstitutions(Team team, TimeManager timeManager)
        {
            Strategy.UpdateLine(team, timeManager);
            
        }

        public void SetStrategy(IZamenaStrategy strategy)
        {
            Strategy = strategy;
        }
    }
}
