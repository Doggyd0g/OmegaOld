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
    /// Логика взаимодействия для StaffPage.xaml
    /// </summary>
    public partial class StaffPage : Page
    {
        static AppOmegaEntities BDApp;

        static IEnumerable<dynamic> StaffPd;
        void DepartUpd()
        {
            BDApp = new AppOmegaEntities();
            
            StaffPd = (from DepartStaff in BDApp.DepartStaff
                      join Staff in BDApp.Staff on DepartStaff.ID_Staff equals Staff.ID_Staff 
                      join Department in BDApp.Department on DepartStaff.ID_Depart equals Department.ID_Depart
                      select new
                      {
                          Staff.ID_Staff,
                          Staff.LastName,
                          Staff.FirstName,
                          Staff.Patronymic,
                          Staff.Phone,
                          Staff.NameUser,
                          Staff.Passwordss,
                          Department.ID_Depart,
                          Department.DepartName
                      }).ToList();

            items_list_staff.ItemsSource = StaffPd;
        }
        public StaffPage()
        {
            InitializeComponent();
            DepartUpd();
        }
        private void btn_info_staff_Click(object sender, RoutedEventArgs e)
        {
           var staff = StaffPd.Where(a => a.ID_Staff.ToString() == (sender as Button).Tag.ToString()).FirstOrDefault();
           Grp_info.DataContext = staff;           
           tb_DepName.Text = staff.DepartName;                                   
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
                    items_list_staff.ItemsSource = StaffPd.Where(a => (a.LastName + " " + a.FirstName + " " + a.Patronymic + " " + a.DepartName + " ").Trim().ToLower().Contains(Search.ToLower()) | IsSearch).ToList();              
            }
            else if(e.Key == Key.F1)
            {
                items_list_staff.ItemsSource = StaffPd;
                DepartUpd();
            }
        }
        private void btn_Click_Add(object sender, RoutedEventArgs e)
        {
            WinStaffAdd Staffadd = new WinStaffAdd(BDApp);
            Staffadd.Show();         
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                items_list_staff.ItemsSource = StaffPd;
                DepartUpd();
            }
        }
    }
}
