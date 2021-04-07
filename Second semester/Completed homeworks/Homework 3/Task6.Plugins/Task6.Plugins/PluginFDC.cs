using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Task6.Interface;

namespace Task6.Plugins
{
    public class PluginFDC
    {
        public PluginFDC(string directory)
        {
            FileInfo[] dllFiles = FindDLLFiles(new DirectoryInfo(directory));
            DownloadDLLFiles(dllFiles);
        }

        private FileInfo[] FindDLLFiles(DirectoryInfo directory)
        {
            FileInfo[] dllFiles = directory.GetFiles("*.dll");

            return (dllFiles);
        }

        private List<Type[]> typesInDLLFiles = new List<Type[]>();

        private void DownloadDLLFiles(FileInfo[] dllFiles)
        {
            List<Assembly> assemblyFromDLLFiles = new List<Assembly>();
            int k = 0;

            for (int i = 0; i < dllFiles.Length; i++)
            {
                assemblyFromDLLFiles.Add(Assembly.LoadFrom(dllFiles[i].FullName));
                typesInDLLFiles.Add(assemblyFromDLLFiles[k].GetTypes());

                bool contains = false;

                for (int j = 0; j < typesInDLLFiles[k].Length; j++)
                {
                    if (typeof(ICalculator).IsAssignableFrom(typesInDLLFiles[k][j]))
                    {
                        contains = true;
                    }
                    else
                    {
                        typesInDLLFiles[k][j] = null;
                    }
                }

                if (contains == false)
                {
                    assemblyFromDLLFiles.RemoveAt(k);
                    typesInDLLFiles.RemoveAt(k);
                    k--;
                }

                k++;
            }
        }

        public List<object> CreateTypeInstance(List<object> objects)
        {
            for (int i = 0; i < typesInDLLFiles.Count; i++)
            {
                for (int j = 0; j < typesInDLLFiles[i].Length; j++)
                {
                    if (typesInDLLFiles[i][j] != null)
                    {
                        objects.Add(Activator.CreateInstance(typesInDLLFiles[i][j]));
                    }
                }
            }

            return (objects);
        }
    }
}