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
        BagItemsListWindow newWindow;

        public Pokemon ComputerPokemon;
        public Pokemon PlayerPokemon;
       
        public MainWindow()
        {
            InitializeComponent();
            service = new BattleService();
            newWindow = new BagItemsListWindow();
        }       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshPokemonList();
            GetComputerImage(ComputerPokemon.Name);
            GetPlayerImage(PlayerPokemon.Name);
            DisplayComputerPokemonStats();
            DisplayPlayerPokemonStats();
            LoadBagItemsListBox();
        }

        private async void BtnFight_Click(object sender, RoutedEventArgs e)
        {
            grpButtons.IsEnabled = false;

            tbkFeedback.Text = "...Player is attacking computer... ";

            service.Attack(ComputerPokemon);
            service.LevelUp(PlayerPokemon, ComputerPokemon);
            await Task.Delay(2000);
            tbkFeedback.Text = $"...{PlayerPokemon.Name} damaged {ComputerPokemon.Name} with {service.Damage[service.Damage.Count-1]}... ";

            if (ComputerPokemon.Health <= 0) 
            {
                ComputerPokemon.Health = 0;
                DisplayComputerPokemonStats();
                await Task.Delay(2000);
                tbkFeedback.Text = $"...Computer's {ComputerPokemon.Name} died - switching... ";
                await Task.Delay(2000);
                service.ComputerPokemons.RemoveAt(service.ComputerPokemonIndex);

                if (service.ComputerPokemons.Count == 0)
                {
                    tbkFeedback.Text = $"...All computer's Pokemon died. You win...";
                    GetComputerImage(null);
                    await Task.Delay(2000);
                    return;
                }

                RefreshPokemonList();
                tbkFeedback.Text = $"...Changed to: {ComputerPokemon.Name}... ";
                GetComputerImage(ComputerPokemon.Name);
                DisplayComputerPokemonStats();             
            }

            DisplayComputerPokemonStats();
            DisplayPlayerPokemonStats();
            await Task.Delay(2000);

            tbkFeedback.Text = "...Computer is attacking player... ";
            await Task.Delay(2000);

            service.Attack(PlayerPokemon);          
            tbkFeedback.Text = $"...{ComputerPokemon.Name} damaged {PlayerPokemon.Name} with {service.Damage[service.Damage.Count - 1]}... ";
            service.LevelUp(ComputerPokemon, PlayerPokemon);
            
            if (PlayerPokemon.Health <= 0)
            {
                PlayerPokemon.Health = 0;
                DisplayPlayerPokemonStats();
                await Task.Delay(2000);
                tbkFeedback.Text = $"...Your {PlayerPokemon.Name} died - switching ";
                await Task.Delay(2000);
                service.PlayerPokemons.RemoveAt(service.PlayerPokemonIndex);

                if (service.PlayerPokemons.Count == 0)
                {
                    tbkFeedback.Text = $"...All your Pokemon died, You lose...";
                    await Task.Delay(3000);
                    GetPlayerImage(null);
                    return;
                }

                RefreshPokemonList();
                tbkFeedback.Text = $"...Changed to: {PlayerPokemon.Name}... ";
                GetPlayerImage(PlayerPokemon.Name);
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
            GetPlayerImage(PlayerPokemon.Name);
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnBag_Click(object sender, RoutedEventArgs e)
        {
            newWindow.Show();
        }

        void RefreshPokemonList()
        {
            ComputerPokemon = service.ComputerPokemons[service.ComputerPokemonIndex];
            PlayerPokemon = service.PlayerPokemons[service.PlayerPokemonIndex];
        }

        public void DisplayPlayerPokemonStats()
        {
            lblPlayerPokemon.Content = string.Empty;
            lblPlayerPokemon.Content = PlayerPokemon;
            pgbPlayerHealth.Value = PlayerPokemon.Health;
        }

        void DisplayComputerPokemonStats()
        {
            lblComputerPokemon.Content = string.Empty;
            lblComputerPokemon.Content = ComputerPokemon;
            pgbComputerHealth.Value = ComputerPokemon.Health;
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

    }
}
