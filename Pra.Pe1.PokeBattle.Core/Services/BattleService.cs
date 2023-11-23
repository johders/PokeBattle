using Pra.Pe1.PokeBattle.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pra.Pe1.PokeBattle.Core.Services
{
    public class BattleService
    {
        public List<string> PokemonNames { get; private set; } = new List<string> { "charmander", "pikachu", "bulbasaur", "squirtle" };
        public List<Pokemon> PlayerPokemon { get; }
        public List<Pokemon> ComputerPokemon { get; }
        public List<BagItem> BagItems { get; }

        private readonly Random random = new Random();

        public BattleService() 
        {
            PlayerPokemon = new List<Pokemon>();
            GenerateRandomPlayerPokemon();
            ComputerPokemon = new List<Pokemon>();
            GenerateRandomComputerPokemon();
        }

        #region GeneratePokemon

        void GenerateRandomPlayerPokemon()
        {           
            for (int i = 0; i < 3; i++)
            {
                int randomNumber = random.Next(PokemonNames.Count);
                string randomName = ChangeFirstLetterToUpper(PokemonNames[randomNumber]);
                Pokemon newPokemon = new Pokemon(randomName);
                PlayerPokemon.Add(newPokemon);
            }          
        }

        void GenerateRandomComputerPokemon()
        {           
            for (int i = 0; i < 3; i++)
            {
                int randomNumber = random.Next(PokemonNames.Count);
                string randomName = ChangeFirstLetterToUpper(PokemonNames[randomNumber]);
                Pokemon newPokemon = new Pokemon(randomName);
                ComputerPokemon.Add(newPokemon);
                PokemonNames.RemoveAt(randomNumber);
            }
        }
        string ChangeFirstLetterToUpper(string name)
        {
            return string.Concat(name[0].ToString().ToUpper(), name.AsSpan(1));
        }

        #endregion



    }
}
