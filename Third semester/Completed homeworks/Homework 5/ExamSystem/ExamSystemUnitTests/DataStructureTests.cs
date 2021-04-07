using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExamSystem.LockFreeListHashTables;
using ExamSystem.StripedHashTables;
using ExamSystem;
using System.Threading.Tasks;

namespace ExamSystemUnitTests
{
    [TestClass]
    public class DataStructureTests
    {
        [TestMethod]
        public void LockFreeListHashTableTest()
        {
            LockFreeListHashTable lockFreeListHashTable = new LockFreeListHashTable(256);
            Task[] tasks = new Task[32];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task(() => MakeRequests(lockFreeListHashTable));
            }

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i].Start();
            }

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i].Wait();
                tasks[i].Dispose();
            }
        }

        [TestMethod]
        public void StripedHashTableTest()
        {
            StripedHashTable stripedHashTable = new StripedHashTable(256);
            Task[] tasks = new Task[32];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task(() => MakeRequests(stripedHashTable));
            }

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i].Start();
            }

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i].Wait();
                tasks[i].Dispose();
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
                int studentID = Math.Abs(Guid.NewGuid().GetHashCode());
                int courseID = Math.Abs(Guid.NewGuid().GetHashCode());

                examSystem.Add(studentID, courseID);
                Assert.IsTrue(examSystem.Contains(studentID, courseID));

                examSystem.Remove(studentID, courseID);
                Assert.IsFalse(examSystem.Contains(studentID, courseID));
            }
        }
    }
}