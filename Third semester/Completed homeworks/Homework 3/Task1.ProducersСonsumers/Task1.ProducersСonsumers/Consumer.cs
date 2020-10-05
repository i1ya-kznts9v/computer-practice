using System;
using System.Collections.Generic;
using System.Threading;

namespace Task1.ProducersСonsumers
{
    public class Consumer
    {
        public Thread thread;
        Semaphore semaphore;
        List<string> storage;
        static Random random = new Random();

        volatile bool isPurchasing = true;

        public Consumer(string name, List<string> storage, Semaphore semaphore)
        {
            thread = new Thread(UploadProduct);
            thread.Name = name;
            this.semaphore = semaphore;
            this.storage = storage;
            thread.Start();
        }

        void UploadProduct()
        {
            //A postcondition loop allows the queue to run to the end.
            do
            {
                semaphore.WaitOne();
                Console.WriteLine(thread.Name + " arrived at the storage.");

                string product = string.Empty;

                if (storage.Count != 0)
                {
                    int productNumber = random.Next(storage.Count);
                    product = storage[productNumber];
                    storage.RemoveAt(productNumber);
                }
                
                Console.WriteLine(thread.Name + " purchase " + (product.Equals(string.Empty)? "nothing" : product) + " and left the storage.");
                semaphore.Release();

                Thread.Sleep(50);
            }
            while (isPurchasing);
        }

        public void Stop()
        {
            isPurchasing = false;
            //thread.Join();
        }

        public void Join()
        {
            thread.Join();
        }
    }
}