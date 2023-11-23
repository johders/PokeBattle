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
        public List<BagItem> BagItems { get; set; }

        public List<int> Damage { get; set; } = new List<int>();

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
            Damage.Add(damage);
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

        public void PlayerAttack(Pokemon player, Pokemon computer/* int PlayerIndex, int computerIndex*/)
        {
            Attack(computer);
            LevelUp(player, computer);

            if (computer.Health <= 0)
            {
                computer.Health = 0;
                ComputerPokemon.Remove(computer);
            } 

        }

      
    //    service.Attack(service.ComputerPokemon[computerPokemonIndex]);
    //        service.LevelUp(service.PlayerPokemon[playerPokemonIndex], service.ComputerPokemon[computerPokemonIndex]);

    //        if (service.ComputerPokemon[computerPokemonIndex].Health <= 0)
    //        {
    //            service.ComputerPokemon[computerPokemonIndex].Health = 0;
    //            DisplayComputerPokemonStats(computerPokemonIndex);
    //    tbkFeedback.Text = $"...Computer's {service.ComputerPokemon[computerPokemonIndex].Name} died... ";
    //            await Task.Delay(1000);
    //    service.ComputerPokemon.RemoveAt(computerPokemonIndex);

    //            if (service.ComputerPokemon.Count == 0) 
                
    //            {
    //                tbkFeedback.Text = $"...You murdered all computer's Pokemon you win...";
    //                GetComputerImage(null);
    //                return;
    //            }

    //tbkFeedback.Text = $"...Computer switched to: {service.ComputerPokemon[computerPokemonIndex].Name}... ";
    //            GetComputerImage(service.ComputerPokemon[playerPokemonIndex].Name);
    //DisplayComputerPokemonStats(computerPokemonIndex);
    //await Task.Delay(1000);


//DisplayComputerPokemonStats(computerPokemonIndex);
//DisplayPlayerPokemonStats(0);


public void SelectBagItem(Pokemon pokemon, int index)
        {
            int healthIncrease = BagItems[index].HealthPoints;

            pokemon.Health += healthIncrease;

        }

    }
}
