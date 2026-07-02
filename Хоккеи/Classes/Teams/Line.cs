using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Хоккеи.Classes.Players;

namespace Хоккеи.Classes.Teams
{
    public class Line
    {
        public List<Defender> Defenders { get; private set; }
        public List<Forward> Forwards { get; private set; }

        public Line(List<Defender> defenders, List<Forward> forwards)
        {
            if (defenders.Count != 2)
                throw new ArgumentException("Звено должно содержать ровно 2 защитника");
            if (forwards.Count != 3)
                throw new ArgumentException("Звено должно содержать ровно 3 нападающих");

            Defenders = new List<Defender>(defenders);
            Forwards = new List<Forward>(forwards);
        }

        public List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();
            players.AddRange(Defenders);
            players.AddRange(Forwards);
            return players;
        }

        public void SetAllOnIce(Boolean isOnIce)
        {
            foreach (Defender defender in Defenders)
            {
                defender.SetOnIce(isOnIce);
            }
            foreach (Forward forward in Forwards)
            {
                forward.SetOnIce(isOnIce);
            }
        }

        public void ReplaceDefender(Defender oldDefender, Defender newDefender)
        {
            Int32 index = Defenders.IndexOf(oldDefender);
            if (index >= 0)
            {
                oldDefender.SetOnIce(false);
                newDefender.SetOnIce(true);
                Defenders[index] = newDefender;
            }
        }

        public void ReplaceForward(Forward oldForward, Forward newForward)
        {
            Int32 index = Forwards.IndexOf(oldForward);
            if (index >= 0)
            {
                oldForward.SetOnIce(false);
                newForward.SetOnIce(true);
                Forwards[index] = newForward;
            }
        }
    }
}
