using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task_5_Weak_References;

namespace Task_5_Weak_References_Unit_Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void HashTableTestForClassWithDeleting()
        {
            HashTable<TestClass> hashTable = new HashTable<TestClass>();
            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                int key = random.Next(10000);
                string value = Convert.ToString(random.Next(100));

                hashTable.Put(key, new TestClass(value));

                Assert.AreEqual(value, hashTable.GetValue(key).ToString());

                hashTable.DeleteValue(key);

                if (hashTable.GetValue(key) != null)
                {
                    throw new Exception("Deletion failed.");
                }
            }
        }

        [TestMethod]
        public void HashTableTestForClassWithGC()
        {
            HashTable<TestClass> hashTable = new HashTable<TestClass>();

            hashTable.Put(140, new TestClass("agp"));
            hashTable.Put(224, new TestClass("baf"));
            hashTable.Put(323, new TestClass("ccce"));
            hashTable.Put(411, new TestClass("dwax"));
            hashTable.Put(500, new TestClass("ewe"));
            hashTable.OutputOnDisplay();

            GC.Collect();

            Assert.AreEqual("dwax", hashTable.GetValue(411).ToString());

            hashTable.DeleteValue(224);
            Console.WriteLine(hashTable.GetValue(224));

            hashTable.DeleteValue(411);
            Console.WriteLine(hashTable.GetValue(411));

            hashTable.OutputOnDisplay();

            Console.WriteLine("Let's make a delay.\n");
            Thread.Sleep(2000);
            GC.Collect();
            hashTable.OutputOnDisplay();
        }
    }

    public class TestClass
    {
        public string str;

        public TestClass(string strt)
        {
            str = strt;
        }

        public override string ToString()
        {
            return(str);
        }
    }
}


