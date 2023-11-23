﻿using Pra.Pe1.PokeBattle.Core.Entities;
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
        //BagItemsListWindow newWindow;

        int playerPokemonIndex;
        int computerPokemonIndex;


        public MainWindow()
        {
            InitializeComponent();
            service = new BattleService();
            //newWindow = new BagItemsListWindow();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetComputerImage(service.ComputerPokemon[playerPokemonIndex].Name);
            GetPlayerImage(service.PlayerPokemon[computerPokemonIndex].Name);
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
            service.Attack(service.ComputerPokemon[computerPokemonIndex]);
            service.LevelUp(service.PlayerPokemon[playerPokemonIndex], service.ComputerPokemon[computerPokemonIndex]);
            await Task.Delay(3000);
            tbkFeedback.Text = $"...{service.PlayerPokemon[playerPokemonIndex].Name} damaged {service.ComputerPokemon[computerPokemonIndex].Name} with {service.Damage[service.Damage.Count-1]}... ";
            


            if (service.ComputerPokemon[computerPokemonIndex].Health <= 0)
            {
                service.ComputerPokemon[computerPokemonIndex].Health = 0;
                DisplayComputerPokemonStats(computerPokemonIndex);
                await Task.Delay(3000);
                tbkFeedback.Text = $"...Computer's {service.ComputerPokemon[computerPokemonIndex].Name} died - switching... ";
                await Task.Delay(3000);
                service.ComputerPokemon.RemoveAt(computerPokemonIndex);

                if (service.ComputerPokemon.Count == 0)

                {
                    tbkFeedback.Text = $"...You murdered all computer's Pokemon you win...";
                    GetComputerImage(null);
                    await Task.Delay(3000);
                    return;
                }

                tbkFeedback.Text = $"...Changed to: {service.ComputerPokemon[computerPokemonIndex].Name}... ";
                GetComputerImage(service.ComputerPokemon[computerPokemonIndex].Name);
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

            service.Attack(service.PlayerPokemon[playerPokemonIndex]);          
            tbkFeedback.Text = $"...{service.ComputerPokemon[computerPokemonIndex].Name} damaged {service.PlayerPokemon[playerPokemonIndex].Name} with {service.Damage[service.Damage.Count - 1]}... ";
            service.LevelUp(service.ComputerPokemon[computerPokemonIndex], service.PlayerPokemon[playerPokemonIndex]);
            

            if (service.PlayerPokemon[playerPokemonIndex].Health <= 0)
            {
                service.PlayerPokemon[playerPokemonIndex].Health = 0;
                DisplayPlayerPokemonStats(playerPokemonIndex);
                await Task.Delay(3000);
                tbkFeedback.Text = $"...Your {service.PlayerPokemon[playerPokemonIndex].Name} died - switching ";
                await Task.Delay(3000);
                service.PlayerPokemon.RemoveAt(playerPokemonIndex);

                if (service.PlayerPokemon.Count == 0)

                {
                    tbkFeedback.Text = $"...All your Pokemon died, You lose...";
                    await Task.Delay(3000);
                    GetPlayerImage(null);
                    return;
                }

                tbkFeedback.Text = $"...Changed to: {service.PlayerPokemon[playerPokemonIndex].Name}... ";
                GetPlayerImage(service.PlayerPokemon[playerPokemonIndex].Name);
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
            lblPlayerPokemon.Content = service.PlayerPokemon[playerPokemonIndex];
            pgbPlayerHealth.Value = service.PlayerPokemon[playerPokemonIndex].Health;
        }

        void DisplayComputerPokemonStats(int index)
        {
            lblComputerPokemon.Content = string.Empty;
            lblComputerPokemon.Content = service.ComputerPokemon[computerPokemonIndex];
            pgbComputerHealth.Value = service.ComputerPokemon[computerPokemonIndex].Health;
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

            service.SelectBagItem(service.PlayerPokemon[playerPokemonIndex], index);
            service.LevelUp(service.PlayerPokemon[playerPokemonIndex], service.ComputerPokemon[computerPokemonIndex]);
            DisplayPlayerPokemonStats(playerPokemonIndex);
        }

        private void BtnChangePokemon_Click(object sender, RoutedEventArgs e)
        {
            if (playerPokemonIndex < service.PlayerPokemon.Count -1)
            {
                playerPokemonIndex++;
            }
            else playerPokemonIndex = 0;
            DisplayPlayerPokemonStats(playerPokemonIndex);
            GetPlayerImage(service.PlayerPokemon[playerPokemonIndex].Name);
        }
    }
}
