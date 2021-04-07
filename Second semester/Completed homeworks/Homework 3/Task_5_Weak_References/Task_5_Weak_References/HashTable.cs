using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task_5_Weak_References
{
    public class HashTable<T> where T: class
    {
        private static int hashTableMinTime = 1000;
        private int hashTableTime;

        private static uint arrayMinSize = 100; // Recommended value
        private uint arraySize;

        private static double arrayMinLoad = 0.72; // Recommended value
        private double arrayLoad;

        private static uint arrayMinMultiplier = 2; // Recommended value
        private uint arrayMultiplier;

        private uint hashTableLimit;

        public HashTable() : this(arrayMinSize, arrayMinLoad, arrayMinMultiplier, hashTableMinTime)
        {
        }

        public HashTable(uint arraySize) : this(arraySize, arrayMinLoad, arrayMinMultiplier, hashTableMinTime)
        {
        }

        public HashTable(uint arraySize, double arrayLoad) : this(arraySize, arrayLoad, arrayMinMultiplier, hashTableMinTime)
        {
        }

        public HashTable(uint arraySize, double arrayLoad, uint arrayMultiplier) : this(arraySize, arrayLoad, arrayMultiplier, hashTableMinTime)
        {
        }

        public HashTable(uint arraySize, double arrayLoad, uint arrayMultiplier, int hashTableTime)
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

            this.hashTableTime = hashTableTime;

            // Create
            hashTable = new List<Info>[arraySize];

            for (uint arrayIndex = 0; arrayIndex < arraySize; arrayIndex++)
            {
                hashTable[arrayIndex] = new List<Info>();
            }
        }

        private class Info
        {
            public int key;
            public WeakReference<T> value;

            public Info(int key, T value)
            {
                this.key = key;
                this.value = new WeakReference<T>(value);
            }

            public bool Existence()
            {
                return (value.TryGetTarget(out T target));
            }

            public T GetValue()
            {
                value.TryGetTarget(out T target);

                return(target);
            }

            public void SetValue(T newValue)
            {
                value.SetTarget(newValue);
            }
        }

        private uint hashTableSize = 0; // Number of hash table elements at all
        private List<Info>[] hashTable;

        async public void Put(int key, T value)
        {
            Info info = new Info(key, value);

            Put(info);

            await Task.Delay(hashTableTime);
        }

        private int HashFunction(int key)
        {
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
                        if (hashTable[arrayIndex][listIndex].key == info.key)
                        {
                            Console.WriteLine($"The key {info.key} repeated. The old value {hashTable[arrayIndex][listIndex].GetValue()} is\nreplaced by the new value {info.GetValue()}.\n");

                            hashTable[arrayIndex][listIndex].SetValue(info.GetValue());

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

        private Info FindListElement(int key, uint arrayIndex)
        {
            if (hashTable[arrayIndex].Count != 0)
            {
                for (int listIndex = 0; listIndex < hashTable[arrayIndex].Count; listIndex++)
                {
                    if (hashTable[arrayIndex][listIndex].key == key && hashTable[arrayIndex][listIndex].Existence() == true)
                    {
                        return (hashTable[arrayIndex][listIndex]);
                    }
                }
            }

            return (null);
        }

        public T GetValue(int key)
        {
            uint arrayIndex = (uint)HashFunction(key) % arraySize;
            Info listElement = FindListElement(key, arrayIndex);

            if (listElement == null)
            {
                Console.WriteLine($"No value found for the key {key}.\n");

                return (default);
            }

            Console.WriteLine($"Search of {listElement.GetValue()} by the key {key} was successfully completed.\n");

            return (listElement.GetValue());
        }

        public void DeleteValue(int key)
        {
            uint arrayIndex = (uint)HashFunction(key) % arraySize;
            Info listElement = FindListElement(key, arrayIndex);

            if (listElement == null)
            {
                Console.WriteLine($"No value to remove for the key {key}.\n");

                return;
            }

            Console.WriteLine($"Deleting value {listElement.GetValue()} by the key {key} was successfully completed.\n");

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
                    Console.WriteLine($"(Key: {hashTable[arrayIndex][listIndex].key}, Value: {hashTable[arrayIndex][listIndex].GetValue()})\n\n");
                }
            }
        }
    }
}