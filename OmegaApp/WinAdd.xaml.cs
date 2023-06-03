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
    public partial class WinAdd : Window
    {
        AppOmegaEntities BDApp = new AppOmegaEntities();      
        public WinAdd(AppOmegaEntities BDApp)
        {
            InitializeComponent();
            this.BDApp = BDApp;
            Cmb_stat.ItemsSource = BDApp.StatusApp.ToList();
            Cmb_Quarter.ItemsSource = BDApp.Quarter.ToList();
            Cmb_DepartMent.ItemsSource = BDApp.Department.ToList();

        }
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_Theme.Text != "" & Cmb_Quarter.Text != "" & Cmb_CorNum.Text != "" & Cmb_Room.Text != "" & Cmb_DepartMent.Text != "" & dp_Date.Text != "" & Cmb_stat.Text != "")
            {               
                GuestRoom guestRoom = new GuestRoom();
                guestRoom.ID_Room = BDApp.Room.Where(a => a.Corpus.CorpusNum.ToString() == Cmb_CorNum.Text & a.Corpus.Quarter.QuarterName == Cmb_Quarter.Text).First().ID_Room;
                BDApp.GuestRoom.Add(guestRoom);

                Applications applications = new Applications();
                applications.ID_Status = BDApp.StatusApp.Where(a => a.StatusName == Cmb_stat.Text).First().ID_Status;
                applications.ID_DepartStaff = BDApp.Department.Where(a => a.DepartName == Cmb_DepartMent.Text).First().ID_Depart;
                applications.GuestRoom = guestRoom;
                applications.DateStart = DateTime.Parse(dp_Date.Text);
                applications.Theme = tbx_Theme.Text;
                applications.Remark = tbx_Remark.Text;
                BDApp.Applications.Add(applications);
                BDApp.SaveChanges();           
                MessageBox.Show("Заявка сохранена", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
             
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Cmb_Quarter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if( Equals(Cmb_Quarter.SelectedValue))
            {
                Cmb_CorNum.ItemsSource = null;
                Cmb_CorNum.ItemsSource = BDApp.Corpus.ToList();
            }
            else
            {
                Cmb_CorNum.ItemsSource = null;
                Cmb_CorNum.ItemsSource = BDApp.Corpus.Where(a => a.Quarter.ID_Quarter.ToString() == Cmb_Quarter.SelectedValue.ToString()).ToList();            
            }

        }
        private void Cmb_CorNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {        
            if (Equals(Cmb_CorNum.SelectedValue))
            {
                Cmb_Room.ItemsSource = null;
                Cmb_Room.ItemsSource = BDApp.Room.ToList();
            }
            else
            {
                Cmb_Room.ItemsSource = null;
                Cmb_Room.ItemsSource = BDApp.Room.Where(a => a.Corpus.ID_Corpus.ToString() == Cmb_CorNum.SelectedValue.ToString()).ToList();
            }          
        }
        private void Cmb_Room_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
