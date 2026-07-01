using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Хоккеи.Classes.equipment;
using Хоккеи.Classes.Players;
using Хоккеи.Classes.Teams;

namespace Хоккеи
{
    class Program
    {
        static void Main(String[] args)
        {
            Console.WriteLine("ТЕСТ СИМУЛЯТОРА ХОККЕЙНОГО МАТЧА\n");

            TestEquipmentCreation();

            TestPlayerCreation();

            Team team = TestTeamCreation();

            TestGoalieSwitch(team);

            TestPlayerSubstitution(team);

            TestFullLineChange(team);

            TestAttackLine(team);

            TestEnergy(team);

            Console.WriteLine("\nВСЕ ТЕСТЫ ЗАВЕРШЕНЫ");
            
        }

        static void TestEquipmentCreation()
        {
            Console.WriteLine("ТЕСТ 1: Создание экипировки");

            GearItem jersey = new GearItem("Форма", 2, 3, 5);
            GearItem stick = new GearItem("Клюшка", 5, 0, 2);
            GearItem skates = new GearItem("Коньки", 3, 2, 3);
            GearItem gloves = new GearItem("Перчатки", 2, 1, 2);

            PlayerEquipment equipment = new PlayerEquipment("Полный комплект", jersey, stick, skates, gloves);

            Console.WriteLine($"Название: {equipment.Name}");
            Console.WriteLine($"Бонус к ловкости: {equipment.TotalAgilityBonus}");
            Console.WriteLine($"Бонус к силе: {equipment.TotalStrengthBonus}");
            Console.WriteLine($"Бонус к стамине: {equipment.TotalStaminaBonus}");
            Console.WriteLine();
        }

        static void TestPlayerCreation()
        {
            Console.WriteLine("ТЕСТ 2: Создание игроков");

            GearItem jersey = new GearItem("Форма", 2, 3, 5);
            GearItem stick = new GearItem("Клюшка", 5, 0, 2);
            GearItem skates = new GearItem("Коньки", 3, 2, 3);
            GearItem gloves = new GearItem("Перчатки", 2, 1, 2);
            PlayerEquipment equipment = new PlayerEquipment("Комплект", jersey, stick, skates, gloves);

            Goalie goalie = new Goalie(1, "Иван Петров", 50, 60, 70, 75, equipment);
            Console.WriteLine($"Вратарь: {goalie.Name}");
            Console.WriteLine($"  Базовый шанс сейва: {goalie.BaseSaveChance}%");
            Console.WriteLine($"  Итоговый шанс сейва: {goalie.GetSaveChance()}%");
            Console.WriteLine($"  Макс. энергия: {goalie.MaxEnergy}");
            Console.WriteLine();

            Defender defender = new Defender(2, "Сергей Иванов", 40, 70, 65, equipment);
            Console.WriteLine($"Защитник: {defender.Name}");
            Console.WriteLine($"  Шанс атаки: {defender.GetAttackChance()}%");
            Console.WriteLine($"  Шанс защиты: {defender.GetDefenseChance()}%");
            Console.WriteLine($"  Макс. энергия: {defender.MaxEnergy}");
            Console.WriteLine();

            Forward forward = new Forward(3, "Алексей Смирнов", 75, 45, 60, equipment);
            Console.WriteLine($"Нападающий: {forward.Name}");
            Console.WriteLine($"  Шанс атаки: {forward.GetAttackChance()}%");
            Console.WriteLine($"  Шанс защиты: {forward.GetDefenseChance()}%");
            Console.WriteLine($"  Макс. энергия: {forward.MaxEnergy}");
            Console.WriteLine();
        }

        static Team TestTeamCreation()
        {
            Console.WriteLine("ТЕСТ 3: Создание команды");

            GearItem jersey = new GearItem("Форма", 2, 3, 5);
            GearItem stick = new GearItem("Клюшка", 5, 0, 2);
            GearItem skates = new GearItem("Коньки", 3, 2, 3);
            GearItem gloves = new GearItem("Перчатки", 2, 1, 2);
            PlayerEquipment equipment = new PlayerEquipment("Комплект", jersey, stick, skates, gloves);

            Goalie goalie1 = new Goalie(1, "Вратарь 1", 50, 60, 70, 75, equipment);
            Goalie goalie2 = new Goalie(2, "Вратарь 2", 55, 65, 75, 70, equipment);

            List<Defender> defenders = new List<Defender>();
            for (Int32 i = 0; i < 8; i++)
            {
                Defender defender = new Defender(10 + i, $"Защитник {i + 1}", 40, 70, 65, equipment);
                defenders.Add(defender);
            }

            List<Forward> forwards = new List<Forward>();
            for (Int32 i = 0; i < 12; i++)
            {
                Forward forward = new Forward(100 + i, $"Нападающий {i + 1}", 75, 45, 60, equipment);
                forwards.Add(forward);
            }

            Team team = new Team("Медведи", goalie1, goalie2, defenders, forwards);

            Console.WriteLine($"Команда: {team.Name}");
            Console.WriteLine($"Текущий вратарь: {team.CurrentGoalie.Name}");
            Console.WriteLine($"Защитников всего: {team.AllDefenders.Count}");
            Console.WriteLine($"Нападающих всего: {team.AllForwards.Count}");
            Console.WriteLine($"В текущем звене защитников: {team.CurrentLine.Defenders.Count}");
            Console.WriteLine($"В текущем звене нападающих: {team.CurrentLine.Forwards.Count}");
            Console.WriteLine($"На скамейке игроков: {team.Bench.Count}");
            Console.WriteLine($"Счёт: {team.Score}");
            Console.WriteLine();

            return team;
        }

