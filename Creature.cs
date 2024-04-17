using System;
using System.Collections.Generic;
using System.Linq;

namespace ADVC
{
    // Creature class represents the creatures present in the game world.
    public class Creature
    {
        // Property to store the name of the creature.
        public string Name { get; set; }

        // Property to store the current hit points of the creature.
        public int HitPoint { get; set; }

        // Property to store the maximum hit points the creature can have.
        public int MaxHitPoints { get; set; }

        // Properties to store the coordinates of the creature in the world.
        public int X { get; set; }
        public int Y { get; set; }

        // Lists to store the attack items, defence items, and consumables carried by the creature.
        public List<AttackItem> AttackItems { get; set; }
        public List<DefenceItem> DefenceItems { get; set; }
        public List<Consumable> Consumables { get; private set; }

        // Constructor to initialize the creature with a name, hit points, maximum hit points, and coordinates.
        public Creature(string name, int hitPoint, int maxHitPoints, int x, int y)
        {
            Name = name;
            HitPoint = hitPoint;
            MaxHitPoints = maxHitPoints;
            X = x;
            Y = y;
            AttackItems = new List<AttackItem>();
            DefenceItems = new List<DefenceItem>();
            Consumables = new List<Consumable>();
        }

        // Helper function to determine if a target is within the range of the weapon.
        private bool IsInRange(Creature target, AttackItem weapon)
        {
            int distance = Math.Abs(X - target.X) + Math.Abs(Y - target.Y);
            return distance <= weapon.Range;
        }

        // Perform an attack with a selected weapon.
        public void Hit(Creature target)
        {
            // Check if the creature has any equipped weapons.
            if (AttackItems == null || AttackItems.Count == 0)
            {
                UnarmedHit(target);
            }
            else
            {
                // Select the weapon with the highest damage.
                AttackItem weapon = AttackItems.OrderByDescending(w => w.HitPoint).FirstOrDefault();

                if (weapon != null && IsInRange(target, weapon))
                {
                    int damage = weapon.HitPoint;
                    target.ReceiveHit(damage);
                    Console.WriteLine($"{Name} attacked {target.Name} with {weapon.Name} for {damage} damage!");
                }
                else if (weapon != null)
                {
                    Console.WriteLine($"{Name} is too far away from {target.Name} to attack with {weapon.Name}.");
                }
            }
        }

        // Method to handle an unarmed attack.
        private void UnarmedHit(Creature target)
        {
            // Assume a standard damage for unarmed attacks.
            int damage = 1;
            target.ReceiveHit(damage);
            Console.WriteLine($"{Name} attacked {target.Name} unarmed for {damage} damage!");
        }

        // Receive an attack and apply defense.
        public void ReceiveHit(int hitPoints)
        {
            int totalDefense = DefenceItems.Sum(item => item.ReduceHitPoint);
            int damageTaken = Math.Max(hitPoints - totalDefense, 0);

            HitPoint -= damageTaken;
            Console.WriteLine($"{Name} received {damageTaken} damage after defense and now has {HitPoint} hitpoints left.");

            if (HitPoint <= 0)
                Die();
        }

        // Logic for looting an object.
        public void Loot(WorldObject worldObject)
        {
            if (worldObject is AttackItem attackItem && attackItem.Lootable)
            {
                AttackItems.Add(attackItem);
                Console.WriteLine($"{Name} has looted an attack item: {attackItem.Name}.");
            }
            else if (worldObject is DefenceItem defenceItem && defenceItem.Lootable)
            {
                DefenceItems.Add(defenceItem);
                Console.WriteLine($"{Name} has looted a defence item: {defenceItem.Name}.");
            }
            else
            {
                Console.WriteLine($"{Name} cannot loot {worldObject.Name}.");
            }
        }

        // Logic for what happens when the creature dies.
        private void Die()
        {
            Console.WriteLine($"{Name} has died.");
            // Implement logic to remove the creature from the world here.
        }

        // A "template" method that defines the steps in a battle.
        public void PerformAction(Creature target)
        {

            ChooseAction(); // Choose action based on the creature's state and available items.
            Hit(target); // Perform attack or other action.
            Logger.Log($"{Name} has performed an action.");
        }

        // A virtual method that can be overridden in derived classes.
        protected virtual void ChooseAction()
        {
            // Choose action. Can be more complex in derived classes.
        }

