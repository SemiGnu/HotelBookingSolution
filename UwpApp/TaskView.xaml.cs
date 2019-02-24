using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TaskView : Page
    {
        private List<ServiceTask> serviceTaskList = new List<ServiceTask>();
        private string CurrentServiceType;
        

        public TaskView()
        {
            this.InitializeComponent();
            UpdateServiceTaskList();
        }

       
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CurrentServiceType = e.Parameter.ToString();
            typeOfService.Text = CurrentServiceType;
            TaskListView.ItemsSource = serviceTaskList.Where(stask => stask.TypeOfService == CurrentServiceType).OrderBy(stask => stask.TimeIssued);
            base.OnNavigatedTo(e);
        } 

        // henter servicetasks fra db -t
        private void UpdateServiceTaskList()
        {
            using (var client = new HttpClient())
            {
                //først tasks -t
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(new Uri("http://localhost:52285/api/ServiceTasks"));
                });
                task.Wait();
                serviceTaskList = JsonConvert.DeserializeObject<List<ServiceTask>>(response);

                //så en roomnumbers fra roomid -t
                List<Task> roomNumberTaskList = new List<Task>();
                foreach (ServiceTask serviceTask in serviceTaskList)
                {
                    roomNumberTaskList.Add(Task.Run(async () =>
                    {
                        string uri = String.Format("http://localhost:52285/api/Rooms/{0}", serviceTask.RoomId);
                        response = await client.GetStringAsync(new Uri(uri));
                        serviceTask.Room = JsonConvert.DeserializeObject<Room>(response);
                    }));
                }
                Task.WaitAll(roomNumberTaskList.ToArray());

                TaskListView.ItemsSource = serviceTaskList.Where(stask => stask.TypeOfService == typeOfService.Text);
            }
        }


        private void Add_Task(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnToSelection(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SelectionView));
        }
    }
}
