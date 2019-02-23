using DatabaseModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for CheckOutWindow.xaml
    /// </summary>
    public partial class CheckOutWindow : Window
    {
        dat154_19_2Entities dac;
        private DbSet<Booking> booking;

        public CheckOutWindow()
        {

            InitializeComponent();
        }

        public CheckOutWindow(dat154_19_2Entities dac)
        {
            booking = dac.Booking;
            this.dac = dac;
            InitializeComponent();
            booking.Load();
            ReservationListView.DataContext = booking.Local;

        }

        private void CheckOutCustomer_Click(object sender, RoutedEventArgs e)
        {
            string userFullName;
            int roomNr;


            try
            {
                userFullName = CustomerNameTextBox.Text;
                roomNr = int.Parse(RoomNumberTextBox.Text);
                Customer c = dac.Customer.FirstOrDefault(cu => cu.Name == userFullName);
                Booking b = dac.Booking.FirstOrDefault(bo => bo.Customer.Username == c.Username && bo.RoomId == roomNr);

                dac.Booking.Remove(b);
                dac.SaveChanges();
                CustomerNameTextBox.Text = RoomNumberTextBox.Text = "";


            } catch (Exception exc)
            {
                Console.WriteLine(exc);
                MessageBox.Show("Denne kunden finnes ikke! Prøv igjen");

            }


        }

        private void CancelCheckOutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SelectCustomer_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Booking selectedBooking = (Booking) ReservationListView.SelectedItem;
            RoomNumberTextBox.Text = selectedBooking.RoomId.ToString();

            CustomerNameTextBox.Text = dac.Customer.FirstOrDefault(cu => cu.Username == selectedBooking.CustomerUsername).Name;


        }
    }
}
