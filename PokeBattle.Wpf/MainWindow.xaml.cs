using Pra.Pe1.PokeBattle.Core.Entities;
using Pra.Pe1.PokeBattle.Core.Services;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace PokeBattle.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public BattleService service;
        public Pokemon computerPokemon; 
        public Pokemon playerPokemon;

        BagItemsListWindow newWindow;
      
        public MainWindow()
        {
            InitializeComponent();
            service = new BattleService();
            newWindow = new BagItemsListWindow();
        }

        #region Event Handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshPokemonList();
            GetComputerImage(computerPokemon.Name);
            GetPlayerImage(playerPokemon.Name);
            DisplayComputerPokemonStats();
            DisplayPlayerPokemonStats();
            LoadBagItemsListBox();
        }

        private async void BtnFight_Click(object sender, RoutedEventArgs e)
        {
            grpButtons.IsEnabled = false;

            tbkFeedback.Text = "...Player is attacking computer... ";

            service.Attack(computerPokemon);
            service.LevelUp(playerPokemon, computerPokemon);
            await Task.Delay(2000);
            tbkFeedback.Text = $"...{playerPokemon.Name} damaged {computerPokemon.Name} with {service.Damage[service.Damage.Count-1]}... ";

            if (computerPokemon.Health <= 0) 
            {
                DisplayComputerPokemonStats();
                await Task.Delay(1500);
                tbkFeedback.Text = $"...Computer's {computerPokemon.Name} died...";
                await Task.Delay(1500);
                service.RemoveComputerPokemon(service.ComputerPokemonIndex);

                if (service.ComputerPokemons.Count == 0)
                {
                    tbkFeedback.Text = $"...All computer's Pokemon died. You win...";
                    GetComputerImage(null);
                    await Task.Delay(2000);
                    return;
                }

                tbkFeedback.Text = $"...switching...";
                await Task.Delay(1500);
                RefreshPokemonList();
                tbkFeedback.Text = $"...Changed to: {computerPokemon.Name}...";
                GetComputerImage(computerPokemon.Name);
                DisplayComputerPokemonStats();             
            }

            DisplayComputerPokemonStats();
            DisplayPlayerPokemonStats();
            await Task.Delay(1500);

            tbkFeedback.Text = "...Computer is attacking player...";
            await Task.Delay(2000);

            service.Attack(playerPokemon);          
            tbkFeedback.Text = $"...{computerPokemon.Name} damaged {playerPokemon.Name} with {service.Damage[service.Damage.Count - 1]}...";
            service.LevelUp(computerPokemon, playerPokemon);

            if (playerPokemon.Health <= 0)
            {
                DisplayPlayerPokemonStats();
                await Task.Delay(1500);
                tbkFeedback.Text = $"...Your {playerPokemon.Name} died...";
                await Task.Delay(1500);
                service.RemovePlayerPokemon(service.PlayerPokemonIndex);
                service.ResetPlayerIndex();

                if (service.PlayerPokemons.Count == 0)
                {
                    tbkFeedback.Text = $"...All your Pokemon died, You lose...";
                    await Task.Delay(2000);
                    GetPlayerImage(null);
                    return;
                }

                tbkFeedback.Text = $"...switching...";
                await Task.Delay(1500);
                RefreshPokemonList();
                tbkFeedback.Text = $"...Changed to: {playerPokemon.Name}...";
                GetPlayerImage(playerPokemon.Name);
                DisplayComputerPokemonStats();             
            }

            DisplayComputerPokemonStats();
            DisplayPlayerPokemonStats();
            await Task.Delay(3000);

            tbkFeedback.Text = "Do your move... ";
            grpButtons.IsEnabled = true;
        }

        private void BtnChangePokemon_Click(object sender, RoutedEventArgs e)
        {
            service.ChangePokemon();
            RefreshPokemonList();
            DisplayPlayerPokemonStats();
            GetPlayerImage(playerPokemon.Name);
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnBag_Click(object sender, RoutedEventArgs e)
        {
            newWindow.Show();
        }

        #endregion

        #region Methods

        void RefreshPokemonList()
        {
            computerPokemon = service.ComputerPokemons[service.ComputerPokemonIndex];
            playerPokemon = service.PlayerPokemons[service.PlayerPokemonIndex];
        }

        public void DisplayPlayerPokemonStats()
        {
            lblPlayerPokemon.Content = string.Empty;
            lblPlayerPokemon.Content = playerPokemon;
            pgbPlayerHealth.Value = playerPokemon.Health;
        }

        void DisplayComputerPokemonStats()
        {
            lblComputerPokemon.Content = string.Empty;
            lblComputerPokemon.Content = computerPokemon;
            pgbComputerHealth.Value = computerPokemon.Health;
        }

        void GetComputerImage(string name)
        {
            Uri relativeUri = new Uri("../Images/" + name + ".png", UriKind.Relative);
            imgComputer.Source = new BitmapImage(relativeUri);
        }

        void GetPlayerImage(string name)
        {
            Uri relativeUri = new Uri("../Images/" + name + ".png", UriKind.Relative);
            imgPlayer.Source = new BitmapImage(relativeUri);
        }
        void LoadBagItemsListBox()
        {
            newWindow.lstBagItems.Items.Clear();

            foreach (BagItem item in service.BagItems)
            {
                newWindow.lstBagItems.Items.Add(item);
            }
        }
        #endregion

    }
}
