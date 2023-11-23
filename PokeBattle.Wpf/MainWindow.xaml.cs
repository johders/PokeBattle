using Pra.Pe1.PokeBattle.Core.Services;
using System;
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

        public MainWindow()
        {
            InitializeComponent();
            service = new BattleService();
        }

        private async void BtnFight_Click(object sender, RoutedEventArgs e)
        {
            // Door de groupbox te disablen worden alle onderliggende controls ook gedisabled.
            grpButtons.IsEnabled = false;
            
            // Geef alle updates door in de feedback textblock
            tbkFeedback.Text = "...Player is attacking computer... ";

            // Met await Task.Delay(aantal milliseconden) kan je een pauze inlassen
            // Let op ! Gebruik dit voor je eigen veiligheid enkel in deze methode. 
            await Task.Delay(1000);

            tbkFeedback.Text = "Do your move... ";
            grpButtons.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetComputerImage(service.ComputerPokemon[0].Name);
            GetPlayerImage(service.PlayerPokemon[0].Name);
        }
       

        public void GetComputerImage(string name)
        {
            Uri relativeUri = new Uri("../Images/" + name + ".png", UriKind.Relative);
            imgComputer.Source = new BitmapImage(relativeUri);
        }

        public void GetPlayerImage(string name)
        {
            Uri relativeUri = new Uri("../Images/" + name + ".png", UriKind.Relative);
            imgPlayer.Source = new BitmapImage(relativeUri);
        }
    }
}
