﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Task2.ThreadsPool
{
    public class ThreadsPool : IDisposable
    {
        List<Thread> threads = new List<Thread>();
        Queue<Action> tasks = new Queue<Action>();
        volatile bool isRunning = true;
        bool isDisposed;

        public ThreadsPool(uint threadsQuantity)
        {
            for (int i = 0; i < threadsQuantity; i++)
            {
                Thread thread = new Thread(new ThreadStart(Execute));

                thread.Name = $"ThreadsPool's thread #{i}";
                thread.IsBackground = true;

                threads.Add(thread);
                thread.Start();
            }
        }

        public int GetThreadsCount()
        {
            if (isDisposed)
            {
                return(0);
            }

            return (threads.Count);
        }

        public void Enqueue(Action tasksDelegate)
        {
            if (isDisposed)
            {
                return;
            }

            Monitor.Enter(tasks);
            List<Delegate> taskDelegates = tasksDelegate.GetInvocationList().ToList();

            foreach (var taskDelegate in taskDelegates)
            {
                Action task = (Action)taskDelegate;
                
                tasks.Enqueue(task);
            }

            Monitor.PulseAll(tasks);
            Monitor.Exit(tasks);
        }

        //void Execute()
        //{
        //    while (isRunning)
        //    {
        //        Monitor.Enter(tasks);

        //        if (tasks.Count > 0 && isRunning)
        //        {
        //            Action task = tasks.Dequeue();
        //            Monitor.Exit(tasks);

        //            task?.Invoke();
        //        }
        //        else if (tasks.Count == 0 && isRunning)
        //        {
        //            Monitor.Wait(tasks);
        //            Monitor.Exit(tasks);
        //        }
        //        else if (!isRunning)
        //        {
        //            Monitor.Exit(tasks);
        //        }
        //    }
        //}

        void Execute()
        {
            while (isRunning)
            {
                Action task = null;

                Monitor.Enter(tasks);

                while(task == null && isRunning)
                {
                    if(tasks.Count == 0)
                    {
                        Monitor.Wait(tasks);

                        continue;
                    }

                    task = tasks.Dequeue();
                }

                Monitor.Exit(tasks);

                task?.Invoke();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool isDisposing)
        {
            if(isDisposed)
            {
                return;
            }

            /*Threads must terminate correctly anyway*/

            isRunning = false;

            Monitor.Enter(tasks);
            Monitor.PulseAll(tasks);
            Monitor.Exit(tasks);

            for (int i = 0; i < threads.Count; i++)
            {
                threads[i].Join();
            }

            /*Managed Resources: 
              If Dispose() was not called manually, they will be garbage collected*/

            if (isDisposing)
            {
                threads.Clear();
                tasks.Clear();
            }

            isDisposed = true;
            Console.WriteLine("\nThreadsPool is disposed.\n");
        }
        
        ~ThreadsPool()
        {
            Dispose(false);
        }
    }
}