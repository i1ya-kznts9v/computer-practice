using System;
using System.Threading;

namespace Task_5_Weak_References
{
    class MainPart
    {
        static void Main(string[] args)
        {
            HashTable<TestClass> hashTable = new HashTable<TestClass>();
            Random random = new Random();

            hashTable.Put(140, new TestClass("agp"));
            hashTable.Put(224, new TestClass("baf"));
            hashTable.Put(323, new TestClass("ccce"));
            hashTable.Put(411, new TestClass("dwax"));
            hashTable.Put(500, new TestClass("ewe"));
            hashTable.OutputOnDisplay();

            GC.Collect();

            hashTable.DeleteValue(224);
            Console.WriteLine(hashTable.GetValue(224));

            hashTable.DeleteValue(411);
            Console.WriteLine(hashTable.GetValue(411));

            hashTable.OutputOnDisplay();

            Console.WriteLine("Let's make a delay.\n\n");
            Thread.Sleep(2000);
            GC.Collect();
            hashTable.OutputOnDisplay();

            Console.ReadKey();
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
            return (str);
        }
    }
}
