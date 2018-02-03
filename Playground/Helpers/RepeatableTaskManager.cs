using System;
using Playground.Util;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace Playground.Helpers {
    public class RepeatableTaskManager : Singleton<RepeatableTaskManager> {
        List<RepeatingAction> Tasks = new List<RepeatingAction>();

        public RepeatingAction StartRepeatingAction(string id, Action action, int delay = 1000, bool manage = true) {
            RepeatingAction task = new RepeatingAction(action, id, delay);
            if (manage) {
                Tasks.Add(task);
            }
            return task;
        }

        public void StopRepeatingAction(string id) {
            var rt = Tasks.FirstOrDefault(t => t.TaskID == id);
            if (rt != null) {
                rt.Stop();
            }
        }

        public class RepeatingAction {
            public int Delay;
            public Action Action { get; set; }
            public Task Task { get; set; }
            CancellationToken CancelToken;
            CancellationTokenSource CancellationTokenSource;
            public string TaskID { get; set; }

            public RepeatingAction(Action action, string id, int delay = 1000) {
                Delay = delay;
                TaskID = id;
                Action = action;
                Task = Start();
            }

            Task Start() {
                CancellationTokenSource = new CancellationTokenSource();
                CancelToken = CancellationTokenSource.Token;
                return Task.Run(async () => {
                    while (true) {
                        Action.Invoke();
                        CancelToken.ThrowIfCancellationRequested();
                        await Task.Delay(Delay);
                    }
                }, CancelToken);
            }

            public void Stop() {
                var Started = Task?.Status == TaskStatus.WaitingForActivation;
                if (Started) {
                    CancellationTokenSource.Cancel();
                }
            }
        }
    }


}
