using System;
using System.Threading;

namespace Task2.ThreadsPool
{
    class MainPart
    {
        static void InputAndChecks(out uint threadsQuantity)
        {
            Console.Write("Enter the number of threads for the ThreadsPool (from 1 to 128): ");

            while(!uint.TryParse(Console.ReadLine(), out threadsQuantity) || threadsQuantity == 0 || threadsQuantity > 128)
            {
                Console.WriteLine("Try again! t must be a number between 1 and 128.");
            }

            Console.WriteLine("\nWait for tasks to complete...\n");
        }

        static void PrintOne()
        {
            Console.WriteLine("1!");
        }

        static void PrintTwo()
        {
            Console.WriteLine("2!");
        }

        static void PrintThree()
        {
            Console.WriteLine("3!");
        }

        static void PrintFour()
        {
            Console.WriteLine("4!");
        }

        static void Main(string[] args)
        {
            uint threadsQuantity;

            InputAndChecks(out threadsQuantity);

            ThreadsPool threadsPool = new ThreadsPool(threadsQuantity);

            Action tasksDelegate = new Action(PrintOne);
            tasksDelegate += PrintTwo;
            tasksDelegate += PrintThree;
            tasksDelegate += PrintFour;
            tasksDelegate += PrintOne;
            tasksDelegate += PrintTwo;
            tasksDelegate += PrintThree;
            tasksDelegate += PrintFour;

            threadsPool.Enqueue(tasksDelegate);
            Thread.Sleep(50);
            threadsPool.Enqueue(tasksDelegate);

            threadsPool.Dispose();

            Console.ReadKey();
        }
    }
}