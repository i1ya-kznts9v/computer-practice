using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task6.Plugins;

namespace Task6.UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void ReflectionUnitTest()
        {
            string directory = Path.Combine(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")), @"Plugins\");
            PluginFDC pluginFDC = new PluginFDC(directory);
            List<object> objects = new List<object>();

            List<string> expected = new List<string>() {"Task6.ClassLibraryFive.GalaxyCalculator", // The order of the elements coincides with the order of the class
                                                        "Task6.ClassLibraryFour.UnrealCalculator", // libraries and the location of the classes inside them.
                                                        "Task6.ClassLibraryFour.RealCalculator",
                                                        "Task6.ClassLibraryOne.SimpleCalcucator",
                                                        "Task6.ClassLibraryOne.ProfessionalCalculator",
                                                        "Task6.ClassLibraryTwo.СhemicalСalculator",
                                                        "Task6.ClassLibraryTwo.IngeneerCalculator"};

            pluginFDC.CreateTypeInstance(objects);

            for (int i = 0; i < objects.Count; i++)
            {
                Assert.AreEqual(expected[i], objects[i].ToString());
                Console.WriteLine(objects[i].ToString());
            }
        }
    }
}