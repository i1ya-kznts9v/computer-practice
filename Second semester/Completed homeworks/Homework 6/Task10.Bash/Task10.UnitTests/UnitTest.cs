using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task10.Bash;

namespace Task10.UnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void BashTest()
        {
            Bash.Bash bash = new Bash.Bash("$x = 9 | eCHo");

            Assert.AreEqual("$x", bash.Commands[0].Name);
            Assert.AreEqual("9", bash.Commands[0].Input[1]);
            Assert.AreEqual("9", bash.Commands[0].Output[0]);

            Assert.AreEqual("echo", bash.Commands[1].Name);
            Assert.AreEqual("9", bash.Commands[1].Input[0]);
            Assert.AreEqual("9", bash.Commands[1].Output[0]);
        }

        [TestMethod]
        public void InterpreterTest()
        {
            List<ICommand> resultParse = Interpreter.Parse(" $x  =    2  |        echo|   pwd|wc|  cat  |     exit    ", new Dictionary<string, string>());

            Assert.AreEqual("$x", resultParse[0].Name);
            Assert.AreEqual("2", resultParse[0].Input[1]);
            Assert.AreEqual("echo", resultParse[1].Name);
            Assert.AreEqual("pwd", resultParse[2].Name);
            Assert.AreEqual("wc", resultParse[3].Name);
            Assert.AreEqual("cat", resultParse[4].Name);
            Assert.AreEqual("exit", resultParse[5].Name);

            Dictionary<string, string> variables = new Dictionary<string, string>();
            variables.Add("$x", "49");

            List<string> resultToArguments = Interpreter.ToArguments(new List<string> { "$x" }, variables);
            Assert.AreEqual("49", resultToArguments[0]);
        }

        [TestMethod]
        public void LocalVariableTest()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            LocalVariable localVariableFirst = new LocalVariable(new List<string> { "$x", "777" }, dictionary);
            LocalVariable localVariableSecond = new LocalVariable(new List<string> { "$y", "666" }, dictionary);

            localVariableFirst.Run();

            Assert.AreEqual("$x", localVariableFirst.Name);
            Assert.AreEqual("777", localVariableFirst.Input[1]);
            Assert.AreEqual("777", localVariableFirst.Output[0]);
            dictionary.TryGetValue("$x", out string valueFirst);
            Assert.AreEqual("777", valueFirst);

            localVariableSecond.Run();

            Assert.AreEqual("$y", localVariableSecond.Name);
            Assert.AreEqual("666", localVariableSecond.Input[1]);
            Assert.AreEqual("666", localVariableSecond.Output[0]);
            dictionary.TryGetValue("$y", out string valueSecond);
            Assert.AreEqual("666", valueSecond);
        }

        [TestMethod]
        public void SystemShellTest()
        {
            SystemShell systemShell = new SystemShell(new List<string> { "pathping", "nasa.com" });

            systemShell.Run();

            Assert.AreEqual("system shell", systemShell.Name);
            Assert.AreEqual("pathping", systemShell.Input[0]);
            Assert.AreEqual("nasa.com", systemShell.Input[1]);
        }

        [TestMethod]
        public void EchoTest()
        {
            Echo echo = new Echo(new List<string> { "AY!" });

            echo.Run();

            Assert.AreEqual("echo", echo.Name);
            Assert.AreEqual("AY!", echo.Input[0]);
            Assert.AreEqual("AY!", echo.Output[0]);
        }

        [TestMethod]
        public void ExitTest()
        {
            Exit exit = new Exit(new List<string>());

            Assert.AreEqual("exit", exit.Name);
            Assert.AreEqual(0, exit.Input.Count);
            Assert.AreEqual(null, exit.Output);
        }

        [TestMethod]
        public void PwdTest()
        {
            Pwd pwd = new Pwd(new List<string>());
            List<string> list = new List<string>();

            string directory = Directory.GetCurrentDirectory();
            string[] directoryFiles = Directory.GetFiles(directory);
            list.Add(directory);
            list.AddRange(directoryFiles);

            pwd.Run();

            Assert.AreEqual("pwd", pwd.Name);
            Assert.AreEqual(0, pwd.Input.Count);

            for(int i = 0; i < pwd.Output.Count; i++)
            {
                Assert.AreEqual(list[i], pwd.Output[i]);
            }
        }

        [TestMethod]
        public void CatTest()
        {
            string path = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\"), @"TestFile.txt");

            Cat cat = new Cat(new List<string> { path });
            string[] lines = File.ReadAllLines(path);

            cat.Run();

            Assert.AreEqual("cat", cat.Name);
            Assert.AreEqual(path, cat.Input[0]);

            for(int i = 0; i < cat.Output.Count; i++)
            {
                Assert.AreEqual(lines[i], cat.Output[i]);
            }
        }

        [TestMethod]
        public void WcTest()
        {
            string path = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\"), @"TestFile.txt");

            Wc wc = new Wc(new List<string> { path });
            int linesCounter = 0, wordsCounter = 0, bytesCounter = 0;

            string[] lines = File.ReadAllLines(path);
            linesCounter = lines.Length;

            foreach (var i in lines)
            {
                string[] words = i.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                wordsCounter += words.Length;
            }

            byte[] bytes = File.ReadAllBytes(path);
            bytesCounter = bytes.Length;

            wc.Run();

            Assert.AreEqual("wc", wc.Name);
            Assert.AreEqual(path, wc.Input[0]);
            Assert.AreEqual(linesCounter.ToString(), wc.Output[0]);
            Assert.AreEqual(wordsCounter.ToString(), wc.Output[1]);
            Assert.AreEqual(bytesCounter.ToString(), wc.Output[2]);
        }
    }
}