using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Хоккеи.Classes.equipment;
using Хоккеи.Classes.equipment.TypesOfEquip;
using Хоккеи.Classes.Managers;
using Хоккеи.Classes.Players;
using Хоккеи.Classes.Teams;

namespace Хоккеи
{
    class Program
    {
        static void Main(String[] args)
        {
            Console.WriteLine("  ТЕСТ СИМУЛЯТОРА ХОККЕЙНОГО МАТЧА");
   

            TestEquipmentInheritance();
            TestEquipmentSpecialization();
            TestEquipmentWeight();
            TestPlayerWithEquipment();

            Team team = TestTeamCreation();

            TestGoalieSwitch(team);
            TestPlayerSubstitution(team);
            TestFullLineChange(team);
            TestAttackLine(team);
            TestEnergy(team);
            TestTimeManager();

            Console.WriteLine("  ВСЕ ТЕСТЫ ЗАВЕРШЕНЫ");

        }

        static void TestEquipmentInheritance()
        {
            Console.WriteLine("ТЕСТ 1: Наследование экипировки");
         
            Uniform Uniform = new Uniform(2, 3, 5, 2);
            Stick stick = new Stick(5, 0, 2, 1, 10);
            Skates skates = new Skates(3, 2, 3, 3, 0.10f);
            Gloves gloves = new Gloves(2, 1, 2, 1, 8);

            Console.WriteLine($"Uniform.Name = '{Uniform.Name}' (ожидается: 'Форма')");
            Console.WriteLine($"Stick.Name = '{stick.Name}' (ожидается: 'Клюшка')");
            Console.WriteLine($"Skates.Name = '{skates.Name}' (ожидается: 'Коньки')");
            Console.WriteLine($"Gloves.Name = '{gloves.Name}' (ожидается: 'Перчатки')");

            Console.WriteLine($"\nВсе предметы являются GearItem:");
            Console.WriteLine($"  Uniform is GearItem: {Uniform is GearItem}");
            Console.WriteLine($"  stick is GearItem: {stick is GearItem}");
            Console.WriteLine($"  skates is GearItem: {skates is GearItem}");
            Console.WriteLine($"  gloves is GearItem: {gloves is GearItem}");

            Boolean isValid = Uniform.Name == "Форма" &&
                             stick.Name == "Клюшка" &&
                             skates.Name == "Коньки" &&
                             gloves.Name == "Перчатки" &&
                             Uniform is GearItem &&
                             stick is GearItem &&
                             skates is GearItem &&
                             gloves is GearItem;
            Console.WriteLine($"\nРезультат: {(isValid ? "ПРОЙДЕН" : "ПРОВАЛЕН")}\n");
        }

        static void TestEquipmentSpecialization()
        {
            Console.WriteLine("ТЕСТ 2: Специализация экипировки");
      
            Uniform Uniform = new Uniform(2, 3, 5, 2);
            Stick stick = new Stick(5, 0, 2, 1, 10);
            Skates skates = new Skates(3, 2, 3, 3, 0.10f);
            Gloves gloves = new Gloves(2, 1, 2, 1, 8);

            Console.WriteLine("Специализированные бонусы:");
            Console.WriteLine($"  Uniform:   Attack={Uniform.AttackBonus}, Defense={Uniform.DefenseBonus}, Efficiency={Uniform.StaminaEfficiency}");
            Console.WriteLine($"  Stick:    Attack={stick.AttackBonus}, Defense={stick.DefenseBonus}, Efficiency={stick.StaminaEfficiency}");
            Console.WriteLine($"  Skates:   Attack={skates.AttackBonus}, Defense={skates.DefenseBonus}, Efficiency={skates.StaminaEfficiency}");
            Console.WriteLine($"  Gloves:   Attack={gloves.AttackBonus}, Defense={gloves.DefenseBonus}, Efficiency={gloves.StaminaEfficiency}");

            Boolean stickCorrect = stick.AttackBonus == 10 &&
                                   stick.DefenseBonus == 0 &&
                                   stick.StaminaEfficiency == 0.0f;
            Console.WriteLine($"\n  Клюшка даёт ТОЛЬКО атаку: {(stickCorrect ? "✓" : "✗")}");

            Boolean glovesCorrect = gloves.AttackBonus == 0 &&
                                    gloves.DefenseBonus == 8 &&
                                    gloves.StaminaEfficiency == 0.0f;
            Console.WriteLine($"  Перчатки дают ТОЛЬКО защиту: {(glovesCorrect ? "✓" : "✗")}");

            Boolean skatesCorrect = skates.AttackBonus == 0 &&
                                    skates.DefenseBonus == 0 &&
                                    skates.StaminaEfficiency == 0.10f;
            Console.WriteLine($"  Коньки дают ТОЛЬКО эффективность: {(skatesCorrect ? "✓" : "✗")}");

            Boolean isValid = stickCorrect && glovesCorrect && skatesCorrect;
            Console.WriteLine($"\nРезультат: {(isValid ? "✓ ПРОЙДЕН" : "✗ ПРОВАЛЕН")}\n");
        }

