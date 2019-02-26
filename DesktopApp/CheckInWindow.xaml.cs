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
using DatabaseModel;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for CheckInWindow.xaml
    /// </summary>
    public partial class CheckInWindow : Window
    {
        dat154_19_2Entities dac;
        public CheckInWindow()
        {
            InitializeComponent();
        }

        public CheckInWindow(dat154_19_2Entities dac)
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
                checkInDate = DateTime.Parse(CheckInDateBox.Text);
                checkOutdate = DateTime.Parse(CheckOutDateBox.Text);
                Customer c = dac.Customer.Where(cu => cu.Name == userFullName).FirstOrDefault<Customer>();

                List<Room> avaliableRooms = dac.Room.Where(ro => ro.NumberOfBeds >= numberOfBeds).ToList<Room>();
                Room room = null;

                foreach(Room r in avaliableRooms)
                {
                    if(checkDate(checkInDate, checkOutdate, r.RoomId))
                    {
                        room = r;
                        break;
                    }
                }

                if (room != null)
                {
                    try
                    {
                        Booking booking = new Booking//(id(auto), username, numberOfBeds, checkInDate, checkOutdate)
                        {

                            CustomerUsername = c.Username,
                            RoomId = room.RoomId,
                            CheckInDate = checkInDate,
                            CheckOutDate = checkOutdate
                        };

                        dac.Booking.Add(booking);
                        dac.SaveChanges();
                        this.Close();
                    }catch(NullReferenceException ex)
                    {
                        Console.WriteLine(e);
                        MessageBox.Show("user not found");
                    }
                }
                else
                {
                    MessageBox.Show("No room found");
                }

            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Check your input");

            }

        }

        private void CancelCheckIn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private bool checkDate(DateTime inDate, DateTime outDate, int roomNumber)
        {
            bool isAvaliable = false;

            if (inDate.CompareTo(outDate) < 0) //Check if check-in date is before check-out date
            {
                List<Booking> bookings = dac.Booking.Where(bo => bo.RoomId == roomNumber).ToList<Booking>();
                if (bookings.Count > 0) {
                    foreach (Booking b in bookings)
                    {

                        if(inDate.CompareTo(b.CheckOutDate) >= 0 || outDate.CompareTo(b.CheckInDate) <= 0) //Check if date crashes with other bookings
                        {
                            isAvaliable = true;
                            
                        }
                        else
                        {
                            isAvaliable = false;
                            break;
                        }
                    }
                }
                else
                {
                    isAvaliable = true;
                }
            }

            return isAvaliable;
        }
    }
}
