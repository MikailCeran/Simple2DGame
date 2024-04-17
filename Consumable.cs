using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVC
{
    public class Consumable
    {
        // Property to store the name of the consumable item.
        public string Name { get; private set; }

        // Property to store the health points restored by the consumable item.
        public int HealthRestore { get; private set; }

        // Property to check if the consumable item has been used.
        public bool IsUsed { get; private set; }

        // Constructor to initialize the consumable item with a name and health restore value.
        public Consumable(string name, int healthRestore)
        {
            Name = name;
            HealthRestore = healthRestore;
            IsUsed = false;
        }

        // Method to use the consumable item on a target creature.
        public bool Use(Creature target)
        {
            // Check if the consumable item has not been used and the target is not null.
            if (!IsUsed && target != null)
            {
                // Restore health to the target creature.
                target.RestoreHealth(HealthRestore);
                // Mark the consumable item as used.
                IsUsed = true;
                // Print a message indicating the consumable was used and the health restored.
                Console.WriteLine($"{Name} was used on {target.Name}, restoring {HealthRestore} HP.");
                return true;
            }
            else
            {
                // Print a message indicating the consumable cannot be used again or the target is null.
                Console.WriteLine($"{Name} cannot be used again or target is null.");
                return false;
            }
        }
    }
}
