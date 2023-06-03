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
    /// Логика взаимодействия для ApplicPage.xaml
    /// </summary>
    public partial class ApplicPage : Page
    {
        static AppOmegaEntities BDApp = new AppOmegaEntities();
        public ApplicPage()
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
        private void btn_ful_info_Click(object sender, RoutedEventArgs e)
        {
            var temp = BDApp.Applications.ToList().Where(a => a.ID_Application.ToString() == (sender as Button).Tag.ToString()).FirstOrDefault();
            Grp_info.DataContext = temp;
        }
        private void btn_Click_Add(object sender, RoutedEventArgs e)
        {
            WinAdd add = new WinAdd(BDApp);
            add.Show();
            items_list.ItemsSource = BDApp.Applications.ToList();
        }
        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string Search = SearchTxb.Text;
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
                items_list.ItemsSource = BDApp.Applications.Where(a => (a.Theme + " " + a.DateStart.ToString() + " " + a.GuestRoom.Room.Corpus.Quarter.QuarterName + " " + a.StatusApp.StatusName + " ").Trim().ToLower().Contains(Search.ToLower()) | IsSearch).ToList();
            }
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            if (Loginer.IsAdmin)
            {
                var temp = BDApp.Applications.ToList().Where(a => a.ID_Application.ToString() == (sender as Button).Tag.ToString()).Single();
                items_list.DataContext = temp;

                MessageBoxResult res = MessageBox.Show("Вы действительно хотите удалить данную заявку?",
                "Внимание", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Warning);
                if (res == MessageBoxResult.Yes)
                {
                    BDApp.Applications.Remove(temp);
                    BDApp.SaveChanges();
                    items_list.ItemsSource = BDApp.Applications.ToList();
                }
            }
            else
            {
                MessageBox.Show("У вас нет доступа для удаления заявок", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                items_list.ItemsSource = BDApp.Applications.ToList();
            }
        }
    }
}
