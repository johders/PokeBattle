using Pra.Pe1.PokeBattle.Core.Entities;
using Pra.Pe1.PokeBattle.Core.Services;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PokeBattle.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BattleService service;
        BagItemsListWindow newWindow;

        public MainWindow()
        {
            InitializeComponent();
            service = new BattleService();
            newWindow = new BagItemsListWindow();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetComputerImage(service.ComputerPokemon[0].Name);
            GetPlayerImage(service.PlayerPokemon[0].Name);
            DisplayPlayerPokemonStats(0);
            DisplayComputerPokemonStats(0);

            LoadBagItemsListBox();
        }
        private void BtnBag_Click(object sender, RoutedEventArgs e)
        {
            newWindow.Show();
            //service.SelectBagItem(service.PlayerPokemon[0], newWindow.lstBagItems.SelectedIndex);         
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }     

        private async void BtnFight_Click(object sender, RoutedEventArgs e)
        {
            // Door de groupbox te disablen worden alle onderliggende controls ook gedisabled.
            grpButtons.IsEnabled = false;

            // Geef alle updates door in de feedback textblock
            tbkFeedback.Text = "...Player is attacking computer... ";

            service.Attack(service.ComputerPokemon[0]);
            DisplayComputerPokemonStats(0);

            // Met await Task.Delay(aantal milliseconden) kan je een pauze inlassen
            // Let op ! Gebruik dit voor je eigen veiligheid enkel in deze methode. 
            await Task.Delay(1000);

            tbkFeedback.Text = "...Computer is attacking player... ";

            service.Attack(service.PlayerPokemon[0]);
            DisplayPlayerPokemonStats(0);

            // Met await Task.Delay(aantal milliseconden) kan je een pauze inlassen
            // Let op ! Gebruik dit voor je eigen veiligheid enkel in deze methode. 
            await Task.Delay(1000);

            tbkFeedback.Text = "Do your move... ";
            grpButtons.IsEnabled = true;
        }

        void DisplayPlayerPokemonStats(int index)
        {
            lblPlayerPokemon.Content = string.Empty;
            lblPlayerPokemon.Content = service.PlayerPokemon[index];
            pgbPlayerHealth.Value = service.PlayerPokemon[index].Health;
        }

        void DisplayComputerPokemonStats(int index)
        {
            lblComputerPokemon.Content = string.Empty;
            lblComputerPokemon.Content = service.ComputerPokemon[index];
            pgbComputerHealth.Value = service.ComputerPokemon[index].Health;
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
            /*newWindow.*/lstBagItems.Items.Clear();

            foreach (BagItem item in service.BagItems)
            {
                /*newWindow.*/lstBagItems.Items.Add(item);
            }
        }

        private void LstBagItems_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int index = lstBagItems.SelectedIndex;
            BagItem selectedItem = lstBagItems.SelectedItem as BagItem;

            service.SelectBagItem(service.PlayerPokemon[0], index);
            DisplayPlayerPokemonStats(0);
        }
    }
}
