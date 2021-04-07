using System;
using System.Collections.Generic;

namespace Task_4_Hash_Table
{
    public class HashTable<S, T>
    {
        private static uint arrayMinSize = 100; // Recommended value
        private uint arraySize;

        private static double arrayMinLoad = 0.72; // Recommended value
        private double arrayLoad;

        private static uint arrayMinMultiplier = 2; // Recommended value
        private uint arrayMultiplier;

        private uint hashTableLimit;

        public HashTable() : this(arrayMinSize, arrayMinLoad, arrayMinMultiplier)
        {

        }

        public HashTable(uint arraySize) : this(arraySize, arrayMinLoad, arrayMinMultiplier)
        {

        }

        public HashTable(uint arraySize, double arrayLoad) : this(arraySize, arrayLoad, arrayMinMultiplier)
        {

        }

        public HashTable(uint arraySize, double arrayLoad, uint arrayMultiplier)
        {
            if (arraySize < arrayMinSize)
            {
                this.arraySize = arrayMinSize;

                Console.WriteLine("The number of array elements you entered is too small.\n" +
                    $"This parameter has been changed to the default value ({arrayMinSize}).\n");
            }
            else
            {
                this.arraySize = arraySize;
            }

            if (arrayLoad < arrayMinLoad || arrayLoad > 1)
            {
                this.arrayLoad = arrayMinLoad;

                Console.WriteLine("The percentage of load of the array you entered is not optimal, because it is too large or too small.\n" +
                    $"This parameter will be changed to the default value({(uint)(arrayMinLoad * 100)}).\n");
            }
            else
            {
                this.arrayLoad = arrayLoad;
            }

            if (arrayMultiplier < arrayMinMultiplier)
            {
                this.arrayMultiplier = arrayMinMultiplier;

                Console.WriteLine("The value you entered, how many times the length of the array will increase during rebalancing, is not optimal, because it is too small.\n" +
                    $"This parameter will be changed to the default value({arrayMinMultiplier}).\n");
            }
            else
            {
                this.arrayMultiplier = arrayMultiplier;
            }

            hashTableLimit = (uint)(this.arraySize * this.arrayLoad);

            // Create
            hashTable = new List<Info>[arraySize];

            for (uint arrayIndex = 0; arrayIndex < arraySize; arrayIndex++)
            {
                hashTable[arrayIndex] = new List<Info>();
            }
        }

        private class Info
        {
            public S key;
            public T value;

            public Info(S key, T value)
            {
                this.key = key;
                this.value = value;
            }
        }

        private uint hashTableSize = 0; // Number of hash table elements at all
        private List<Info>[] hashTable;

        public void Put(S key, T value)
        {
            Info info = new Info(key, value);

            Put(info);
        }

        private int HashFunction(S keyOpen)
        {
            int key = keyOpen.GetHashCode();

            key = ((key >> 16) ^ key) * 0x45d9f3b;
            key = ((key >> 16) ^ key) * 0x45d9f3b;
            key = (key >> 16) ^ key;

            return (key);
        }

        private void Put(Info info)
        {
            uint arrayIndex = (uint)HashFunction(info.key) % arraySize;

            if (hashTableSize < hashTableLimit)
            {
                if (hashTable[arrayIndex].Count == 0)
                {
                    hashTable[arrayIndex].Add(info);
                }
                else
                {
                    for (int listIndex = 0; listIndex < hashTable[arrayIndex].Count; listIndex++)
                    {
                        if (hashTable[arrayIndex][listIndex].key.Equals(info.key))
                        {
                            Console.WriteLine($"The key {info.key} repeated. The old value {hashTable[arrayIndex][listIndex].value} is\nreplaced by the new value {info.value}.\n");

                            hashTable[arrayIndex].Insert(listIndex, info);

                            return;
                        }
                    }

                    hashTable[arrayIndex].Add(info);
                }
            }
            else
            {
                Rebalance();
                Put(info);
            }

            hashTableSize++;
        }

        private void Rebalance()
        {
            uint arraySizeOld = arraySize;

            arraySize *= arrayMultiplier;
            hashTableLimit = (uint)(arraySize * arrayLoad);

            Array.Resize(ref hashTable, (int)arraySize);

            for (uint arrayIndex = arraySizeOld; arrayIndex < arraySize; arrayIndex++)
            {
                hashTable[arrayIndex] = new List<Info>();
            }
        }

        private Info FindListElement(S key, uint arrayIndex)
        {
            if (hashTable[arrayIndex].Count != 0)
            {
                for (int listIndex = 0; listIndex < hashTable[arrayIndex].Count; listIndex++)
                {
                    if (hashTable[arrayIndex][listIndex].key.Equals(key))
                    {
                        return (hashTable[arrayIndex][listIndex]);
                    }
                }
            }

            return (null);
        }

        public T GetValue(S key)
        {
            uint arrayIndex = (uint)HashFunction(key) % arraySize;
            Info listElement = FindListElement(key, arrayIndex);

            if(listElement == null)
            {
                Console.WriteLine($"No value found for the key {key}.\n");

                return (default);
            }

            Console.WriteLine($"Search of {listElement.value} by the key {key} was successfully completed.\n");

            return (listElement.value);
        }

        public void DeleteValue(S key)
        {
            uint arrayIndex = (uint)HashFunction(key) % arraySize;
            Info listElement = FindListElement(key, arrayIndex);

            if (listElement == null)
            {
                Console.WriteLine($"No value to remove for the key {key}.\n");

                return;
            }

            Console.WriteLine($"Deleting value {listElement.value} by the key {key} was successfully completed.\n");

            hashTable[arrayIndex].Remove(listElement);
            hashTableSize--;
        }

        public void OutputOnDisplay()
        {
            for (uint arrayIndex = 0; arrayIndex < arraySize; arrayIndex++)
            {
                Console.WriteLine($"Array element #{arrayIndex} --------------------------------------------\n\n");

                for (int listIndex = 0; listIndex < hashTable[arrayIndex].Count; listIndex++)
                {
                    Console.WriteLine($"List element #{listIndex} -------------------------------\n");
                    Console.WriteLine($"(Key: {hashTable[arrayIndex][listIndex].key}, Value: {hashTable[arrayIndex][listIndex].value})\n\n");
                }
            }
        }
    }
}