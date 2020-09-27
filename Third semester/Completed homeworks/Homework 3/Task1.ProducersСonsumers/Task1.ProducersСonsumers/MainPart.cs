using System;
using System.Collections.Generic;
using System.Threading;

namespace Task1.ProducersСonsumers
{
    class MainPart
    {
        static void InputAndChecks(out uint producersQuantity, out uint consumersQuantity)
        {
            Console.Write("Enter the number of producers (from 1 to 32): ");

            //Randomly selected upper limit
            while (!uint.TryParse(Console.ReadLine(), out producersQuantity) || producersQuantity == 0 || producersQuantity > 32)
            {
                Console.WriteLine("Try again! It must be a number between 1 and 32.");
            }

            Console.WriteLine();
            Console.Write("Enter the number of consumers (from 1 to 32): ");

            //Randomly selected upper limit
            while (!uint.TryParse(Console.ReadLine(), out consumersQuantity) || consumersQuantity == 0 || consumersQuantity > 32)
            {
                Console.WriteLine("Try again! It must be a number between 1 and 32.");
            }

            Console.WriteLine("\nPress \"Enter\" to stop and wait for the threads to shutdown safely.\n");
        }

        static void ProducersConsumersInitialize(ref Producer[] producers, uint producersQuantity, ref Consumer[] сonsumers, uint consumersQuantity,
            List<string> storage, Semaphore semaphore)
        {
            for (int i = 0; i < producersQuantity; i++)
            {
                producers[i] = new Producer($"Producer #{i}", storage, semaphore);
            }

            for (int i = 0; i < consumersQuantity; i++)
            {
                сonsumers[i] = new Consumer($"Consumer #{i}", storage, semaphore);
            }
        }

        //Graceful shutdown by exiting the method on which the threads were based.
        static void ProducersConsumersDeinitialize(ref Producer[] producers, uint producersQuantity, ref Consumer[] сonsumers, uint consumersQuantity)
        {
            for (int i = 0; i < producersQuantity; i++)
            {
                producers[i].RequestStop();
            }

            for (int i = 0; i < consumersQuantity; i++)
            {
                сonsumers[i].RequestStop();
            }

            for (int i = 0; i < producersQuantity; i++)
            {
                producers[i].thread.Join();
            }

            for (int i = 0; i < consumersQuantity; i++)
            {
                сonsumers[i].thread.Join();
            }

            Console.WriteLine("\nAll threads of producers and consumers have been successfully completed.\n");
        }

        static void Main(string[] args)
        {
            Semaphore semaphore = new Semaphore(1, 1);
            List<string> storage = new List<string>();

            uint producersQuantity;
            uint consumersQuantity;

            InputAndChecks(out producersQuantity, out consumersQuantity);

            Producer[] producers = new Producer[producersQuantity];
            Consumer[] сonsumers = new Consumer[consumersQuantity];

            ProducersConsumersInitialize(ref producers, producersQuantity, ref сonsumers, consumersQuantity, storage, semaphore);

            Console.ReadKey();
            ProducersConsumersDeinitialize(ref producers, producersQuantity, ref сonsumers, consumersQuantity);

            Console.ReadKey();
        }
    }
}