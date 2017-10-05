using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Playground.Pages {
    /// <summary>
    /// Playing around with cancellation tokens and tasks
    /// </summary>
    public partial class TaskAroundPage : ContentPage {
        public ObservableCollection<string> ListData = new ObservableCollection<string>();
        CancellationTokenSource CancelTokenSource = new CancellationTokenSource();
        CancellationToken CancelToken;
        Task BuildListTask;
        bool Started;
        public TaskAroundPage() {
            InitializeComponent();
            NumbersList.ItemsSource = ListData;
        }

        /// <summary>
        /// 
        /// </summary>
        void StartBuildingList() {
            CancelTokenSource = new CancellationTokenSource();
            CancelToken = CancelTokenSource.Token;
            BuildListTask = Task.Run(CreateBuildTask, CancelToken);
        }


        /// <summary>
        /// Continue Adding to the list until cancelled
        /// </summary>
        /// <returns>The building list.</returns>
        async Task CreateBuildTask() {
            Started = true;
            while (true) {
                await Task.Delay(100);
                CancelToken.ThrowIfCancellationRequested();
                // Will throw error unless invoked on main thread
                Device.BeginInvokeOnMainThread(() => ListData.Add("" + ListData.Count));
            }
            return;
        }

        /// <summary>
        /// When the button at the top of the page is pressed
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void StartStop(object sender, EventArgs e) {
            var button = sender as Button;
            var action = Started ? "Stopping" : "Starting";
            button.Text = Started ? "Start" : "Stop";
            Debug.WriteLine($"Clicked - {action}");
            if (!Started) {
                StartBuildingList();
            } else {
                Debug.WriteLine("Cancelling");
                CancelTokenSource.Cancel();
                Started = false;
            }

        }

        void CancelClicked(object sender, System.EventArgs e) {
            ListData.Clear();
        }
    }
}
