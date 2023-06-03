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
using System.Windows.Shapes;

namespace OmegaApp
{
    /// <summary>
    /// Логика взаимодействия для WinStaffAdd.xaml
    /// </summary>
    public partial class WinStaffAdd : Window
    {
        AppOmegaEntities BDApp = new AppOmegaEntities();
        public WinStaffAdd(AppOmegaEntities BDApp)
        {
            InitializeComponent();
            this.BDApp = BDApp;
            Cmb_DepartMent.ItemsSource = BDApp.Department.ToList();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if(tbx_LastName.Text !=  "" & tbx_FirstName.Text != ""  & tbx_login.Text != "" & tbx_password.Text != "" & Cmb_DepartMent.Text != "")
            {
                Staff staff = new Staff();
                staff.LastName = tbx_LastName.Text;
                staff.FirstName = tbx_FirstName.Text;
                staff.Patronymic = tbx_Patronym.Text;
                staff.Phone = tbx_phone.Text;
                staff.NameUser = tbx_login.Text;
                staff.Passwordss = tbx_password.Text;
                BDApp.Staff.Add(staff);

                DepartStaff departStaff = new DepartStaff();
                departStaff.ID_Depart = BDApp.Department.Where(a => a.DepartName.ToString() == Cmb_DepartMent.Text).FirstOrDefault().ID_Depart;
                departStaff.ID_Staff = staff.ID_Staff;
                BDApp.DepartStaff.Add(departStaff);          
                BDApp.SaveChanges();
                MessageBox.Show("Добавлен новый сотрудник", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
