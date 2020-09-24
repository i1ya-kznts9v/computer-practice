using System;
using System.Collections.Generic;
using System.Threading;

namespace Task1.ProducersСonsumers
{
    public class Producer
    {
        public Thread thread;
        Semaphore semaphore;
        List<string> storage;

        public bool IsWorking { private get; set; } = true;

        public Producer(string name, List<string> storage, Semaphore semaphore)
        {
            thread = new Thread(UnloadProduct);
            thread.Name = name;
            this.semaphore = semaphore;
            this.storage = storage;
            thread.Start();
        }

        void UnloadProduct()
        {
            //A postcondition loop allows the queue to run to the end.
            do
            {
                semaphore.WaitOne();
                Console.WriteLine(thread.Name + " arrived at the storage.");

                string product = thread.Name + "'s product";
                storage.Add(product);

                Console.WriteLine(thread.Name + $" put up {product} for sale and left the storage.");
                Thread.Sleep(500);
                semaphore.Release();
            }
            while (IsWorking == true);
        }
    }
}