        public void LootAllCreature(Creature deadCreature)
        {
            if (deadCreature == null || deadCreature.HitPoint > 0)
            {
                Console.WriteLine("Nothing to loot or the creature is not dead.");
                return;
            }

            AttackItems.AddRange(deadCreature.AttackItems);
            DefenceItems.AddRange(deadCreature.DefenceItems);
            Console.WriteLine($"{Name} has looted all items from {deadCreature.Name}.");
        }

        public void LootSpecificCreature(Creature deadCreature, WorldObject item)
        {
            if (deadCreature == null || deadCreature.HitPoint > 0)
            {
                Console.WriteLine("Nothing to loot or the creature is not dead.");
                return;
            }

            if (item is AttackItem attackItem && deadCreature.AttackItems.Contains(attackItem))
            {
                AttackItems.Add(attackItem);
                deadCreature.AttackItems.Remove(attackItem);
            }
            else if (item is DefenceItem defenceItem && deadCreature.DefenceItems.Contains(defenceItem))
            {
                DefenceItems.Add(defenceItem);
                deadCreature.DefenceItems.Remove(defenceItem);
            }

            Console.WriteLine($"{Name} has looted {item.Name} from {deadCreature.Name}.");
        }

        public void LootAllChest(Chest chest)
        {
            if (chest == null || !chest.IsLootable)
            {
                Console.WriteLine("Nothing to loot or the chest is not accessible.");
                return;
            }

            AttackItems.AddRange(chest.AttackItems);
            DefenceItems.AddRange(chest.DefenceItems);
            Console.WriteLine($"{Name} has looted all items from the chest.");
        }

        public void LootSpecificChest(Chest chest, WorldObject item)
        {
            if (chest == null || !chest.IsLootable)
            {
                Console.WriteLine("Nothing to loot or the chest is not accessible.");
                return;
            }

            if (item is AttackItem attackItem && chest.AttackItems.Contains(attackItem))
            {
                AttackItems.Add(attackItem);
                chest.AttackItems.Remove(attackItem);
            }
            else if (item is DefenceItem defenceItem && chest.DefenceItems.Contains(defenceItem))
            {
                DefenceItems.Add(defenceItem);
                chest.DefenceItems.Remove(defenceItem);
            }

            Console.WriteLine($"{Name} has looted {item.Name} from the chest.");
        }

        public void UseConsumable(Consumable consumable)
        {
            // Check if the consumable is valid and belongs to the creature's inventory
            if (consumable != null && Consumables.Contains(consumable) && !consumable.IsUsed)
            {
                if (consumable.Use(this)) // This will handle the restoration and set IsUsed to true
                {
                    Console.WriteLine($"{Name} has used {consumable.Name} and restored health.");
                    Consumables.Remove(consumable); // Remove the consumable after use
                }
                else
                {
                    Console.WriteLine($"{consumable.Name} cannot be used again or target is null.");
                }
            }
            else
            {
                Console.WriteLine($"Consumable is either not in the inventory or has already been used.");
            }
        }

        // Method to restore health to the creature.
        public void RestoreHealth(int amount)
        {
            HitPoint += amount;
            if (HitPoint > MaxHitPoints)
            {
                HitPoint = MaxHitPoints; // Cap the HitPoint to MaxHitPoints
            }
            Console.WriteLine($"{Name} now has {HitPoint}/{MaxHitPoints} HP.");
        }
        // Add this method to the Creature class
        public void StartBattle(Creature opponent)
        {
            Console.WriteLine($"{Name} starts a battle with {opponent.Name}!");

            while (HitPoint > 0 && opponent.HitPoint > 0)
            {
                // Creature attacks opponent
                Hit(opponent);

                // Check if opponent is still alive
                if (opponent.HitPoint > 0)
                {
                    // Opponent attacks creature
                    opponent.Hit(this);
                }
            }

            // Determine the winner
            if (HitPoint <= 0)
            {
                Console.WriteLine($"{opponent.Name} wins the battle!");
                opponent.RestoreHealth(opponent.MaxHitPoints / 2); // Restore half of the winner's health
            }
            else
            {
                Console.WriteLine($"{Name} wins the battle!");
                RestoreHealth(MaxHitPoints / 2); // Restore half of the creature's health
            }
        }

    }
}
