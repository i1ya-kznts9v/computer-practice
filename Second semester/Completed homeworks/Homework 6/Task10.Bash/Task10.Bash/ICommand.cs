using System.Collections.Generic;

namespace Task10.Bash
{
    public interface ICommand
    {
        string Name { get; }
        List<string> Input { get; set; }
        List<string> Output { get; }

        void Run();
    }
}