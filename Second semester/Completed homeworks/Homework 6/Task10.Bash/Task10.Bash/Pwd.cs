using System;
using System.Collections.Generic;
using System.IO;

namespace Task10.Bash
{
    public class Pwd : ICommand
    {
        public string Name { get; }
        public List<string> Input { get; set; }
        public List<string> Output { get; }

        public Pwd(List<string> input)
        {
            Name = "pwd";
            Input = input;
            Output = new List<string>();
        }

        public static void PrintUsingInformation()
        {
            Console.WriteLine("Use command \"pwd\" to display the current directory and all files in it");
        }

        public void Run()
        {
            if(Input.Count != 0)
            {
                throw new ArgumentException("Incorrect arguments in \"" + Name + "\"");
            }

            string directory = Directory.GetCurrentDirectory();
            string[] directoryFiles = Directory.GetFiles(directory);

            Console.WriteLine(directory);

            foreach (var i in directoryFiles)
            {
                Console.WriteLine(i);
            }

            Output.Add(directory);
            Output.AddRange(directoryFiles);
            Console.WriteLine();
        }
    }
}