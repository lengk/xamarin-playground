using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Playground.Helpers;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Playground.Pages {
    public partial class TaskFactoryTest : ContentPage {
        RepeatableTaskManager rtm = RepeatableTaskManager.Instance;

        public TaskFactoryTest() {
            InitializeComponent();
            StartTasks();
        }

        async void StartTasks(){
            var id = "task";
            rtm.StartRepeatingAction(id, () => Debug.WriteLine("asda"));
            await Task.Delay(10000);
            rtm.StopRepeatingAction(id);
        }

    }
}
