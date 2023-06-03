﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OmegaApp
{
    /// <summary>
    /// Логика взаимодействия для MapPage.xaml
    /// </summary>
    public partial class MapPage : Page
    {
        static AppOmegaEntities BDApp = new AppOmegaEntities();
        public MapPage()
        {
            InitializeComponent();
        }
        private void btn_map(object sender, RoutedEventArgs e)
        {
            var CorpusCheck = BDApp.Applications.ToList().Where(a => a.GuestRoom.Room.Corpus.ID_Corpus.ToString() == (sender as Button).Tag.ToString());       
           if (CorpusCheck.Count() != 0)
            {
               items_list.ItemsSource = CorpusCheck;
                vis.Visibility = Visibility.Visible;
               bor_null.Visibility = Visibility.Hidden; 
            }
            else
            {
                vis.Visibility = Visibility.Hidden;
                bor_null.Visibility = Visibility.Visible;
            }
        }


    }
}
