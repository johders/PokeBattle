using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pra.Pe1.PokeBattle.Core.Entities
{
    public class BagItem
    {
        public string Name { get; set; }
        public int HealthPoints { get; set; }

        public BagItem(string name)
        {
           Name = name;
        }
        public BagItem(string name, int healthPoints) : this(name)
        {
          HealthPoints = healthPoints;
        }

        public override string ToString() 
        
        {
            return $"{Name} ({HealthPoints})";
        }

    }
}
