using System;
using System.Collections.Generic;

namespace Task10.Bash
{
    public class LocalVariable : ICommand
    {
        public string Name { get; }
        public List<string> Input { get; set; }
        public List<string> Output { get; }
        public Dictionary<string, string> Variables { get; private set; }

        public LocalVariable(List<string> input, Dictionary<string, string> variables)
        {
            Name = input[0];
            Input = input;
            Variables = variables;
            Output = new List<string>();
        }

        public static void PrintUsingInformation()
        {
            Console.WriteLine("Use command \"$\" to assigment and using local variables");
        }

        public void Run()
        {
            if(Input.Count != 2)
            {
                throw new ArgumentException("Incorrect or lack of assigned value in local variable");
            }

            string[] variables = Input[0].Trim().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string value = Input[1].Trim();

            for(int i = 0; i < variables.Length; i++)
            {
                variables[i] = variables[i].Trim();

                if (variables[i][0] != '$')
                {
                    throw new ArgumentException("Incorrect \"" + Name + "\"");
                }

                try
                {
                    Variables.Add(variables[i], value);
                }
                catch(ArgumentException)
                {
                    Variables[variables[i]] = value;
                }
            }

            Output.Add(value);
        }
    }
}