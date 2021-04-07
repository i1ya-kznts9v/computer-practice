using System;

namespace Task_4_Hash_Table
{
    class MainPart
    {
        static void Main(string[] args) // Visual test
        {
            HashTable<int, int> hashTable = new HashTable<int, int>();
            Random random = new Random();

            for (int listIndex = 0; listIndex < 201; listIndex++)
            {
                int key = random.Next(10000);
                int value = random.Next(10000);

                hashTable.Put(key, value);

                Console.WriteLine($"Info #{listIndex}. (Key: {key}, Value: {value})\n");
            }

            hashTable.OutputOnDisplay();

            Console.Write("Enter some key: ");
            int testingKey = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            hashTable.GetValue(testingKey);
            hashTable.DeleteValue(testingKey);

            Console.Write($"Press \"Enter\": ");
            Console.ReadKey();

            hashTable.OutputOnDisplay();

            Console.ReadKey();
        }
    }
}