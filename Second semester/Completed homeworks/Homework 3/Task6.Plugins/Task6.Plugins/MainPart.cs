using System;
using System.IO;
using System.Collections.Generic;

namespace Task6.Plugins
{
    class MainPart
    {
        static void Main(string[] args) // Visual test
        {
            string directory = Path.Combine(Directory.GetCurrentDirectory(), "Plugins\\");
            PluginFDC pluginFDC = new PluginFDC(directory);
            List<object> objects = new List<object>();

            pluginFDC.CreateTypeInstance(objects);

            for (int i = 0; i < objects.Count; i++)
            {
                Console.WriteLine(objects[i].ToString());
            }

            Console.ReadKey();
        }
    }
}
