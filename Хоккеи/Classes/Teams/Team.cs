using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Хоккеи.Classes.Players;

namespace Хоккеи.Classes.Teams
{
    public class Team
    {
        public String Name { get; private set; }
        public Player[] Players { get; }
        public Goalie StartingGoalie { get; private set; }
        public Goalie BackupGoalie { get; private set; }
        
        public Line CurrentLine { get; private set; }
        
        public Int32 Score { get; private set; }


        public Player[] OnIcePlayers => Players.Where(p => p.IsOnIce).ToArray();
        public Player[] Bench => Players.Where(p => !p.IsOnIce).ToArray();

        public Goalie Goalie => OnIcePlayers.OfType<Goalie>().First();
        public Defender[] Defenders => OnIcePlayers.OfType<Defender>().ToArray();
        public Forward[] Forwards => OnIcePlayers.OfType<Forward>().ToArray();

        public Defender[] AllDefenders => Players.OfType<Defender>().ToArray();
        public Forward[] AllForwards => Players.OfType<Forward>().ToArray();

        public Team(String name, Goalie startingGoalie, Goalie backupGoalie,
                    List<Defender> defenders, List<Forward> forwards)
        {
            if (defenders.Count != 8)
                throw new ArgumentException("Команда должна содержать ровно 8 защитников");
            if (forwards.Count != 12)
                throw new ArgumentException("Команда должна содержать ровно 12 нападающих");

            Name = name;
            StartingGoalie = startingGoalie;
            BackupGoalie = backupGoalie;
            Score = 0;
            List<Player> allPlayers = new List<Player>();
            allPlayers.Add(startingGoalie);
            allPlayers.Add(backupGoalie);
            allPlayers.AddRange(defenders);
            allPlayers.AddRange(forwards);
            Players = allPlayers.ToArray();


            startingGoalie.SetOnIce(true);
            backupGoalie.SetOnIce(false);

            List<Defender> startingDefenders = defenders.Take(2).ToList();
            List<Forward> startingForwards = forwards.Take(3).ToList();
            CurrentLine = new Line(startingDefenders, startingForwards);
            CurrentLine.SetAllOnIce(true);
        }

        public void SwitchGoalie()
        {
            Goalie currentGoalie = Goalie;
            currentGoalie.SetOnIce(false);

            if (currentGoalie == StartingGoalie)
            {
                BackupGoalie.SetOnIce(true);
            }
            else
            {
                StartingGoalie.SetOnIce(true);
            }
        }

        public void Goal()
        {
            Score++;
        }

        public void ReplacePlayerInLine(Player oldPlayer, Player newPlayer)
        {
            CurrentLine.ReplacePlayer(oldPlayer, newPlayer);
        }

        public void SetLine(Defender[] defenders, Forward[] forwards)
        {
            foreach (Defender d in AllDefenders) d.SetOnIce(false);
            foreach (Forward f in AllForwards) f.SetOnIce(false);

            CurrentLine = new Line(new List<Defender>(defenders), new List<Forward>(forwards));
            CurrentLine.SetAllOnIce(true);
        }
        public void SetStandardLine()
        {
            foreach (Defender d in AllDefenders) d.SetOnIce(false);
            foreach (Forward f in AllForwards) f.SetOnIce(false);

            List<Defender> standardDefenders = AllDefenders.Take(2).ToList();
            List<Forward> standardForwards = AllForwards.Take(3).ToList();
            CurrentLine = new Line(standardDefenders, standardForwards);
            CurrentLine.SetAllOnIce(true);
        }

        public void SetAttackLine()
        {
            SetStandardLine();
        }


        public void FullLineChange()
        {
            Defender[] currentDefenders = Defenders;
            Forward[] currentForwards = Forwards;

            List<Defender> newDefenders = AllDefenders.Where(d => !currentDefenders.Contains(d)).Take(2).ToList();
            List<Forward> newForwards = AllForwards.Where(f => !currentForwards.Contains(f)).Take(3).ToList();

            if (newDefenders.Count == 2 && newForwards.Count == 3)
            {
                foreach (Defender d in currentDefenders) d.SetOnIce(false);
                foreach (Forward f in currentForwards) f.SetOnIce(false);

                CurrentLine = new Line(newDefenders, newForwards);
                CurrentLine.SetAllOnIce(true);
            }
        }

        public Defender[] GetAvailableDefenders()
        {
            return AllDefenders.Where(d => !CurrentLine.Defenders.Contains(d)).ToArray();
        }

        public Forward[] GetAvailableForwards()
        {
            return AllForwards.Where(f => !CurrentLine.Forwards.Contains(f)).ToArray();
        }

        

        public void TickEnergy()
        {
            foreach (Player player in Players)
            {
                player.TickEnergy();
            }
        }

        public void AddEnergyToAll(Single amount)
        {
            foreach (Player player in Players)
            {
                player.AddEnergy(amount);
            }
        }

        public void SetCurrentLine(Line newLine)
        {
            CurrentLine.SetAllOnIce(false);
            CurrentLine = newLine;
            CurrentLine.SetAllOnIce(true);
        }
    }
}
