using System;
using System.Collections.Generic;
using System.IO;

namespace Task10.Bash
{
    public class Wc : ICommand
    {
        public string Name { get; }
        public List<string> Input { get; set; }
        public List<string> Output { get; }

        public Wc(List<string> input)
        {
            Name = "wc";
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

                if (i != input.Count - 1)
                {
                    temp = temp.Insert(temp.Length, " ");
                }

                if (i < input.Count - 1 && input[i + 1].Contains(@":\") == true)
                {
                    inputNew.Add(temp);
                    temp = string.Empty;
                }

                if (i == input.Count - 1)
                {
                    inputNew.Add(temp);
                    return (inputNew);
                }
            }

            return (inputNew);
        }

        public static void PrintUsingInformation()
        {
            Console.WriteLine("Use command \"wc [FILENAME]\" to display the number of lines, words and bytes");
        }

        public void Run()
        {
            if(Input.Count == 0)
            {
                throw new ArgumentException("Incorrect arguments in \"" + Name + "\"");
            }

            foreach(var i in Input)
            {
                int linesCounter = 0, wordsCounter = 0, bytesCounter = 0;

                try
                {
                    string[] lines = File.ReadAllLines(i);
                    linesCounter = lines.Length;

                    foreach (var j in lines)
                    {
                        string[] words = j.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        wordsCounter += words.Length;
                    }

                    byte[] bytes = File.ReadAllBytes(i);
                    bytesCounter = bytes.Length;
                }
                catch
                {
                    throw new ArgumentException("Incorrect path to the file");
                }

                Console.WriteLine($"Number of lines: {linesCounter}");
                Console.WriteLine($"Number of words: {wordsCounter}");
                Console.WriteLine($"Number of bytes: {bytesCounter}");
                Output.Add(linesCounter.ToString());
                Output.Add(wordsCounter.ToString());
                Output.Add(bytesCounter.ToString());
                Console.WriteLine();
            }
        }
    }
}