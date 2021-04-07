using System;
using System.Collections.Generic;
using System.IO;

namespace Task10.Bash
{
    public class Cat : ICommand
    {
        public string Name { get; }
        public List<string> Input { get; set; }
        public List<string> Output { get; }

        public Cat(List<string> input)
        {
            Name = "cat";
            Input = MakePathWithSpaces(input);
            Output = new List<string>();
        }

        private List<string> MakePathWithSpaces(List<string> input)
        {
            List<string> inputNew = new List<string>();
            string temp = string.Empty;

            for (int i = 0; i < input.Count; i++)
            {
                temp = temp.Insert(temp.Length, input[i]);

                if(i != input.Count - 1)
                {
                    temp = temp.Insert(temp.Length, " ");
                }

                if (i < input.Count - 1 && input[i + 1].Contains(@":\") == true)
                {
                    inputNew.Add(temp);
                    temp = string.Empty;
                }
                
                if(i == input.Count - 1)
                {
                    inputNew.Add(temp);
                    return (inputNew);
                }
            }

            return (inputNew);
        }

        public static void PrintUsingInformation()
        {
            Console.WriteLine("Use comand \"cat [FILENAME]\" to display what the fail contains");
        }

        public void Run()
        {
            if(Input.Count == 0)
            {
                throw new ArgumentException("Incorrect arguments in \"" + Name + "\"");
            }

            foreach (var i in Input)
            {
                string[] lines;

                try
                {
                    lines = File.ReadAllLines(i);
                }
                catch
                {
                    throw new ArgumentException("Incorrect path to the file");
                }

                foreach(var j in lines)
                {
                    Console.WriteLine(j);
                }

                Output.AddRange(lines);
                Console.WriteLine();
            }
        }
    }
}