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
        public List<string> PlayerPokemonNames { get; private set; } = new List<string> { "charmander", "pikachu", "bulbasaur", "squirtle" };
        public List<string> ComputerPokemonNames { get; private set; } = new List<string> { "charmander", "pikachu", "bulbasaur", "squirtle" };
        public List<Pokemon> PlayerPokemons { get; }
        public List<Pokemon> ComputerPokemons { get; }
        public List<BagItem> BagItems { get; set; }
        public List<int> Damage { get; set; } = new List<int>();

        private int totalDamage; //Computed property added to meet assignment requirements, does not have any other function in program

        public int TotalDamage
        {
            get 
            {               
                return Damage.Sum(); 
            }           
        }

        public int PlayerPokemonIndex { get; private set; }
        public int ComputerPokemonIndex { get; private set; }

        private readonly Random random = new Random();

        public BattleService()
        {
            PlayerPokemons = new List<Pokemon>();
            GenerateRandomPlayerPokemons();
            ComputerPokemons = new List<Pokemon>();
            GenerateRandomComputerPokemons();
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

        void GenerateRandomPlayerPokemons()
        {
            for (int i = 0; i < 3; i++)
            {
                int randomNumber = random.Next(0, PlayerPokemonNames.Count);
                string randomName = PlayerPokemonNames[randomNumber];
                Pokemon newPokemon = new Pokemon(ChangeFirstLetterToUpper(randomName));
                PlayerPokemons.Add(newPokemon);
                PlayerPokemonNames.RemoveAt(randomNumber);
            }
        }

        void GenerateRandomComputerPokemons()
        {
            for (int i = 0; i < 3; i++)
            {
                int randomNumber = random.Next(0, ComputerPokemonNames.Count);
                string randomName = ComputerPokemonNames[randomNumber];
                Pokemon newPokemon = new Pokemon(ChangeFirstLetterToUpper(randomName));
                ComputerPokemons.Add(newPokemon);
                ComputerPokemonNames.RemoveAt(randomNumber);
            }
        }

        string ChangeFirstLetterToUpper(string name)
        {
            return string.Concat(name[0].ToString().ToUpper(), name.AsSpan(1));
        }

        #endregion

        #region Game Logic

        public void Attack(Pokemon pokemonUnderAttack)
        {
            int damage;
            damage = random.Next(10, 51);
            Damage.Add(damage);
            pokemonUnderAttack.TakeDamage(damage);
        }

        public void GiveBagItem(Pokemon pokemon, BagItem selectedItem)
        {
            int healthIncrease = selectedItem.HealthPoints;
            pokemon.HealthIncrease(healthIncrease);
        }

        private bool CheckIfPokemonIsDead(Pokemon pokemon)
        {
            if (pokemon.Health <= 0)
                return true;

            else
                return false;
        }

        public void LevelUp(Pokemon pokemon, Pokemon opposingPokemon)
        {
            if (pokemon.Health >= 100)
            {
                pokemon.IncreaseLevelAndAdjustHealth();
                return;
            }

            if (CheckIfPokemonIsDead(opposingPokemon) == true)
            {
                pokemon.IncreaseLevel();
            }
        }

        public void ChangePokemon()
        {
            if (PlayerPokemonIndex < PlayerPokemons.Count - 1)
            {
                PlayerPokemonIndex++;
            }
            else PlayerPokemonIndex = 0;
        }

        public void RemovePlayerPokemon(int index)
        {
            PlayerPokemons.RemoveAt(index);
        }

        public void RemoveComputerPokemon(int index)
        {
            ComputerPokemons.RemoveAt(index);
        }

        public void ResetPlayerIndex()
        {
            PlayerPokemonIndex = 0;
        }

        #endregion
    }
}
