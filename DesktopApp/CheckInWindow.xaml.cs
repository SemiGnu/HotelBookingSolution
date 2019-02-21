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
            //string username = "";
            int numberOfBeds = 0;


            try
            {
                numberOfBeds = int.Parse(NumberOfBeds.Text);
            }catch(FormatException exc)
            {
                Console.Write(exc);
                MessageBox.Show("Number of Beds must be a number");
            }

            //try
            //{
            //    username = (string)customer.Username.Local.Where(cn => cn.Name.Equals(CustomerName.Text));
            //}catch(Exception exc)
            //{
            //    Console.Write(exc);
            //}

            //if(CustomerName.Text.Length > 0 || )
            

        }
    }
}
