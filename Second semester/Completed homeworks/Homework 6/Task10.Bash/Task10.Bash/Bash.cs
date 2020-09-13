using System;
using System.Collections.Generic;

namespace Task10.Bash
{
    public class Bash
    {
        public List<ICommand> Commands { get; private set; }
        private Dictionary<string, string> variables = new Dictionary<string, string>();

        private void PrintUsingInformation()
        {
            Exit.PrintUsingInformation();
            Echo.PrintUsingInformation();
            Pwd.PrintUsingInformation();
            Cat.PrintUsingInformation();
            Wc.PrintUsingInformation();
            LocalVariable.PrintUsingInformation();
            Console.WriteLine("Use \"|\" for command pipelining");
            SystemShell.PrintUsingInformation();
            Console.WriteLine();
        }

        private void RunCommands(List<ICommand> Commands)
        {
            Commands[0].Run();
            List<string> pipeliningOutput = Commands[0].Output;

            for(int i = 1; i < Commands.Count; i++)
            {
                Commands[i].Input = Interpreter.ToArguments(pipeliningOutput, variables);
                Commands[i].Run();
                pipeliningOutput = Commands[i].Output;
            }
        }

        public Bash()
        {
            PrintUsingInformation();

            while(true)
            {
                Commands = null;

                try
                {
                    string input = Console.ReadLine();
                    Commands = Interpreter.Parse(input, variables);
                }
                catch(ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                    Commands = null;
                }
                catch(Exception)
                {
                    Commands = null;
                }

                if(Commands != null)
                {
                    try
                    {
                        RunCommands(Commands);
                    }
                    catch(Exception exection)
                    {
                        Console.WriteLine(exection.Message);
                    }
                }
            }
        }

        public Bash(string input)
        {
            PrintUsingInformation();

            Commands = null;

            try
            {
                Commands = Interpreter.Parse(input, variables);
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
                Commands = null;
            }
            catch (Exception)
            {
                Commands = null;
            }

            if (Commands != null)
            {
                try
                {
                    RunCommands(Commands);
                }
                catch (Exception exection)
                {
                    Console.WriteLine(exection.Message);
                }
            }
        }
    }
}