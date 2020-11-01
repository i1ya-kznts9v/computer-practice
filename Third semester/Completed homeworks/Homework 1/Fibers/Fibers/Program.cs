using System;

namespace Fibers
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < 10; i++)
            {
                Process process = new Process();
                ProcessManager.Add(process);
            }

            ProcessManager.Run(Policy.Priority);

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}