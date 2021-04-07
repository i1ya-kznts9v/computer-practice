using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using static MPI.ArrayFileGenerator;
using static MPI.ArrayFileComparison;

namespace MPI.UnitTests
{
    [TestClass]
    public class EvenOddSortTest
    {
        int processors = 11;
        string[] paths;

        [TestInitialize]
        public void Initialize()
        {
            paths = Generate();

            var command = $"mpiexec -n {processors} MPI.exe \"{paths[0]}\" \"{paths[2]}\" Increase";

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine($"cd {Path.Combine(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\")), @"MPI\bin\Debug\")}");
            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
        }

        [TestMethod]
        public void Test()
        {
            Assert.IsTrue(Compare(paths[1], paths[2]));
        }
    }
}