﻿using Pra.Pe1.PokeBattle.Core.Entities;
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
        public List<BagItem> BagItems { get; set; }

        private readonly Random random = new Random();

        public BattleService()
        {
            PlayerPokemon = new List<Pokemon>();
            GenerateRandomPlayerPokemon();
            ComputerPokemon = new List<Pokemon>();
            GenerateRandomComputerPokemon();
            LoadBagItems();
        }

        void LoadBagItems()
        {
            BagItems = new List<BagItem>();

            BagItems.Add(new BagItem("Croissant", 25));
            BagItems.Add(new BagItem("Chocoladekoek", 50));
            BagItems.Add(new BagItem("Eclair", 75));
            BagItems.Add(new BagItem("Boule de Berlin", 100));

        }

        #region GeneratePokemon

        void GenerateRandomPlayerPokemon()
        {
            for (int i = 0; i < 3; i++)
            {
                int randomNumber = random.Next(PokemonNames.Count);
                string randomName = PokemonNames[randomNumber];
                Pokemon newPokemon = new Pokemon(randomName);
                PlayerPokemon.Add(newPokemon);
            }
        }

        void GenerateRandomComputerPokemon()
        {
            for (int i = 0; i < 3; i++)
            {
                int randomNumber = random.Next(PokemonNames.Count);
                string randomName = PokemonNames[randomNumber];
                Pokemon newPokemon = new Pokemon(randomName);
                ComputerPokemon.Add(newPokemon);
                PokemonNames.RemoveAt(randomNumber);
            }
        }


        #endregion

        public void Attack(Pokemon pokemonUnderAttack)
        {
            int damage;
            damage = random.Next(10, 51);
            int healthAfterAttack = pokemonUnderAttack.Health - damage;

            pokemonUnderAttack.Health = healthAfterAttack;
        }

        public void LevelUp(Pokemon pokemon, Pokemon opposingPokemon)
        {
            if ((pokemon.Health >= 100) || (PokemonDead(opposingPokemon) == true))
            {
                pokemon.Level++;
                pokemon.Health = 1;
            }
        }

        private bool PokemonDead(Pokemon pokemon)
        {
            if (pokemon.Health <= 0)
                return true;

            else
                return false;
        }

      
        public void SelectBagItem(Pokemon pokemon, int index)
        {
            int healthIncrease = BagItems[index].HealthPoints;

            pokemon.Health += healthIncrease;

        }

    }
}
