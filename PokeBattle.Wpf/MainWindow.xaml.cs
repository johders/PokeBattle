using Pra.Pe1.PokeBattle.Core.Entities;
using Pra.Pe1.PokeBattle.Core.Services;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace PokeBattle.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BattleService service;
        BagItemsListWindow newWindow;

        int playerPokemonIndex;
        int computerPokemonIndex;


        public MainWindow()
        {
            InitializeComponent();
            service = new BattleService();
            newWindow = new BagItemsListWindow();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetComputerImage(service.ComputerPokemons[playerPokemonIndex].Name);
            GetPlayerImage(service.PlayerPokemons[computerPokemonIndex].Name);
            DisplayPlayerPokemonStats(playerPokemonIndex);
            DisplayComputerPokemonStats(computerPokemonIndex);

            LoadBagItemsListBox();
        }

        private async void BtnFight_Click(object sender, RoutedEventArgs e)
        {
            // Door de groupbox te disablen worden alle onderliggende controls ook gedisabled.
            grpButtons.IsEnabled = false;

            // Geef alle updates door in de feedback textblock
            tbkFeedback.Text = "...Player is attacking computer... ";

            //service.PlayerAttack(service.PlayerPokemon[playerPokemonIndex], service.ComputerPokemon[computerPokemonIndex]);
            service.Attack(service.ComputerPokemons[computerPokemonIndex]);
            service.LevelUp(service.PlayerPokemons[playerPokemonIndex], service.ComputerPokemons[computerPokemonIndex]);
            await Task.Delay(3000);
            tbkFeedback.Text = $"...{service.PlayerPokemons[playerPokemonIndex].Name} damaged {service.ComputerPokemons[computerPokemonIndex].Name} with {service.Damage[service.Damage.Count-1]}... ";


            Pokemon currentComputerPokemon = service.ComputerPokemons[computerPokemonIndex]; // Use variable for readability

            if (service.ComputerPokemons[computerPokemonIndex].Health <= 0) 
            {
                service.ComputerPokemons[computerPokemonIndex].Health = 0;
                DisplayComputerPokemonStats(computerPokemonIndex);
                await Task.Delay(3000);
                tbkFeedback.Text = $"...Computer's {service.ComputerPokemons[computerPokemonIndex].Name} died - switching... ";
                await Task.Delay(3000);
                service.ComputerPokemons.RemoveAt(computerPokemonIndex);

                if (service.ComputerPokemons.Count == 0)

                {
                    tbkFeedback.Text = $"...All computer's Pokemon died. You win...";
                    GetComputerImage(null);
                    await Task.Delay(3000);
                    return;
                }

                tbkFeedback.Text = $"...Changed to: {service.ComputerPokemons[computerPokemonIndex].Name}... ";
                GetComputerImage(service.ComputerPokemons[computerPokemonIndex].Name);
                DisplayComputerPokemonStats(computerPokemonIndex);
               
            }

            DisplayComputerPokemonStats(computerPokemonIndex);
            DisplayPlayerPokemonStats(playerPokemonIndex);
            await Task.Delay(3000);
            // Met await Task.Delay(aantal milliseconden) kan je een pauze inlassen
            // Let op ! Gebruik dit voor je eigen veiligheid enkel in deze methode. 
            //await Task.Delay(3000);

            tbkFeedback.Text = "...Computer is attacking player... ";
            await Task.Delay(3000);

            service.Attack(service.PlayerPokemons[playerPokemonIndex]);          
            tbkFeedback.Text = $"...{service.ComputerPokemons[computerPokemonIndex].Name} damaged {service.PlayerPokemons[playerPokemonIndex].Name} with {service.Damage[service.Damage.Count - 1]}... ";
            service.LevelUp(service.ComputerPokemons[computerPokemonIndex], service.PlayerPokemons[playerPokemonIndex]);
            

            if (service.PlayerPokemons[playerPokemonIndex].Health <= 0)
            {
                service.PlayerPokemons[playerPokemonIndex].Health = 0;
                DisplayPlayerPokemonStats(playerPokemonIndex);
                await Task.Delay(3000);
                tbkFeedback.Text = $"...Your {service.PlayerPokemons[playerPokemonIndex].Name} died - switching ";
                await Task.Delay(3000);
                service.PlayerPokemons.RemoveAt(playerPokemonIndex);

                if (service.PlayerPokemons.Count == 0)

                {
                    tbkFeedback.Text = $"...All your Pokemon died, You lose...";
                    await Task.Delay(3000);
                    GetPlayerImage(null);
                    return;
                }

                tbkFeedback.Text = $"...Changed to: {service.PlayerPokemons[playerPokemonIndex].Name}... ";
                GetPlayerImage(service.PlayerPokemons[playerPokemonIndex].Name);
                DisplayComputerPokemonStats(playerPokemonIndex);
               
            }

            DisplayComputerPokemonStats(computerPokemonIndex);
            DisplayPlayerPokemonStats(playerPokemonIndex);
            await Task.Delay(3000);
            // Met await Task.Delay(aantal milliseconden) kan je een pauze inlassen
            // Let op ! Gebruik dit voor je eigen veiligheid enkel in deze methode. 
            

            tbkFeedback.Text = "Do your move... ";
            grpButtons.IsEnabled = true;
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnBag_Click(object sender, RoutedEventArgs e)
        {
            lstBagItems.SelectedIndex = 0;
            //OPTIONAL EXTRA
            //newWindow.Show();
            //service.SelectBagItem(service.PlayerPokemon[0], newWindow.lstBagItems.SelectedIndex);
        }

        void DisplayPlayerPokemonStats(int index)
        {
            lblPlayerPokemon.Content = string.Empty;
            lblPlayerPokemon.Content = service.PlayerPokemons[playerPokemonIndex];
            pgbPlayerHealth.Value = service.PlayerPokemons[playerPokemonIndex].Health;
        }

        void DisplayComputerPokemonStats(int index)
        {
            lblComputerPokemon.Content = string.Empty;
            lblComputerPokemon.Content = service.ComputerPokemons[computerPokemonIndex];
            pgbComputerHealth.Value = service.ComputerPokemons[computerPokemonIndex].Health;
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
            ///*newWindow.*/lstBagItems.Items.Clear();

            //foreach (BagItem item in service.BagItems)
            //{
            //    /*newWindow.*/lstBagItems.Items.Add(item);
            //}

            lstBagItems.Items.Clear();

            foreach (BagItem item in service.BagItems)
            {
                lstBagItems.Items.Add(item);
            }



        }

        private void LstBagItems_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BagItem selectedItem = lstBagItems.SelectedItem as BagItem;

            service.SelectBagItem(service.PlayerPokemons[playerPokemonIndex], selectedItem);
            service.LevelUp(service.PlayerPokemons[playerPokemonIndex], service.ComputerPokemons[computerPokemonIndex]);
            DisplayPlayerPokemonStats(playerPokemonIndex);
        }

        private void BtnChangePokemon_Click(object sender, RoutedEventArgs e)
        {
            if (playerPokemonIndex < service.PlayerPokemons.Count -1)  //Battleservice method 
            {
                playerPokemonIndex++;
            }
            else playerPokemonIndex = 0;
            DisplayPlayerPokemonStats(playerPokemonIndex);
            GetPlayerImage(service.PlayerPokemons[playerPokemonIndex].Name);
        }
    }
}
