using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Task2.ThreadsPool
{
    public class ThreadsPool : IDisposable
    {
        AutoResetEvent autoResetEvent = new AutoResetEvent(true);
        public List<Thread> threads { get; private set; } = new List<Thread>();
        Queue<Action> tasks = new Queue<Action>();
        volatile bool isExists;

        public ThreadsPool(uint threadsQuantity)
        {
            isExists = true;

            for(int i = 0; i < threadsQuantity; i++)
            {
                Thread thread = new Thread(new ThreadStart(Execute));

                thread.Name = $"ThreadsPool's thread #{i}";
                thread.IsBackground = true;

                threads.Add(thread);
                thread.Start();
            }
        }

        void WaitingNewTasks()
        {
            while(tasks.Count == 0 && isExists)
            {
                //Waiting for new tasks
            }

            if (isExists)
            {
                autoResetEvent.Set();
            }
        }

        void Execute()
        {
            while (isExists)
            {
                autoResetEvent.WaitOne();

                if(tasks.Count > 0 && isExists)
                {
                    Action task = tasks.Dequeue();

                    autoResetEvent.Set();

                    task?.Invoke();
                }
                else if(tasks.Count == 0 && isExists)
                {
                    Thread thread = new Thread(new ThreadStart(WaitingNewTasks));

                    thread.Name = "Waiting thread";
                    thread.IsBackground = true;

                    thread.Start();
                }
            }
        }

        public void Enqueue(Action tasksDelegate)
        {
            List<Delegate> taskDelegates = tasksDelegate.GetInvocationList().ToList();

            foreach(var taskDelegate in taskDelegates)
            {
                Action task = (Action)taskDelegate;

                tasks.Enqueue(task);
            }
        }

        public void Dispose()
        {
            while(tasks.Count != 0)
            {
                //Waiting for completion of all tasks
            }

            isExists = false;

            for(int i = 0; i < threads.Count; i++)
            {
                autoResetEvent.Set();
            }

            for(int i = 0; i < threads.Count; i++)
            {
                threads[i].Join();
            }

            threads.Clear();
            tasks.Clear();
            autoResetEvent.Dispose();
        }
    }
}