        static void TestEquipmentWeight()
        {
            Console.WriteLine("ТЕСТ 3: Вес экипировки");
          
            Uniform Uniform = new Uniform(2, 3, 5, 2);    // 2 кг
            Stick stick =       new Stick(5, 0, 2, 1, 10);   // 1 кг
            Skates skates =    new Skates(3, 2, 3, 3, 0.10f); // 3 кг
            Gloves gloves =    new Gloves(2, 1, 2, 1, 8); // 1 кг

            List<GearItem> items = new List<GearItem> { Uniform, stick, skates, gloves };
            Equipment equipment = new Equipment("Полный комплект", items);

            Console.WriteLine($"Вес каждого предмета:");
            Console.WriteLine($"  Форма: {Uniform.Weight} кг");
            Console.WriteLine($"  Клюшка: {stick.Weight} кг");
            Console.WriteLine($"  Коньки: {skates.Weight} кг");
            Console.WriteLine($"  Перчатки: {gloves.Weight} кг");
            Console.WriteLine($"\nОбщий вес: {equipment.TotalWeight} кг (ожидается: 7)");

            Single weightPenalty = equipment.TotalWeight * 0.01f;
            Console.WriteLine($"\nШтраф от веса: {equipment.TotalWeight} * 0.01 = {weightPenalty:F2}");
            Console.WriteLine($"Итоговый базовый расход: 1 + {weightPenalty:F2} = {1 + weightPenalty:F2} ед/тик");

            Boolean isValid = equipment.TotalWeight == 7;
            Console.WriteLine($"\nРезультат: {(isValid ? "ПРОЙДЕН" : "ПРОВАЛЕН")}\n");
        }

        static void TestPlayerWithEquipment()
        {
            Console.WriteLine("ТЕСТ 4: Игрок с экипировкой");
 
            Uniform Uniform = new Uniform(2, 3, 5, 2);
            Stick stick = new Stick(5, 0, 2, 1, 10);
            Skates skates = new Skates(3, 2, 3, 3, 0.10f);
            Gloves gloves = new Gloves(2, 1, 2, 1, 8);

            List<GearItem> items = new List<GearItem> { Uniform, stick, skates, gloves };
            Equipment equipment = new Equipment("Полный комплект", items);

            Forward forward = new Forward(1, "Нападающий Тест", 75, 45, 60, equipment);

            Console.WriteLine($"Нападающий: {forward.Name}");
            Console.WriteLine($"  Базовая ловкость: {forward.Agility}");
            Console.WriteLine($"  Бонус к ловкости от экипировки: {equipment.TotalAgilityBonus}");
            Console.WriteLine($"  Специальный бонус к атаке (клюшка): {equipment.TotalAttackBonus}");

            Int32 expectedAttack = 75 + equipment.TotalAgilityBonus + equipment.TotalAttackBonus;
            Console.WriteLine($"  Итоговый шанс атаки: {forward.GetAttackChance()}% (ожидается: {expectedAttack})");

            Console.WriteLine($"\n  Базовая сила: {forward.Strength}");
            Console.WriteLine($"  Бонус к силе от экипировки: {equipment.TotalStrengthBonus}");
            Console.WriteLine($"  Специальный бонус к защите (перчатки): {equipment.TotalDefenseBonus}");

            Int32 expectedDefense = 45 + equipment.TotalStrengthBonus + equipment.TotalDefenseBonus;
            Console.WriteLine($"  Итоговый шанс защиты: {forward.GetDefenseChance()}% (ожидается: {expectedDefense})");

            Boolean isValid = forward.GetAttackChance() == expectedAttack &&
                             forward.GetDefenseChance() == expectedDefense;
            Console.WriteLine($"\nРезультат: {(isValid ? "ПРОЙДЕН" : "ПРОВАЛЕН")}\n");
        }

