using DesktopApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using Task = DesktopApp.Task;

namespace HotelBooking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// test 2
    /// test kommentar
    /// 
    public partial class MainWindow : Window
    {
        //ObservableCollection<TaskDummy> TaskDummyList { get; set; }
        DesktopAppConfig dac = new DesktopAppConfig();
        DbSet<Room> room;
        DbSet<Booking> booking;
        DbSet<Customer> customer;
        DbSet<Task> task;
        public MainWindow()
        {
            InitializeComponent();
            //Binding the data from the database
            room = dac.Room;
            booking = dac.Booking;
            customer = dac.Customer;
            task = dac.Task;

            //loading the data from the database
            room.Load();
            booking.Load();
            customer.Load();
            task.Load();

            //filling the list in reservations with data from booking
            reservationList.DataContext = booking.Local;

            //DataContext = this;
            //TaskDummyList = new ObservableCollection<TaskDummy> {
            //    new TaskDummy {Type = "Room Service", RoomNumber = 101, Description = "More pizza!", TimeAdded = new DateTime(2019,1,20,14,13,0) },
            //    new TaskDummy {Type = "Room Service", RoomNumber = 102, Description = "More beer!", TimeAdded = new DateTime(2019,1,20,14,12,0) },
            //    new TaskDummy {Type = "Maintainance", RoomNumber = 101, Description = "Muh fan not werking", TimeAdded = new DateTime(2019,1,20,12,13,0) }
            //};

        }


        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {
            new CheckInWindow(dac).ShowDialog();
        }
    }

    //class TaskDummy
    //{
    //    public string Type { get; set; }
    //    public int RoomNumber { get; set; }
    //    public string Description { get; set; }
    //    public DateTime TimeAdded { get; set; }
    //}
}