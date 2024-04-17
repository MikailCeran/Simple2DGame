using System;
using System.Collections.Generic;
using System.Linq;

namespace ADVC
{
    public class Chest
    {
        // List to store attack items in the chest.
        public List<AttackItem> AttackItems { get; set; }

        // List to store defence items in the chest.
        public List<DefenceItem> DefenceItems { get; set; }

        // List to store consumable items in the chest.
        public List<Consumable> Consumables { get; set; }

        // Property to determine if the chest is lootable.
        public bool IsLootable { get; set; }

        // Constructor to initialize the chest and its contents.
        public Chest()
        {
            AttackItems = new List<AttackItem>();
            DefenceItems = new List<DefenceItem>();
            Consumables = new List<Consumable>();
            IsLootable = true; // Assuming chests are always lootable by default.
            InitializeContents();
        }

        // Method to initialize the contents of the chest.
        private void InitializeContents()
        {
            Logger.Log("Creating chest object");

            Random rnd = new Random();

            // Randomly decide the count of each type of item (for simplicity, 0-2 of each)
            int attackItemCount = rnd.Next(0, 3);
            int defenceItemCount = rnd.Next(0, 3);
            int consumableCount = rnd.Next(0, 3);

            // Fill attack items
            for (int i = 0; i < attackItemCount; i++)
            {
                AttackItems.Add(GameObjectFactory.CreateAttackItem($"Sword{i + 1}", 10 * (i + 1), 1));
            }

            // Fill defence items
            for (int i = 0; i < defenceItemCount; i++)
            {
                DefenceItems.Add(GameObjectFactory.CreateDefenceItem($"Shield{i + 1}", 5 * (i + 1)));
            }

            // Fill consumable items
            for (int i = 0; i < consumableCount; i++)
            {
                Consumables.Add(GameObjectFactory.CreateConsumable($"Health Potion{i + 1}", 20 * (i + 1)));
            }
        }

        // Method to check if the chest is empty.
        public bool IsEmpty()
        {
            return !AttackItems.Any() && !DefenceItems.Any() && !Consumables.Any();
        }
    }
}
