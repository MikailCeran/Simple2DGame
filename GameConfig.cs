using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace ADVC
{
    // GameConfig class to represent the game configuration.
    public class GameConfig
    {
        // Properties to store the maximum coordinates of the game world.
        public int WorldMaxX { get; set; }
        public int WorldMaxY { get; set; }

        // List to store the configurations of creatures in the game.
        public List<CreatureConfig> Creatures { get; set; }

        // List to store the configurations of items in chests.
        public List<ItemConfig> ChestItems { get; set; }

        // Method to load the game configuration from a JSON file.
        public static GameConfig Load(string path)
        {
            try
            {
                var configText = File.ReadAllText(path);
                var config = JsonConvert.DeserializeObject<GameConfig>(configText);
                Console.WriteLine("Configuration loaded successfully.");
                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load configuration: " + ex.Message);
                return null; // or handle the error as appropriate
            }
        }
    }

    // CreatureConfig class to represent the configuration of a creature.
    public class CreatureConfig
    {
        // Property to store the name of the creature.
        public string Name { get; set; }

        // Property to store the maximum health of the creature.
        public int MaxHealth { get; set; }

        // Properties to store the starting coordinates of the creature.
        public int StartingX { get; set; }
        public int StartingY { get; set; }
    }

    // ItemConfig class to represent the configuration of an item.
    public class ItemConfig
    {
        // Property to store the type of the item.
        public string Type { get; set; }

        // Property to store the name of the item.
        public string Name { get; set; }

        // Property to store the quantity of the item.
        public int Quantity { get; set; }
    }
}
