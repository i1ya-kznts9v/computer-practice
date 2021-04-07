using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MPI
{
    public static class ArrayFileGenerator
    {
        public static string[] Generate()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Random r = new Random(DateTime.Now.Millisecond);

            int capacity = 1000000;

            List<int> lst = new List<int>(capacity);

            for (int i = 0; i < capacity; i++)
            {
                lst.Add(r.Next(1000000));
            }

            string path = Path.Combine(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")), "Arrays");

            string path1 = path + @"\unsortedIntArray.dat";
            File.WriteAllText(path1, string.Join(" ", lst.Select(x => x.ToString())));

            lst.Sort();

            string path2 = path + @"\sortedByDefaultSortIntArray.dat";
            File.WriteAllText(path2, string.Join(" ", lst.Select(x => x.ToString())));

            string path3 = path + @"\sortedByEvenOddSortIntArray.dat";

            sw.Stop();

            return (new string[] {path1, path2, path3});
        }
    }
}