        static void TestGoalieSwitch(Team team)
        {
            Console.WriteLine("ТЕСТ 4: Смена вратаря");

            Console.WriteLine($"До смены: {team.CurrentGoalie.Name}");
            team.SwitchGoalie();
            Console.WriteLine($"После 1-й смены: {team.CurrentGoalie.Name}");
            team.SwitchGoalie();
            Console.WriteLine($"После 2-й смены: {team.CurrentGoalie.Name}");
            Console.WriteLine();
        }

        static void TestPlayerSubstitution(Team team)
        {
            Console.WriteLine("ТЕСТ 5: Замена игроков в звене");

            Defender oldDefender = team.CurrentLine.Defenders[0];
            Defender newDefender = team.GetAvailableDefenders()[0];

            Console.WriteLine($"Заменяем защитника: {oldDefender.Name} → {newDefender.Name}");
            team.ReplaceDefenderInLine(oldDefender, newDefender);
            Console.WriteLine($"Теперь в звене: {team.CurrentLine.Defenders[0].Name}");
            Console.WriteLine($"На скамейке: {team.Bench.Count} игроков");
            Console.WriteLine();
        }

        static void TestFullLineChange(Team team)
        {
            Console.WriteLine("ТЕСТ 6: Полная смена звена");

            Console.WriteLine("Текущее звено:");
            foreach (Defender d in team.CurrentLine.Defenders)
            {
                Console.WriteLine($"  Защитник: {d.Name}");
            }
            foreach (Forward f in team.CurrentLine.Forwards)
            {
                Console.WriteLine($"  Нападающий: {f.Name}");
            }

            team.FullLineChange();

            Console.WriteLine("После полной замены:");
            foreach (Defender d in team.CurrentLine.Defenders)
            {
                Console.WriteLine($"  Защитник: {d.Name}");
            }
            foreach (Forward f in team.CurrentLine.Forwards)
            {
                Console.WriteLine($"  Нападающий: {f.Name}");
            }
            Console.WriteLine();
        }

        static void TestAttackLine(Team team)
        {
            Console.WriteLine("ТЕСТ 7: Звено атаки");

            team.SetAttackLine();

            Console.WriteLine("Звено атаки (2З + 3Н):");
            foreach (Defender d in team.CurrentLine.Defenders)
            {
                Console.WriteLine($"  Защитник: {d.Name}");
            }
            foreach (Forward f in team.CurrentLine.Forwards)
            {
                Console.WriteLine($"  Нападающий: {f.Name}");
            }
            Console.WriteLine();
        }

        static void TestEnergy(Team team)
        {
            Console.WriteLine("ТЕСТ 8: Энергия");

            Player testPlayer = team.CurrentLine.Forwards[0];
            Console.WriteLine($"Игрок: {testPlayer.Name}");
            Console.WriteLine($"Энергия до: {testPlayer.Energy}/{testPlayer.MaxEnergy}");

            testPlayer.SetOnIce(true);
            for (Int32 i = 0; i < 100; i++)
            {
                testPlayer.TickEnergy();
                Console.WriteLine($"{testPlayer.Energy}/{testPlayer.MaxEnergy}");
            }
            Console.WriteLine($"После 100 секунд на льду: {testPlayer.Energy}/{testPlayer.MaxEnergy}");

            testPlayer.SetOnIce(false);
            for (Int32 i = 0; i < 10; i++)
            {
                testPlayer.TickEnergy();
                Console.WriteLine($"{testPlayer.Energy}/{testPlayer.MaxEnergy}");
            }
            Console.WriteLine($"После 10 секунд на скамейке: {testPlayer.Energy}/{testPlayer.MaxEnergy}");

            team.AddEnergyToAll(40);
            Console.WriteLine($"После перерыва (+40): {testPlayer.Energy}/{testPlayer.MaxEnergy}");
            Console.WriteLine();
        }
    }
}
