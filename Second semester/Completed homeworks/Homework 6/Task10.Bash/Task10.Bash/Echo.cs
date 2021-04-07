using System;
using System.Collections.Generic;

namespace Task10.Bash
{
    public class Echo : ICommand
    {
        public string Name { get; }
        public List<string> Input { get; set; }
        public List<string> Output { get; }

        public Echo(List<string> input)
        {
            Name = "echo";
            Input = input;
            Output = new List<string>();
        }

        public static void PrintUsingInformation()
        {
            Console.WriteLine("Use command \"echo\" to display the arguments");
        }

        public void Run()
        {
            foreach(var i in Input)
            {
                Console.Write($"{i} ");
            }

            Output.AddRange(Input);
            Console.WriteLine();
        }
    }
}