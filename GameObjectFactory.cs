using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ADVC
{
    // Introducing a Factory class to create game objects.
    public static class GameObjectFactory
    {
        // Method to create a creature.
        public static Creature CreateCreature(string name, int hitPoint, int maxHitPoints, int x, int y)
        {
            Logger.Log($"Creating creature: {name}");
            return new Creature(name, hitPoint, maxHitPoints, x, y);
        }

        // Method to create an attack item.
        public static AttackItem CreateAttackItem(string name, int hitPoint, int range)
        {
            Logger.Log($"Creating attack item: {name}");
            return new AttackItem(name, hitPoint, range);
        }

        // Method to create a defence item.
        public static DefenceItem CreateDefenceItem(string name, int reduceHitPoint)
        {
            Logger.Log($"Creating defence item: {name}");
            return new DefenceItem(name, reduceHitPoint);
        }

        // Method to create a chest object.
        public static Chest CreateChest()
        {
            Chest chest = new Chest();
            // Assume Chest constructor handles randomizing contents
            return chest;
        }

        // Method to create a Consumable item.
        public static Consumable CreateConsumable(string name, int healthRestore)
        {
            Logger.Log($"Creating Consumable");
            return new Consumable(name, healthRestore);
        }
    }
}
