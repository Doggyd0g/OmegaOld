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
    /// Логика взаимодействия для LoginWin.xaml
    /// </summary>
    public partial class LoginWin : Window
    {

        static AppOmegaEntities BDApp = new AppOmegaEntities();
        public LoginWin()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var loginer = BDApp.Staff.ToList().Where(a => a.NameUser == tb_login.Text & a.Passwordss == pw_password.Password).FirstOrDefault();
            if (loginer != null)
            {
                var logindepart = BDApp.DepartStaff.Where(o => o.ID_Staff == loginer.ID_Staff).FirstOrDefault();
                if(logindepart != null)
                {
                    Loginer.DepartID = logindepart.ID_Depart;
                    Loginer.IsAdmin = (bool)loginer.IsAdmin;                                  
                }
                else
                {
                    Loginer.DepartID = 0;
                    Loginer.IsAdmin = false;
                }               
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(
                 "Пароль или логин введены неверно",
                 "Ошибка",
                 MessageBoxButton.OK,
                 MessageBoxImage.Error);
            }
        }
        public bool check = true;
        private void btnPassword_Click(object sender, RoutedEventArgs e)
        {
            if (check)
            {
                var password = pw_password.Password;
                txbPass.Text = password;
                txbPass.Visibility = Visibility.Visible;
                pw_password.Visibility = Visibility.Hidden;
                check = false;
            }
            else
            {
                var password = txbPass.Text;
                pw_password.Password = password;
                txbPass.Visibility = Visibility.Hidden;
                pw_password.Visibility = Visibility.Visible;
                check = true;
            }
        }
    }
}
