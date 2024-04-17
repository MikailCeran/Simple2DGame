using ADVC;
using System;

namespace ADVC
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create two creatures for the battle
            Creature creature1 = new Creature("Leonard Caruso", 150, 150, 1, 1);
            Creature creature2 = new Creature("Vincent Moretti", 150, 150, 1, 2);

            // Equip creatures with weapons
            AttackItem sword = GameObjectFactory.CreateAttackItem("Sword", 20, 2);
            DefenceItem shield = GameObjectFactory.CreateDefenceItem("Shield", 10);

            creature1.AttackItems.Add(sword);
            creature2.DefenceItems.Add(shield);

            // Start the battle
            StartBattle(creature1, creature2);

            Console.ReadLine();
        }

        static void StartBattle(Creature creature1, Creature creature2)
        {
            Console.WriteLine($"A battle begins between {creature1.Name} and {creature2.Name}!");

            while (creature1.HitPoint > 0 && creature2.HitPoint > 0)
            {
                // Creature1 attacks Creature2
                creature1.Hit(creature2);

                // Check if Creature2 is still alive
                if (creature2.HitPoint > 0)
                {
                    // Creature2 attacks Creature1
                    creature2.Hit(creature1);
                }
            }

            // Determine the winner
            if (creature1.HitPoint <= 0)
            {
                Console.WriteLine($"{creature2.Name} wins the battle!");
            }
            else
            {
                Console.WriteLine($"{creature1.Name} wins the battle!");
            }
        }
    }
}
