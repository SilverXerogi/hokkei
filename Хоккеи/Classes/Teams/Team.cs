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
        public Goalie StartingGoalie { get; private set; }
        public Goalie BackupGoalie { get; private set; }
        public Goalie CurrentGoalie { get; private set; }
        public List<Defender> AllDefenders { get; private set; }
        public List<Forward> AllForwards { get; private set; }
        public Line CurrentLine { get; private set; }
        public List<Player> Bench { get; private set; }
        public Int32 Score { get; private set; }

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
            CurrentGoalie = StartingGoalie; 
            AllDefenders = new List<Defender>(defenders);
            AllForwards = new List<Forward>(forwards);
            Score = 0;
            Bench = new List<Player>();

            
            List<Defender> startingDefenders = AllDefenders.Take(2).ToList();
            List<Forward> startingForwards = AllForwards.Take(3).ToList();
            CurrentLine = new Line(startingDefenders, startingForwards);

            
            CurrentGoalie.SetOnIce(true);

            
            Bench.AddRange(AllDefenders.Skip(2));  
            Bench.AddRange(AllForwards.Skip(3));   
        }

        public void SwitchGoalie()
        {
            CurrentGoalie.SetOnIce(false);

            if (CurrentGoalie == StartingGoalie)
            {
                CurrentGoalie = BackupGoalie;
            }
            else
            {
                CurrentGoalie = StartingGoalie;
            }

            CurrentGoalie.SetOnIce(true);
        }

        public void SetLineOnIce(Boolean isOnIce)
        {
            CurrentLine.SetAllOnIce(isOnIce);
        }

        public void SetBenchOnIce(Boolean isOnIce)
        {
            foreach (Player player in Bench)
            {
                player.SetOnIce(isOnIce);
            }
        }

        public void Goal()
        {
            Score++;
        }

        public void ReplaceDefenderInLine(Defender oldDefender, Defender newDefender)
        {
            CurrentLine.ReplaceDefender(oldDefender, newDefender);
            UpdateBench();
        }

        public void ReplaceForwardInLine(Forward oldForward, Forward newForward)
        {
            CurrentLine.ReplaceForward(oldForward, newForward);
            UpdateBench();
        }

        public void SetAttackLine()
        {
            List<Defender> attackDefenders = AllDefenders.Take(2).ToList();
            List<Forward> attackForwards = AllForwards.Take(3).ToList();
            CurrentLine = new Line(attackDefenders, attackForwards);
            UpdateBench();
        }

        public void SetStandardLine()
        {
            List<Defender> standardDefenders = AllDefenders.Take(2).ToList();
            List<Forward> standardForwards = AllForwards.Take(3).ToList();
            CurrentLine = new Line(standardDefenders, standardForwards);
            UpdateBench();
        }

        public void FullLineChange()
        {
            List<Defender> newDefenders = AllDefenders.Where(d => !CurrentLine.Defenders.Contains(d)).Take(2).ToList();
            List<Forward> newForwards = AllForwards.Where(f => !CurrentLine.Forwards.Contains(f)).Take(3).ToList();

            if (newDefenders.Count == 2 && newForwards.Count == 3)
            {
                CurrentLine = new Line(newDefenders, newForwards);
                UpdateBench();
            }
        }

        public List<Defender> GetAvailableDefenders()
        {
            return AllDefenders.Where(d => !CurrentLine.Defenders.Contains(d)).ToList();
        }

        public List<Forward> GetAvailableForwards()
        {
            return AllForwards.Where(f => !CurrentLine.Forwards.Contains(f)).ToList();
        }

        private void UpdateBench()
        {
            Bench.Clear();
            Bench.AddRange(AllDefenders.Where(d => !CurrentLine.Defenders.Contains(d)));
            Bench.AddRange(AllForwards.Where(f => !CurrentLine.Forwards.Contains(f)));
        }

        public void TickEnergy()
        {
            CurrentGoalie.TickEnergy();
            foreach (Player player in AllDefenders)
            {
                player.TickEnergy();
            }
            foreach (Player player in AllForwards)
            {
                player.TickEnergy();
            }
        }

        public void AddEnergyToAll(Single amount)
        {
            StartingGoalie.AddEnergy(amount);
            BackupGoalie.AddEnergy(amount);
            foreach (Player player in AllDefenders)
            {
                player.AddEnergy(amount);
            }
            foreach (Player player in AllForwards)
            {
                player.AddEnergy(amount);
            }
        }
    }
}
