using System;
using System.Collections.Generic;
using Хоккеи.Classes.equipment;
using Хоккеи.Classes.equipment.TypesOfEquip;
using Хоккеи.Classes.Managers;
using Хоккеи.Classes.Match;
using Хоккеи.Classes.Players;
using Хоккеи.Classes.Teams;

namespace Хоккеи
{
    class Program
    {
        static void Main(String[] args)
        {
            Console.WriteLine("   СИМУЛЯТОР ХОККЕЙНОГО МАТЧА      ");


            Equipment equipment = CreateDefaultEquipment();
            int stam = 300;
            Team team1 = CreateTeam("Медведи", equipment,
                goalieSaveChance1: 75, goalieSaveChance2: 70,
                defenderAgility: 40, defenderStrength: 70, defenderStamina: stam,
                forwardAgility: 75, forwardStrength: 45, forwardStamina: stam);

            Team team2 = CreateTeam("Волки", equipment,
                goalieSaveChance1: 75, goalieSaveChance2: 70,
                defenderAgility: 45, defenderStrength: 75, defenderStamina: stam,
                forwardAgility: 70, forwardStrength: 50, forwardStamina: stam);

            Match match = new Match(team1, team2);

            Console.WriteLine($"{team1.Name} vs {team2.Name}");
            Console.WriteLine($"Вратари: {team1.CurrentGoalie.Name} vs {team2.CurrentGoalie.Name}");
            

            while (!match.IsMatchOver)
            {
                match.SimulateTick();
            }

            
        }

        static Equipment CreateDefaultEquipment()
        {
            Uniform Uniform = new Uniform(2, 3, 5, 2);
            Stick stick = new Stick(5, 0, 2, 1, 10);
            Skates skates = new Skates(3, 2, 3, 3, 0.10f);
            Gloves gloves = new Gloves(2, 1, 2, 1, 8);

            List<GearItem> items = new List<GearItem> { Uniform, stick, skates, gloves };
            return new Equipment("Стандартный комплект", items);
        }

        static Team CreateTeam(String name, Equipment equipment,
            Int32 goalieSaveChance1, Int32 goalieSaveChance2,
            Int32 defenderAgility, Int32 defenderStrength, Int32 defenderStamina,
            Int32 forwardAgility, Int32 forwardStrength, Int32 forwardStamina)
        {
            Goalie goalie1 = new Goalie(1, $"Вратарь {name} 1", 50, 60, 70, goalieSaveChance1, equipment);
            Goalie goalie2 = new Goalie(2, $"Вратарь {name} 2", 55, 65, 75, goalieSaveChance2, equipment);

            List<Defender> defenders = new List<Defender>();
            for (Int32 i = 0; i < 8; i++)
            {
                defenders.Add(new Defender(10 + i, $"Защитник {name} {i + 1}", defenderAgility, defenderStrength, defenderStamina, equipment));
            }

            List<Forward> forwards = new List<Forward>();
            for (Int32 i = 0; i < 12; i++)
            {
                forwards.Add(new Forward(100 + i, $"Нападающий {name} {i + 1}", forwardAgility, forwardStrength, forwardStamina, equipment));
            }

            return new Team(name, goalie1, goalie2, defenders, forwards);
        }
    }
}