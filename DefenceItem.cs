using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVC
{
    // DefenceItem class that inherits from WorldObject, used to reduce damage.
    public class DefenceItem : WorldObject
    {
        // Property to store the amount of damage reduced by the defence item.
        public int ReduceHitPoint { get; private set; }

        // Constructor to initialize the DefenceItem with a name and reduction value.
        public DefenceItem(string name, int reduceHitPoint)
            : base(name, true, true)
        {
            ReduceHitPoint = reduceHitPoint;
        }
    }
}