        static Team TestTeamCreation()
        {
            Console.WriteLine("ТЕСТ 5: Создание команды (22 игрока)");
           
            Uniform Uniform = new Uniform(2, 3, 5, 2);
            Stick stick = new Stick(5, 0, 2, 1, 10);
            Skates skates = new Skates(3, 2, 3, 3, 0.10f);
            Gloves gloves = new Gloves(2, 1, 2, 1, 8);

            List<GearItem> items = new List<GearItem> { Uniform, stick, skates, gloves };
            Equipment equipment = new Equipment("Комплект", items);

            Goalie goalie1 = new Goalie(1, "Вратарь 1", 50, 60, 70, 75, equipment);
            Goalie goalie2 = new Goalie(2, "Вратарь 2", 55, 65, 75, 70, equipment);

            List<Defender> defenders = new List<Defender>();
            for (Int32 i = 0; i < 8; i++)
            {
                defenders.Add(new Defender(10 + i, $"Защитник {i + 1}", 40, 70, 65, equipment));
            }

            List<Forward> forwards = new List<Forward>();
            for (Int32 i = 0; i < 12; i++)
            {
                forwards.Add(new Forward(100 + i, $"Нападающий {i + 1}", 75, 45, 60, equipment));
            }

            Team team = new Team("Медведи", goalie1, goalie2, defenders, forwards);

            Int32 totalPlayers = 2 + team.AllDefenders.Count + team.AllForwards.Count;

            Console.WriteLine($"Команда: {team.Name}");
            Console.WriteLine($"Вратарей: 2 (StartingGoalie + BackupGoalie)");
            Console.WriteLine($"Защитников: {team.AllDefenders.Count} (ожидается: 8)");
            Console.WriteLine($"Нападающих: {team.AllForwards.Count} (ожидается: 12)");
            Console.WriteLine($"Всего игроков: {totalPlayers} (ожидается: 22)");
            Console.WriteLine($"\nТекущий вратарь: {team.CurrentGoalie.Name}");
            Console.WriteLine($"В звене защитников: {team.CurrentLine.Defenders.Count} (ожидается: 2)");
            Console.WriteLine($"В звене нападающих: {team.CurrentLine.Forwards.Count} (ожидается: 3)");
            Console.WriteLine($"На скамейке: {team.Bench.Count} игроков (ожидается: 15)");

            Boolean isValid = team.AllDefenders.Count == 8 &&
                             team.AllForwards.Count == 12 &&
                             totalPlayers == 22 &&
                             team.CurrentLine.Defenders.Count == 2 &&
                             team.CurrentLine.Forwards.Count == 3 &&
                             team.Bench.Count == 15;
            Console.WriteLine($"\nРезультат: {(isValid ? "✓ ПРОЙДЕН" : "✗ ПРОВАЛЕН")}\n");

            return team;
        }

