﻿using Pra.Pe1.PokeBattle.Core.Entities;
using Pra.Pe1.PokeBattle.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PokeBattle.Wpf
{
    /// <summary>
    /// Interaction logic for BagItemsListWindow.xaml
    /// </summary>
    public partial class BagItemsListWindow : Window
    {
        SelectedBagItem selectedBagItem;

        public BagItemsListWindow()
        {
            InitializeComponent();
            selectedBagItem = new SelectedBagItem();
        }

        public void LstBagItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BagItem selectedItem = lstBagItems.SelectedItem as BagItem;
            selectedBagItem.SelectedBagItems.Add(selectedItem);
                     
        }
    }
}
