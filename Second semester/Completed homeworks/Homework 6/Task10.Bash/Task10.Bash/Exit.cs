using System;
using System.Collections.Generic;

namespace Task10.Bash
{
    public class Exit : ICommand
    {
        public string Name { get; }
        public List<string> Input { get; set; }
        public List<string> Output { get; }

        public Exit(List<string> input)
        {
            Name = "exit";
            Input = input;

            if (input.Count != 0)
            {
                throw new ArgumentException("Incorrect arguments in \"" + Name + "\"");
            }
        }

        public static void PrintUsingInformation()
        {
            Console.WriteLine("Use command \"exit\" to close the interpreter");
        }

        public void Run()
        {
            Environment.Exit(-1);
        }
    }
}