        static void TestGoalieSwitch(Team team)
        {
            Console.WriteLine("ТЕСТ 6: Смена вратаря");
            
            String firstGoalie = team.CurrentGoalie.Name;
            Console.WriteLine($"До смены: {firstGoalie}");

            team.SwitchGoalie();
            String secondGoalie = team.CurrentGoalie.Name;
            Console.WriteLine($"После 1-й смены: {secondGoalie}");

            team.SwitchGoalie();
            String thirdGoalie = team.CurrentGoalie.Name;
            Console.WriteLine($"После 2-й смены: {thirdGoalie}");

            Boolean isValid = firstGoalie != secondGoalie && firstGoalie == thirdGoalie;
            Console.WriteLine($"\nРезультат: {(isValid ? "✓ ПРОЙДЕН" : "✗ ПРОВАЛЕН")}\n");
        }

        static void TestPlayerSubstitution(Team team)
        {
            Console.WriteLine("ТЕСТ 7: Замена игрока в звене");
           

            Defender oldDefender = team.CurrentLine.Defenders[0];
            Defender newDefender = team.GetAvailableDefenders()[0];

            Console.WriteLine($"Заменяем: {oldDefender.Name} → {newDefender.Name}");
            team.ReplaceDefenderInLine(oldDefender, newDefender);

            Console.WriteLine($"Теперь в звене: {team.CurrentLine.Defenders[0].Name}");
            Console.WriteLine($"На скамейке: {team.Bench.Count} игроков");

            Boolean isValid = team.CurrentLine.Defenders[0].Name == newDefender.Name;
            Console.WriteLine($"\nРезультат: {(isValid ? "✓ ПРОЙДЕН" : "✗ ПРОВАЛЕН")}\n");
        }

        static void TestFullLineChange(Team team)
        {
            Console.WriteLine("ТЕСТ 8: Полная смена звена");
           
            Console.WriteLine("До замены:");
            List<String> beforeDefenders = new List<String>();
            foreach (Defender d in team.CurrentLine.Defenders)
            {
                Console.WriteLine($"  Защитник: {d.Name}");
                beforeDefenders.Add(d.Name);
            }

            team.FullLineChange();

            Console.WriteLine("\nПосле замены:");
            List<String> afterDefenders = new List<String>();
            foreach (Defender d in team.CurrentLine.Defenders)
            {
                Console.WriteLine($"  Защитник: {d.Name}");
                afterDefenders.Add(d.Name);
            }

            Boolean isValid = !beforeDefenders.SequenceEqual(afterDefenders);
            Console.WriteLine($"\nРезультат: {(isValid ? "✓ ПРОЙДЕН" : "✗ ПРОВАЛЕН")}\n");
        }

        static void TestAttackLine(Team team)
        {
            Console.WriteLine("ТЕСТ 9: Звено атаки (2З + 3Н)");
           
            team.SetAttackLine();

            Console.WriteLine("Звено атаки:");
            foreach (Defender d in team.CurrentLine.Defenders)
            {
                Console.WriteLine($"  Защитник: {d.Name}");
            }
            foreach (Forward f in team.CurrentLine.Forwards)
            {
                Console.WriteLine($"  Нападающий: {f.Name}");
            }

            Boolean isValid = team.CurrentLine.Defenders.Count == 2 &&
                             team.CurrentLine.Forwards.Count == 3;
            Console.WriteLine($"\nРезультат: {(isValid ? "ПРОЙДЕН" : "ПРОВАЛЕН")}\n");
        }

