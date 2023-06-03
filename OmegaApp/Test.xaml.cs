using System;
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
    /// Логика взаимодействия для Test.xaml
    /// </summary>
    public partial class Test : Page
    {
        static AppOmegaEntities BDApp = new AppOmegaEntities();
        public Test()
        {
            InitializeComponent();
            if (Loginer.IsAdmin)
            {
                btn_Add.Visibility = Visibility.Visible;
                items_list.ItemsSource = BDApp.Applications.ToList();
            }
            else
            {
                items_list.ItemsSource = BDApp.Applications.Where(a => a.DepartStaff.Department.ID_Depart == Loginer.DepartID).ToList();
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void btn_ful_info_Click(object sender, RoutedEventArgs e)
        {
            var temp = BDApp.Applications.ToList().Where(a => a.ID_Application.ToString() == (sender as Button).Tag.ToString()).FirstOrDefault();
            Grp_info.DataContext = temp;
        }
        private void btn_Click_Add(object sender, RoutedEventArgs e)
        {
            //WinAdd add = new WinAdd();
            //add.Show();     
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string Search = SearchTxb.Text.Trim();
                BDApp = new AppOmegaEntities();
                bool IsSearch = false;
                if (SearchTxb.Text == String.Empty)
                {
                    IsSearch = true;
                }
                else
                {
                    IsSearch = false;
                }

                items_list.ItemsSource = BDApp.Applications.Where(a => ((a.Theme + " " + a.DateStart + "." + a.GuestRoom.Room.Corpus.Quarter.QuarterName + " " + a.StatusApp.StatusName + " ").Trim().ToLower().Contains(Search.ToLower()) | IsSearch)).ToList();
                
                //).Where(a => ((a.DriverSurname + " " + a.DriverName + " " + a.DriverPatronymic).Trim().ToLower().Contains(Search.ToLower()) | IsSearch)).ToList();
                //dtgDrivers.ItemsSource = Drivers;
            }
        }
    }
}
