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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static AppOmegaEntities BDApp = new AppOmegaEntities();
        public MainWindow()
        {
            InitializeComponent();
            ApplicFrame.Content = new ApplicPage();
            MapFrame.Content = new MapPage();
            if (Loginer.IsAdmin)
            {
                stFr.Visibility = Visibility.Visible;
                StaffFrame.Content = new StaffPage();         
            }
            else
            {
                stFr.Visibility = Visibility.Hidden;
            }

        }
    }
}
