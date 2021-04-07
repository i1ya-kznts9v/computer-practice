using System;
using System.IO;

namespace MPI
{
    public static class ArrayFileComparison
    {
        public static bool Compare(string file1, string file2)
        {
            if (!File.Exists(file1))
            {
                Console.WriteLine(string.Format("File {0} does not exist\n", file1));

                return (false);
            }

            if (!File.Exists(file2))
            {
                Console.WriteLine(string.Format("File {0} does not exist\n", file2));

                return (false);
            }

            string content1 = File.ReadAllText(file1);
            string content2 = File.ReadAllText(file2);

            if (content1.Length != content2.Length)
            {
                Console.WriteLine("File sizes are different\n");

                return (false);
            }

            for (int i = 0; i < content1.Length; i++)
            {
                if (content1[i] != content2[i])
                {
                    Console.WriteLine(string.Format("Files are different at position {0}\n", i));

                    return(false);
                }
            }

            Console.WriteLine("Files are the same!\n");

            return (true);
        }
    }
}