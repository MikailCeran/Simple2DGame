using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVC
{
    // AttackItem class that inherits from WorldObject, used to perform attacks.
    public class AttackItem : WorldObject, IAttack
    {
        // Property to store the hit points of the attack item.
        public int HitPoint { get; private set; }

        // Property to store the range of the attack item.
        public int Range { get; private set; }

        // Constructor to initialize the AttackItem with a name, hit points, and range.
        public AttackItem(string name, int hitPoint, int range)
            : base(name, true, true)
        {
            HitPoint = hitPoint;
            Range = range;
        }

        // Method to perform an attack on a target creature.
        public void Attack(Creature target)
        {
            // Logic for attack through the interface.
            if (target != null)
            {
                // Logging the attack action.
                Logger.Log($"{Name} attacks {target.Name}");
                // Execute the attack on the target creature.
                target.ReceiveHit(HitPoint);
            }
        }
    }
}
