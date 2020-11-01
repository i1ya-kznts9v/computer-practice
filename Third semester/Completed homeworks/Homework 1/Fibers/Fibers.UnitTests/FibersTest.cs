using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fibers.UnitTests
{
    [TestClass]
    public class FibersTest
    {
        int processes = 5;

        [TestInitialize]
        public void Initialize()
        {
            for (int i = 0; i < processes; i++)
            {
                Process process = new Process();
                ProcessManager.Add(process);
            }
        }

        [TestMethod]
        public void FifoTest()
        {
            Assert.AreEqual(processes, ProcessManager.GetFibersCount());

            ProcessManager.Run(Policy.Fifo);

            Assert.AreEqual(0, ProcessManager.GetFibersCount());
        }

        [TestMethod]
        public void PriorityTest()
        {
            Assert.AreEqual(processes, ProcessManager.GetFibersCount());

            ProcessManager.Run(Policy.Priority);

            Assert.AreEqual(0, ProcessManager.GetFibersCount());
        }
    }
}