using DatabaseModel;
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

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for CheckOutWindow.xaml
    /// </summary>
    public partial class CheckOutWindow : Window
    {
        dat154_19_2Entities dac;

        public CheckOutWindow()
        {
            InitializeComponent();
        }

        public CheckOutWindow(dat154_19_2Entities dac)
        {
            this.dac = dac;
            InitializeComponent();

        }

        private void CheckOutCustomer_Click(object sender, RoutedEventArgs e)
        {
            string userFullName;

            try
            {
                userFullName = CustomerNameTextBox.Text;
                Customer c = dac.Customer.Where(cu => cu.Name == userFullName).FirstOrDefault<Customer>();
                Booking b = dac.Booking.Where(bo => bo.Customer == c).FirstOrDefault<Booking>();

                dac.Booking.Remove(b);
                dac.SaveChanges();


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
    }
}
