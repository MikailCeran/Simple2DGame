using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVC
{
    public class World
    {
        public int MaxX { get; set; } // Maksimal x-koordinat for verdenen
        public int MaxY { get; set; } // Maksimal y-koordinat for verdenen
        public List<Creature> Creatures { get; set; } // Liste over skabninger i verdenen
        public List<WorldObject> WorldObjects { get; set; } // Liste over objekter i verdenen
        public List<Chest> Chests { get; set; }  // Liste over kister i verdenen

        public World(int maxX, int maxY)
        {
            MaxX = maxX;
            MaxY = maxY;
            Creatures = new List<Creature>();
            WorldObjects = new List<WorldObject>();
            Chests = new List<Chest>();
        }

        public void AddCreature(Creature creature)
        {
            if (creature.X <= MaxX && creature.Y <= MaxY)
                Creatures.Add(creature); // Tilføj en skabning til verdenen, hvis dens koordinater er gyldige
        }

        public void AddWorldObject(WorldObject worldObject)
        {
            WorldObjects.Add(worldObject); // Tilføj et objekt til verdenen
        }

        public void AddChest(Chest chest)
        {
            if (Chests != null)
                Chests.Add(chest); // Tilføj en kiste til verdenen
        }

        public void Initialize()
        {
            // Use GameObjectFactory to add starting objects and creatures to the world
            AddCreature(GameObjectFactory.CreateCreature("Orc", 100, 100, 5, 5)); // Tilføj en Ork skabning
            AddCreature(GameObjectFactory.CreateCreature("Elf", 80, 80, 5, 5)); // Tilføj en Elf skabning
            AddWorldObject(GameObjectFactory.CreateAttackItem("Sword", 20, 2)); // Tilføj et sværd objekt
            AddWorldObject(GameObjectFactory.CreateDefenceItem("Shield", 5)); // Tilføj et skjold objekt
            AddWorldObject(GameObjectFactory.CreateAttackItem("Fist", 2, 1)); // Tilføj et knytnæve angrebsobjekt

            // Add a Chest with random content to the world
            Chest treasureChest = GameObjectFactory.CreateChest();
            AddChest(treasureChest);
        }
    }
}
