using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Хоккеи.Classes.enums;
using Хоккеи.Classes.Managers;
using Хоккеи.Classes.Players;
using Хоккеи.Classes.Strategies;
using Хоккеи.Classes.Teams;

namespace Хоккеи.Classes.Match
{
    public class Match
    {
        public Team Team1 { get; private set; }
        public Team Team2 { get; private set; }
        public TimeManager TimeManager { get; private set; }
        public EnergyManager EnergyManager { get; private set; }
        public ZamenaManager ZamenaManager1 { get; private set; }
        public ZamenaManager ZamenaManager2 { get; private set; }
        public CombatManager CombatManager { get; private set; }

        public Boolean IsMatchOver
        {
            get { return TimeManager.IsMatchOver; }
        }

        public Match(Team team1, Team team2)
        {
            Team1 = team1;
            Team2 = team2;
            TimeManager = new TimeManager();
            EnergyManager = new EnergyManager();
            ZamenaManager1 = new ZamenaManager(new RollingZamenaStrategy());
            ZamenaManager2 = new ZamenaManager(new RollingZamenaStrategy());
            CombatManager = new CombatManager();

            TimeManager.PeriodEnded += OnPeriodEnded;
            TimeManager.BreakStarted += OnBreakStarted;
            TimeManager.BreakEnded += OnBreakEnded;
            TimeManager.MatchEnded += OnMatchEnded;

            Team1.SetLineOnIce(true);
            Team1.SetBenchOnIce(false);
            Team2.SetLineOnIce(true);
            Team2.SetBenchOnIce(false);
        }

        public Match(Team team1, Team team2, Int32 seed) : this(team1, team2)
        {
            CombatManager = new CombatManager(seed);
        }

        public void SimulateTick()
        {
            if (TimeManager.IsMatchOver)
            {
                return;
            }

            TimeManager.Tick();

            if (TimeManager.IsMatchOver || TimeManager.IsBreak)
            {
                return;
            }

            ZamenaManager1.UpdateSubstitutions(Team1, TimeManager);
            ZamenaManager2.UpdateSubstitutions(Team2, TimeManager);

            EnergyManager.UpdateEnergy(Team1, Team2, isBreak: false);
            /*
            if ((TimeManager.CurrentSecond == 59)^(TimeManager.CurrentSecond == 1))
            {
                LogLineStatus();
            }
            */
            if (TimeManager.CurrentSecond == 0)
            {
                ResolveCurrentMinuteAttack();
            }
        }

        private void ResolveCurrentMinuteAttack()
        {
            Int32 currentMinute = TimeManager.CurrentMinute;

            Team attackingTeam;
            Team defendingTeam;

            if (currentMinute % 2 != 0)
            {
                attackingTeam = Team1;
                defendingTeam = Team2;
            }
            else
            {
                attackingTeam = Team2;
                defendingTeam = Team1;
            }

            AttackResult result = CombatManager.ResolveAttack(attackingTeam, defendingTeam);

            String resultText;
            switch (result)
            {
                case AttackResult.Blocked:
                    resultText = "Защитники спасли";
                    break;
                case AttackResult.Saved:
                    resultText = "Вратарь спас";
                    break;
                case AttackResult.Goal:
                    attackingTeam.Goal();
                    resultText = "Гол";
                    break;
                default:
                    resultText = "Неизвестно";
                    break;
            }
            Console.WriteLine($"{TimeManager.GetFormattedTime()}  {attackingTeam.Name}  атакуют  {defendingTeam.Name}   {resultText}");
        }

        private void OnPeriodEnded()
        {
            Console.WriteLine($"\nКонец {TimeManager.CurrentPeriod} периода");
            Console.WriteLine($"Счёт: {Team1.Name} {Team1.Score} - {Team2.Score} {Team2.Name}\n");
        }

        private void OnBreakStarted()
        {
            Console.WriteLine("Перерыв");
            
            EnergyManager.UpdateEnergy(Team1, Team2, isBreak: true);
        }

        private void OnBreakEnded()
        {

            ZamenaManager1.Strategy.Reset();
            ZamenaManager2.Strategy.Reset();


            Team1.SetStandardLine();
            Team2.SetStandardLine();
            
            
            Console.WriteLine($"Начало {TimeManager.CurrentPeriod} периода\n");

            Team1.SwitchGoalie();
            Team2.SwitchGoalie();

            Console.WriteLine($"Вратари: {Team1.Name} - {Team1.CurrentGoalie.Name}, {Team2.Name} - {Team2.CurrentGoalie.Name}");
        }

        private void OnMatchEnded()
        {
            Console.WriteLine("         МАТЧ ОКОНЧЕН");

            Console.WriteLine($"Финальный счёт: {Team1.Name} {Team1.Score} - {Team2.Score} {Team2.Name}");

            if (Team1.Score > Team2.Score)
            {
                Console.WriteLine($"Победитель: {Team1.Name}");
            }
            else if (Team2.Score > Team1.Score)
            {
                Console.WriteLine($"Победитель: {Team2.Name}");
            }
            else
            {
                Console.WriteLine("Ничья!");
            }
        }

        private void LogLineStatus()
        {
            Console.WriteLine($"\n {TimeManager.GetFormattedTime()}  Состояние звеньев на льду:  ");

            Console.WriteLine($"{Team1.Name}: ");
            foreach (Defender d in Team1.CurrentLine.Defenders)
            {
                Console.WriteLine($"{d.Name}: energy: {d.Energy}/{d.MaxEnergy}   IsOnIce = {d.IsOnIce}");
            }
            foreach (Forward f in Team1.CurrentLine.Forwards)
            {
                Console.WriteLine($"{f.Name}: energy: {f.Energy}/{f.MaxEnergy}  IsOnIce = { f.IsOnIce}");
            }
            foreach (Defender d in Team2.CurrentLine.Defenders)
            {
                Console.WriteLine($"{d.Name}: energy: {d.Energy}/{d.MaxEnergy}  IsOnIce = { d.IsOnIce}");
            }
            foreach (Forward f in Team2.CurrentLine.Forwards)
            {
                Console.WriteLine($"{f.Name}: energy: {f.Energy}/{f.MaxEnergy}  IsOnIce = { f.IsOnIce}");
            }
        }
    }
}
