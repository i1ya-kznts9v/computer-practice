using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExamSystem.LockFreeListHashTables;
using ExamSystem.StripedHashTables;
using System.Threading;
using ExamSystem;

namespace ExamSystemUnitTests
{
    [TestClass]
    public class DataStructureTests
    {
        Random random = new Random();

        [TestMethod]
        public void LockFreeListHashTableTest()
        {
            LockFreeListHashTable lockFreeListHashTable = new LockFreeListHashTable(256);
            Thread[] threads = new Thread[32];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ParameterizedThreadStart(MakeRequests));
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start(lockFreeListHashTable);
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
        }

        [TestMethod]
        public void StripedHashTableTest()
        {
            StripedHashTable StripedHashTable = new StripedHashTable(256);
            Thread[] threads = new Thread[32];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ParameterizedThreadStart(MakeRequests));
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start(StripedHashTable);
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
        }

        private void MakeRequests(object parameter)
        {
            if (parameter as IExamSystem == null)
            {
                throw new ArgumentException("Invalid argument\n");
            }

            IExamSystem examSystem = (IExamSystem)parameter;

            for (int i = 0; i < 1024; i++)
            {
                int studentID = random.Next();
                int courseID = random.Next();

                examSystem.Add(studentID, courseID);
                Assert.IsTrue(examSystem.Contains(studentID, courseID));

                examSystem.Remove(studentID, courseID);
                Assert.IsFalse(examSystem.Contains(studentID, courseID));
            }
        }
    }
}