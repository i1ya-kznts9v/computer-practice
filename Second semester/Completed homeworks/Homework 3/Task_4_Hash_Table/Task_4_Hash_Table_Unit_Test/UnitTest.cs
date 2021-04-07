using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task_4_Hash_Table;

namespace Task_4_Hash_Table_Unit_Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void HashTableTestForInt()
        {
            HashTable<int, int> hashTable = new HashTable<int, int>();
            Random random = new Random();

            for (int element = 0; element < 100; element++)
            {
                int key = random.Next(10000);
                int value = random.Next(10000);

                hashTable.Put(key, value);
                Assert.AreEqual(value, hashTable.GetValue(key));

                hashTable.DeleteValue(key);

                if(hashTable.GetValue(key) != default)
                {
                    throw new Exception("Deletion failed.");
                }
            }
        }

        [TestMethod]
        public void HashTableTestForChar()
        {
            HashTable<byte, char> hashTable = new HashTable<byte, char>();
            Random random = new Random();

            for (int element = 0; element < 100; element++)
            {
                byte key = Convert.ToByte(random.Next(100));
                char value = (char)random.Next(100);

                hashTable.Put(key, value);
                Assert.AreEqual(value, hashTable.GetValue(key));

                hashTable.DeleteValue(key);

                if (hashTable.GetValue(key) != default)
                {
                    throw new Exception("Deletion failed.");
                }
            }
        }

        [TestMethod]
        public void HashTableTestForString()
        {
            HashTable<int, string> hashTable = new HashTable<int, string>();
            Random random = new Random();

            for (int element = 0; element < 100; element++)
            {
                int key = random.Next(10000);
                string value = GetRandomString(random);

                hashTable.Put(key, value);
                Assert.AreEqual(value, hashTable.GetValue(key));

                hashTable.DeleteValue(key);

                if (hashTable.GetValue(key) != default)
                {
                    throw new Exception("Deletion failed.");
                }
            }
        }

        private string GetRandomString(Random random) // Trash line
        {
            int length = random.Next(100);
            char[] str = new char[length];

            for (int i = 0; i < length; i++)
            {
                str[i] = Convert.ToChar(random.Next(length));
            }

            return(new string(str));
        }
    }
}
