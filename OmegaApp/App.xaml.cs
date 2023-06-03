using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OmegaApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
    }
    public class Loginer
    {
        public static int DepartID { get; set; } = 0;
        public static bool IsAdmin { get; set; } = false;
        public static string vis { get; set; } = "Visible";
        public static string hid { get; set; } = "Hidden";




    }
}
