using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Хоккеи.Classes.enums;
using Хоккеи.Classes.Managers;
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
        public ZamenaManager ZamenaManager { get; private set; }
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
            ZamenaManager = new ZamenaManager(new RollingZamenaStrategy());
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

            ZamenaManager.UpdateSubstitutions(Team1, Team2, TimeManager);

            EnergyManager.UpdateEnergy(Team1, Team2, isBreak: false);

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

            if (result == AttackResult.Goal)
            {
                attackingTeam.Goal();
                Console.WriteLine($"[{TimeManager.GetFormattedTime()}] ГОЛ {attackingTeam.Name} забивает Счёт: {Team1.Name} {Team1.Score} - {Team2.Score} {Team2.Name}");
            }
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
    }
}
