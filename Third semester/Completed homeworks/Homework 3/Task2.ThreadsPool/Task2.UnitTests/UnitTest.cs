using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Task2.UnitTests
{
    [TestClass]
    public class UnitTest
    {
        int[] testArray = new int[8];

        void TestMethodOne()
        {
            testArray[0] = 1;
            testArray[0] *= 5;
        }

        void TestMethodTwo()
        {
            testArray[1] = 2;
            testArray[1] *= 5;
        }

        void TestMethodThree()
        {
            testArray[2] = 3;
            testArray[2] *= 5;
        }

        void TestMethodFour()
        {
            testArray[3] = 4;
            testArray[3] *= 5;
        }

        void TestMethodFive()
        {
            testArray[4] = 5;
            testArray[4] *= 5;
        }

        void TestMethodSix()
        {
            testArray[5] = 6;
            testArray[5] *= 5;
        }

        void TestMethodSeven()
        {
            testArray[6] = 7;
            testArray[6] *= 5;
        }

        void TestMethodEight()
        {
            testArray[7] = 8;
            testArray[7] *= 5;
        }

        [TestMethod]
        public void ThreadsPoolTest()
        {
            uint threadsQuantity = 16;
            ThreadsPool.ThreadsPool threadsPool = new ThreadsPool.ThreadsPool(threadsQuantity);

            Action tasksDelegateOne = new Action(TestMethodOne);
            tasksDelegateOne += TestMethodTwo;
            tasksDelegateOne += TestMethodThree;
            tasksDelegateOne += TestMethodFour;

            Action tasksDelegateTwo = new Action(TestMethodFive);
            tasksDelegateTwo += TestMethodSix;
            tasksDelegateTwo += TestMethodSeven;
            tasksDelegateTwo += TestMethodEight;

            Thread.Sleep(100);

            Assert.AreEqual(threadsQuantity, (uint)threadsPool.GetThreadsCount());

            threadsPool.Enqueue(tasksDelegateOne);
            Thread.Sleep(50);
            threadsPool.Enqueue(tasksDelegateTwo);

            Thread.Sleep(100);

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual((i + 1) * 5, testArray[i]);
            }

            for (int i = 4; i < 8; i++)
            {
                Assert.AreEqual((i + 1) * 5, testArray[i]);
            }

            threadsPool.Dispose();
        }
    }
}