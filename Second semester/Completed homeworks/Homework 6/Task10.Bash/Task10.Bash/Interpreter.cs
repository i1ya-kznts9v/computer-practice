using System;
using System.Collections.Generic;
using System.Linq;

namespace Task10.Bash
{
    public static class Interpreter
    {
        public static List<string> ToArguments(List<string> input, Dictionary<string, string> variables)
        {
            List<string> arguments = new List<string>();

            for(int i = 0; i < input.Count; i++)
            {
                string[] words = input[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for(int j = 0; j < words.Length; j++)
                {
                    if(words[j].Trim()[0] == '$')
                    {
                        if(variables.TryGetValue(words[j].Trim(), out words[j]))
                        {

                        }
                        else
                        {
                            throw new ArgumentException("Incorrect argument \"" + input[i] + "\"");
                        }
                    }
                }

                arguments.AddRange(words);
            }

            return (arguments);
        }

        private static ICommand NewCommand(string input, Dictionary<string, string> variables, bool isFirst)
        {
            string[] words = input.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if((isFirst == false) && (words.Length != 1))
            {
                throw new ArgumentException("Incorrect arguments in \"" + input.Trim() + "\"");
            }

            List<string> arguments = new List<string>();
            string command = words[0].ToLower();

            for(int i = 1; i < words.Length; i++)
            {
                arguments.Add(words[i]);
            }

            if(arguments.Count > 0)
            {
                arguments = ToArguments(arguments, variables);
            }

            switch(command)
            {
                case "exit":
                    {
                        Exit exit = new Exit(arguments);

                        return (exit);
                    }
                case "echo":
                    {
                        Echo echo = new Echo(arguments);

                        return (echo);
                    }
                case "pwd":
                    {
                        Pwd pwd = new Pwd(arguments);

                        return (pwd);
                    }
                case "cat":
                    {
                        Cat cat = new Cat(arguments);

                        return (cat);
                    }
                case "wc":
                    {
                        Wc wc = new Wc(arguments);

                        return (wc);
                    }
                default:
                    {
                        SystemShell processStart = new SystemShell(words.ToList());

                        return (processStart);
                    }
            }
        }

        private static LocalVariable NewVariable(string input, Dictionary<string, string> variables, bool isFirst)
        {
            if(input.Trim()[0] != '$')
            {
                throw new ArgumentException("Incorrect \"" + input.Trim() + "\"");
            }

            string[] words = input.Trim().Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

            if ((isFirst == true) && (words.Length == 2))
            {
                return (new LocalVariable(new List<string> { words[0].Trim(), words[1].Trim() }, variables));
            }
            else if((isFirst == false) && (words.Length == 1))
            {
                return (new LocalVariable(new List<string> { words[0].Trim() }, variables));
            }
            else
            {
                throw new ArgumentException("Incorrect \"" + input.Trim() + "\"$");
            }
        }

        public static List<ICommand> Parse(string input, Dictionary<string, string> variables)
        {
            List<ICommand> result = new List<ICommand>();
            string[] commands = input.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            if(commands[0].Contains('=') == true)
            {
                result.Add(NewVariable(commands[0], variables, true));
            }
            else
            {
                ICommand newCommand = NewCommand(commands[0], variables, true);

                if (newCommand != null)
                {
                    result.Add(newCommand);
                }
            }

            try
            {
                for(int i = 1; i < commands.Length; i++)
                {
                    if(commands[i].Contains('=') == true)
                    {
                        result.Add(NewVariable(commands[i], variables, false));
                    }
                    else
                    {
                        ICommand newCommand = NewCommand(commands[i], variables, false);

                        if(newCommand != null)
                        {
                            result.Add(newCommand);
                        }
                    }
                }
            }
            catch(ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);

                return (null);
            }

            return(result);
        }
    }
}