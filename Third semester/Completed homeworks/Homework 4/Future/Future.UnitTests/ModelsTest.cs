using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Future.UnitTests
{
    [TestClass]
    public class ModelsTest
    {
        int[] testArray;
        double result;

        static int[] Generate(int count)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Random r = new Random(DateTime.Now.Millisecond);

            List<int> lst = new List<int>(count);

            for (int i = 0; i < count; i++)
            {
                lst.Add(r.Next(1000));
            }

            sw.Stop();

            return lst.ToArray();
        }

        [TestInitialize]
        public void Initialize()
        {
            testArray = Generate(1000);

            result = Math.Sqrt(testArray.Sum(x => x * x));
        }

        [TestMethod]
        public void CascadeTest()
        {
            Cascade cascade = new Cascade();

            double actual = cascade.ComputeLength(testArray);

            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void ModifiedCascadeTest()
        {
            ModifiedCascade modifiedCascade = new ModifiedCascade();

            double actual = modifiedCascade.ComputeLength(testArray);

            Assert.AreEqual(result, actual);
        }
    }
}