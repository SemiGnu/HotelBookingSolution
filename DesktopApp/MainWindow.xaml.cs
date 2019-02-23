using DesktopApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DatabaseModel;


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
        dat154_19_2Entities dac = new dat154_19_2Entities();
        DbSet<Room> room;
        DbSet<Booking> booking;
        DbSet<Customer> customer;
        DbSet<ServiceTask> ServiceTask;
        public MainWindow()
        {
            InitializeComponent();
            //Binding the data from the database
            room = dac.Room;
            booking = dac.Booking;
            customer = dac.Customer;
            ServiceTask = dac.ServiceTask;

            //loading the data from the database
            room.Load();
            booking.Load();
            customer.Load();
            ServiceTask.Load();

            //filling the list in reservations with data from booking
            reservationList.DataContext = booking.Local;

            //filling the list in reservations with data from booking, only "Pending" ServiceTasks -t
            ServiceTaskListView.DataContext = ServiceTask.Local.Where<ServiceTask>(ServiceTask => ServiceTask.Status == "Pending");


        }


        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {
            new CheckInWindow(dac).ShowDialog();
        }

        // Legger til en room service ServiceTask, henter roomid fra room number input -t
        private void OrderRoomServiceButton_Click(object sender, RoutedEventArgs e)
        {
            int roomInput = 0;
            Int32.TryParse(ServiceTaskEntryRoomNumber.Text, out roomInput);
            try
            {
                int roomId = (
                        from r in room
                        where r.RoomNumber == roomInput
                        select r
                        ).First<Room>().RoomId;
                ServiceTask t = new ServiceTask
                {
                    RoomId = roomId,
                    TimeIssued = DateTime.Now,
                    TypeOfService = "Room Service",
                    Status = "Pending",
                    Description = ServiceTaskEntryDescription.Text.Trim(),// sjekker at strengen ikke er for lang
                    TimeCompleted = null
                };

                dac.ServiceTask.Add(t);
                try
                {
                    dac.SaveChanges();
                }
                catch (Exception er)
                {
                    new ErrorWindow(er).ShowDialog();
                    throw;
                }
                ICollectionView view = CollectionViewSource.GetDefaultView(ServiceTaskListView.ItemsSource);
                view.Refresh();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Room number does not exist!", "Room entry error", MessageBoxButton.OK);
            }

        }
        // se OrderRoomServiceButton_Click -t 
        private void OrderMaintainanceButton_Click(object sender, RoutedEventArgs e)
        {
            int roomInput = 0;
            Int32.TryParse(ServiceTaskEntryRoomNumber.Text, out roomInput);
            try
            {
                int roomId = (
                        from r in room
                        where r.RoomNumber == roomInput
                        select r
                        ).First<Room>().RoomId;
                ServiceTask t = new ServiceTask
                {
                    RoomId = roomId,
                    TimeIssued = DateTime.Now,
                    TypeOfService = "Maintainance",
                    Status = "Pending",
                    Description = ServiceTaskEntryDescription.Text.Trim(),// sjekker at strengen ikke er for lang
                    TimeCompleted = null
                };

                dac.ServiceTask.Add(t);
                try
                {
                    dac.SaveChanges();
                }
                catch (Exception er)
                {
                    new ErrorWindow(er).ShowDialog();
                    throw;
                }
                ICollectionView view = CollectionViewSource.GetDefaultView(ServiceTaskListView.ItemsSource);
                view.Refresh();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Room number does not exist!", "Room entry error", MessageBoxButton.OK);
            }

        }
        // tallvalidator, stjålet fra Kishor på stack overflow -t
        private void ServiceTaskEntryRoomNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        // sjekker at beskrivelsen ikke er for lang -t
        private void ServiceTaskEntryDescription_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (ServiceTaskEntryDescription.Text.Length > 100);
        }

        private void ReservationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new CheckOutWindow(dac).ShowDialog();
        }

       

        private void CheckOutButton_Click(object sender, RoutedEventArgs e)
        {
            new CheckOutWindow(dac).ShowDialog();

        }


        // slette en ServiceTask fra listen v/dobbelklikk -t
        private void ServiceTaskListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ServiceTaskListView.SelectedItem as ServiceTask;
            MessageBoxResult messageBoxResult = MessageBox.Show(String.Format("Are you sure you want to delete this ServiceTask?\n Room Number: {0}\nType: {1}\nDescription: {2}", selectedItem.Room.RoomNumber, selectedItem.TypeOfService, selectedItem.Description), "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ServiceTask.Remove(selectedItem);
                dac.SaveChanges();
                ICollectionView view = CollectionViewSource.GetDefaultView(ServiceTaskListView.ItemsSource);
                view.Refresh();
            }
        }

    }
}

  