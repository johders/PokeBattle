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

        public int Health { get; private set; } = 35;
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

        public void TakeDamage(int damage)
        {
           {
                if (damage >= Health)
                {
                    Health = 0;
                }
                else
                {
                    Health -= damage;
                }            
            }
        }

        public void IncreaseLevelAndAdjustHealth() 
        {
           Level++;
           Health -= 100;
        }

        public void IncreaseLevel()
        {
            Level++;
        }

       public void HealthIncrease(int healthIncrease)
        {
            Health += healthIncrease;
        }

        public override string ToString()
        {
            return $"{Name} - HP: {Health} ({Level})";
        }
    }
}
