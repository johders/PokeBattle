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
            if(name.Length <= 2 || String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Pokemon name does not meet requirements, needs to be at least ");
            }
            else
            {
                Name = name;
            }         
        }

        public override string ToString()
        {
            return $"{Name} - HP: {Health} ({Level})";
        }
    }
}
