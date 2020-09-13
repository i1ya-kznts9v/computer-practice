using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Task10.Bash
{
    public class SystemShell : ICommand
    {
        public string Name { get; }
        public List<string> Input { get; set; }
        public List<string> Output { get; }

        public SystemShell(List<string> input)
        {
            Name = "system shell";
            Input = input;
        }

        public static void PrintUsingInformation()
        {
            Console.WriteLine("If the entered command is not recognized, the system shell will try to run it");
        }

        public void Run()
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(Input[0]);
                processStartInfo.UseShellExecute = false;

                if(Input.Count > 1)
                {
                    string arguments = Input[1];

                    if(Input.Count > 2)
                    {
                        for(int i = 2; i < Input.Count; i++)
                        {
                            arguments += " " + Input[i];
                        }
                    }

                    processStartInfo.Arguments = arguments;
                }

                Process.Start(processStartInfo);
            }
            catch
            {
                throw new Exception("Error: unable to run command");
            }
        }
    }
}