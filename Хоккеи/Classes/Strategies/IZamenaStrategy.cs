using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Хоккеи.Classes.Managers;
using Хоккеи.Classes.Teams;

namespace Хоккеи.Classes.Strategies
{
    public interface IZamenaStrategy
    {
        void UpdateLine(Team team, TimeManager timeManager);
        void Reset();
    }
}
