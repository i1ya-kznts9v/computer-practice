using System.Collections.Generic;
using System.Threading;
using Task1.ProducersСonsumers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task1.ProducersConsumersUnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void StorageTest()
        {
            Semaphore semaphore = new Semaphore(1, 1);
            List<string> storage = new List<string>();

            uint producersQuantity = 3;
            uint consumersQuantity = 6;

            Producer[] producers = new Producer[producersQuantity];
            Consumer[] сonsumers = new Consumer[consumersQuantity];

            for(int i = 0; i < producersQuantity; i++)
            {
                producers[i] = new Producer($"Producer #{i}", storage, semaphore);
            }

            Thread.Sleep(2000);

            for (int i = 0; i < producersQuantity; i++)
            {
                producers[i].RequestStop();
            }

            for (int i = 0; i < producersQuantity; i++)
            {
                producers[i].thread.Join();
            }

            Assert.IsTrue(storage.Count != 0);

            for (int i = 0; i < consumersQuantity; i++)
            {
                сonsumers[i] = new Consumer($"Consumer #{i}", storage, semaphore);
            }

            Thread.Sleep(4000);

            for (int i = 0; i < producersQuantity; i++)
            {
                сonsumers[i].RequestStop();
            }

            for (int i = 0; i < producersQuantity; i++)
            {
                сonsumers[i].thread.Join();
            }

            Assert.IsTrue(storage.Count == 0);
        }
    }
}