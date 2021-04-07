using System;
using System.Threading.Tasks;
using ExamSystem.LockFreeListHashTables;
using ExamSystem.StripedHashTables;

namespace ExamSystem
{
    class Program
    {
        static Random random = new Random();
        static IExamSystem examSystem;
        static Task[] tasks;

        static void Main(string[] args)
        {
            Console.WriteLine("Working time tests for ExamSystem using variously multithreaded" +
                " synchronized data structures started...\n");

            examSystem = new LockFreeListHashTable(256);
            TasksInitialize(65536);

            Console.Write($"ExamSystem work time using LockFreeListHashTable: ");
            double resultLFLHT = WorkingTimeTest();
            Console.WriteLine(resultLFLHT + " ms.\n");

            examSystem = new StripedHashTable(256);
            TasksInitialize(65536);

            Console.Write($"ExamSystem work time using StripedHashTable: ");
            double resultSHT = WorkingTimeTest();
            Console.WriteLine(resultSHT + " ms.\n");

            if (resultLFLHT > resultSHT)
            {
                Console.WriteLine($"ExamSystem using LockFreeListHashTable works {resultLFLHT - resultSHT} ms." +
                    $" faster then ExamSystem using StripedHashTable");
            }
            else if (resultLFLHT < resultSHT)
            {
                Console.WriteLine($"ExamSystem using StripedHashTable works {resultSHT - resultLFLHT} ms." +
                    $" faster then ExamSystem using LockFreeListHashTable");
            }
            else
            {
                Console.WriteLine($"ExamSystem using LockFreeListHashTable works the same time" +
                    $" with ExamSystem using StripedHashTable");
            }

            Console.ReadKey();
        }

        public static void TasksInitialize(int tasksCount)
        {
            tasks = new Task[tasksCount];

            for (int i = 0; i < tasks.Length; i++)
            {
                var choice = random.Next(100);

                /* 1% is remove */
                if (choice == 0)
                {
                    tasks[i] = new Task(() => examSystem.Remove(random.Next(), random.Next()));
                }
                /* 90% is contains */
                else if (choice > 10)
                {
                    tasks[i] = new Task(() => examSystem.Contains(random.Next(), random.Next()));
                }
                /* 9% is add */
                else
                {
                    tasks[i] = new Task(() => examSystem.Add(random.Next(), random.Next()));
                }
            }
        }

        public static double WorkingTimeTest()
        {
            var startTime = DateTime.Now;

            foreach (var task in tasks)
            {
                task.Start();
            }

            foreach (var task in tasks)
            {
                task.Wait();
            }

            var endTime = DateTime.Now;

            return (endTime - startTime).TotalMilliseconds;
        }
    }
}