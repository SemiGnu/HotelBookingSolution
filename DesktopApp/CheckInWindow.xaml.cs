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
using System.Data.Entity;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for CheckInWindow.xaml
    /// </summary>
    public partial class CheckInWindow : Window
    {
        DesktopAppConfig dac;
        public CheckInWindow()
        {
            InitializeComponent();
        }

        public CheckInWindow(DesktopAppConfig dac)
        {
            InitializeComponent();
            this.dac = dac;

        }
        

        private void AddBooking_Click(object sender, RoutedEventArgs e)
        {
            string userFullName;
            int numberOfBeds;
            DateTime checkInDate;
            DateTime checkOutdate;
            
            try
            {
                numberOfBeds = int.Parse(NumberOfBedsTextBox.Text);
                userFullName = CustomerNameTextBox.Text;
                Customer c = dac.Customer.Where(cu => cu.Name == userFullName).FirstOrDefault<Customer>();
                checkInDate = DateTime.Parse(CheckInDateBox.Text);
                Room room = dac.Room.Where(ro => ro.NumberOfBeds == numberOfBeds && !ro.Booked).FirstOrDefault<Room>();
                    
                checkOutdate = DateTime.Parse(CheckOutDateBox.Text);
                Booking booking = new Booking//(2, username, numberOfBeds, checkInDate, checkOutdate)
                {

                    CustomerUsername = c.Username,
                    RoomId = room.RoomId,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutdate
                };

                dac.Booking.Add(booking);
                room.Booked = true;
                dac.SaveChanges();
                this.Close();

            } catch (FormatException ex) 
            {
                Console.WriteLine(ex);
                MessageBox.Show("Check your input");

            }
           
        }

        private void CancelCheckIn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