        static void TestEnergy(Team team)
        {
            Console.WriteLine("ТЕСТ 10: Энергия (вес + коньки)");
           
            Player testPlayer = team.CurrentLine.Forwards[0];
            Single initialEnergy = testPlayer.Energy;
            Int32 totalWeight = testPlayer.Gear.TotalWeight;
            Single staminaEfficiency = testPlayer.Gear.TotalStaminaEfficiency;

            Single baseDrain = 1.0f + (totalWeight * 0.01f);
            Single actualDrainPerTick = baseDrain * (1.0f - staminaEfficiency);

            Console.WriteLine($"Игрок: {testPlayer.Name}");
            Console.WriteLine($"Общий вес экипировки: {totalWeight} кг");
            Console.WriteLine($"Снижение расхода от коньков: {staminaEfficiency * 100}%");
            Console.WriteLine($"Базовый расход: 1 + ({totalWeight} * 0.01) = {baseDrain:F3}");
            Console.WriteLine($"Итоговый расход: {baseDrain:F3} * (1 - {staminaEfficiency}) = {actualDrainPerTick:F3}");
            Console.WriteLine($"Энергия до: {initialEnergy:F1}/{testPlayer.MaxEnergy:F1}");

            testPlayer.SetOnIce(true);
            for (Int32 i = 0; i < 10; i++)
            {
                testPlayer.TickEnergy();
            }
            Single afterIceEnergy = testPlayer.Energy;
            Single expectedDrain = 10 * actualDrainPerTick;
            Single actualDrain = initialEnergy - afterIceEnergy;

            Console.WriteLine($"\nПосле 10 секунд на льду:");
            Console.WriteLine($"  Энергия: {testPlayer.Energy:F1}/{testPlayer.MaxEnergy:F1}");
            Console.WriteLine($"  Потеряно: {actualDrain:F2} (ожидается: {expectedDrain:F2})");

            testPlayer.SetOnIce(false);
            for (Int32 i = 0; i < 10; i++)
            {
                testPlayer.TickEnergy();
            }
            Console.WriteLine($"\nПосле 10 секунд на скамейке:");
            Console.WriteLine($"  Энергия: {testPlayer.Energy:F1}/{testPlayer.MaxEnergy:F1}");
            Console.WriteLine($"  Восстановлено (1% от MaxEnergy в сек): {(testPlayer.MaxEnergy * 0.01f * 10):F1}");

            // Тест перерыва
            team.AddEnergyToAll(40);
            Console.WriteLine($"\nПосле перерыва (+40):");
            Console.WriteLine($"  Энергия: {testPlayer.Energy:F1}/{testPlayer.MaxEnergy:F1}");

            Boolean isValid = Math.Abs(actualDrain - expectedDrain) < 0.5;
            Console.WriteLine($"\nРезультат: {(isValid ? "ПРОЙДЕН" : "ПРОВАЛЕН")}\n");
        }

        static void TestTimeManager()
        {
            Console.WriteLine("ТЕСТ 11: TimeManager");
            
            TimeManager time = new TimeManager();
            Int32 periodEndedCount = 0;
            Int32 breakStartedCount = 0;
            Int32 breakEndedCount = 0;

            time.PeriodEnded += () =>
            {
                periodEndedCount++;
                Console.WriteLine($"[СОБЫТИЕ] Конец периода {time.CurrentPeriod}");
            };
            time.BreakStarted += () =>
            {
                breakStartedCount++;
                Console.WriteLine("[СОБЫТИЕ] Начало перерыва");
            };
            time.BreakEnded += () =>
            {
                breakEndedCount++;
                Console.WriteLine($"[СОБЫТИЕ] Начало периода {time.CurrentPeriod}");
            };

            Console.WriteLine("Прокручиваем время до конца 1-го периода (1200 тиков)...");
            while (time.CurrentPeriod == 1 && !time.IsBreak)
            {
                time.Tick();
            }

            Console.WriteLine($"\nТекущее состояние: {time.GetFormattedTime()}");
            Console.WriteLine($"IsBreak: {time.IsBreak}");

            Console.WriteLine("\nПрокручиваем перерыв (60 тиков)...");
            while (time.IsBreak)
            {
                time.Tick();
            }

            Console.WriteLine($"\nТекущее состояние: {time.GetFormattedTime()}");
            Console.WriteLine($"IsBreak: {time.IsBreak}");

            Boolean isValid = periodEndedCount == 1 &&
                             breakStartedCount == 1 &&
                             breakEndedCount == 1 &&
                             time.CurrentPeriod == 2;
            Console.WriteLine($"\nРезультат: {(isValid ? "ПРОЙДЕН" : "ПРОВАЛЕН")}\n");
        }
    }
}