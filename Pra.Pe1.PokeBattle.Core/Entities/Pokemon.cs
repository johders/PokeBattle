using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pra.Pe1.PokeBattle.Core.Entities
{
    public class Pokemon
    {

        public string Name { get; }
        public int Health { get; set; } = 35;
        public int Level { get; set; } = 5;

        public Pokemon(string name) 
        {
            Name = name;
        }


    }
}
