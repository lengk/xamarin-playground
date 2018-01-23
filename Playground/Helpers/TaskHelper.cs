using System;
using Playground.Util;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
namespace Playground.Helpers {
    public class TaskHelper : Singleton {
        List<ActionTask> Tasks = new List<ActionTask>();

        void StartTask(Action action, string id){
            
        }

        void StopTask(){
            
        }
    }

    public class ActionTask{
        Action Action { get; set; }
        Task Task { get; set; }
        CancellationToken CancelToken;
        CancellationTokenSource CancellationTokenSource;
        public string TaskID { get; set; }
    }
}
