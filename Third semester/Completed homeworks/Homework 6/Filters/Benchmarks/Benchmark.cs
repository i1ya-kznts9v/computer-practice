using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Benchmarks
{
    // 60 seconds - a service failure time

    class Benchmark
    {
        static void Main(string[] args)
        {
            Bitmap image = null;

            bool correct = false;
            string pathIn = null;

            while(!correct)
            {
                Console.Write("Enter the path to the image: ");
                pathIn = Console.ReadLine();

                try
                {
                    pathIn = Path.GetFullPath(pathIn);
                    image = new Bitmap(pathIn);

                    correct = true;
                }
                catch (Exception exception)
                {
                    correct = false;

                    Console.WriteLine(exception.Message);
                }
            }

            int requestsPerSec = 0;

            Console.Write("Enter the number of requests per second: ");
            while (!int.TryParse(Console.ReadLine(), out requestsPerSec) || requestsPerSec <= 0)
            {
                Console.WriteLine("It must be a integer > 0");
            }

            int seconds = 0;

            Console.Write("Enter measurement time in seconds: ");
            while (!int.TryParse(Console.ReadLine(), out seconds) || seconds <= 0)
            {
                Console.WriteLine("It must be a integer > 0");
            }

            correct = false;
            string pathOut = null;

            while (!correct)
            {
                Console.Write("Enter the path to the results of benchmarks: ");
                pathOut = Console.ReadLine();

                try
                {
                    pathOut = Path.GetFullPath(pathOut);

                    correct = true;
                }
                catch (Exception exception)
                {
                    correct = false;

                    Console.WriteLine(exception.Message);
                }
            }

            byte[] bytes = null;

            using(MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                bytes = memoryStream.GetBuffer();
            }
            image = null;

            List<Task<int>> tasks = new List<Task<int>>();

            for(int s = 0; s < seconds; s++)
            {
                for(int r = 0; r < requestsPerSec; r++)
                {
                    Task<int> task = new Task<int>(new Tester(bytes).GetTime);

                    tasks.Add(task);
                    task.Start();
                }
                
                // Per second
                Thread.Sleep(1000);
            }

            List<int> results = new List<int>();

            foreach(var task in tasks)
            {
                task.Wait();

                results.Add(task.Result);
            }

            if(results.Contains(-1))
            {
                Console.WriteLine($"At load {requestsPerSec} req/sec, there was a service failure caused by waiting more than 60 seconds");

                return;
            }

            StreamWriter streamWriter = new StreamWriter(pathOut);

            try
            {
                foreach(var result in results)
                {
                    streamWriter.WriteLine(result == 0 ? 0 : (double) result / 1000);
                }
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);

                return;
            }

            results.Sort();

            int averageTime = (int)results.Average();
            int medianTime = results[results.Count / 2];

            Console.WriteLine($"Average time: {(double) averageTime / 1000} seconds");
            streamWriter.WriteLine($"Average time: {(double) averageTime / 1000}");
            Console.WriteLine($"Median time: {(double) medianTime / 1000} seconds");
            streamWriter.WriteLine($"Median time: {(double) medianTime / 1000}");

            streamWriter.Dispose();
        }
    }
}