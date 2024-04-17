using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVC
{
    // WorldObject class that can be interacted with in the game.
    public class WorldObject
    {
        public string Name { get; private set; } // Navn på verdensobjektet
        public bool Lootable { get; private set; } // Angiver om objektet kan plyndres
        public bool Removable { get; private set; } // Angiver om objektet kan fjernes

        protected WorldObject(string name, bool lootable, bool removable)
        {
            Name = name;
            Lootable = lootable;
            Removable = removable;
        }
    }
}
