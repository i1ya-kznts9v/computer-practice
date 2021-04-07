using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace ExamSystem.StripedHashTables
{
    public class StripedHashTable : IExamSystem
    {
        List<StripedNode>[] hashTable;
        uint capacity;
        int elements;

        Mutex[] lockers;
        uint lockability;

        bool RequiredResize
        {
            get
            {
                return elements / capacity >= 4;
            }
        }

        public StripedHashTable(uint capacity)
        {
            try
            {
                this.capacity = Check(capacity);
            }
            catch
            {
                this.capacity = 256;
            }
            elements = 0;
            lockability = this.capacity;

            hashTable = new List<StripedNode>[this.capacity];
            lockers = new Mutex[this.capacity];
            
            for (int i = 0; i < this.capacity; i++)
            {
                hashTable[i] = new List<StripedNode>();
                lockers[i] = new Mutex();
            }
        }

        uint Check(uint capacity)
        {
            if (capacity < 256)
            {
                throw new ArgumentException("Too small capacity\n");
            }

            return capacity;
        }

        long ComputeHash(long studentID, uint mod)
        {
            studentID ^= (studentID << 13);
            studentID ^= (studentID >> 17);
            studentID ^= (studentID << 5);

            return studentID % mod;
        }

        void Acquire(long hash)
        {
            lockers[hash].WaitOne();
        }

        void Release(long hash)
        {
            lockers[hash].ReleaseMutex();
        }

        public void Add(long studentId, long courseId)
        {
            Acquire(ComputeHash(studentId, lockability));

            try
            {
                long hash = ComputeHash(studentId, capacity);

                if (!hashTable[hash].Any(x => x.StudentID == studentId && x.CourseID == courseId))
                {
                    StripedNode node = new StripedNode(studentId, courseId);

                    hashTable[hash].Add(node);
                    Interlocked.Increment(ref elements);
                }
            }
            finally
            {
                Release(ComputeHash(studentId, lockability));
            }

            if (RequiredResize)
            {
                Resize();
            }
        }

        void Resize()
        {
            uint oldCapacity = capacity;

            foreach(var locker in lockers)
            {
                locker.WaitOne();
            }

            try
            {
                /* If somebody has done resize before */
                if (oldCapacity != capacity)
                {
                    return;
                }

                List<StripedNode>[] oldHashTable = hashTable;

                capacity = 2 * oldCapacity;
                hashTable = new List<StripedNode>[capacity];

                for(int i = 0; i < capacity; i++)
                {
                    hashTable[i] = new List<StripedNode>();
                }

                foreach(var list in oldHashTable)
                {
                    foreach(var node in list)
                    {
                        long hash = ComputeHash(node.StudentID, capacity);

                        hashTable[hash].Add(node);
                    }
                }
            }
            finally
            {
                foreach (var locker in lockers)
                {
                    locker.ReleaseMutex();
                }
            }
        }

        public bool Contains(long studentId, long courseId)
        {
            Acquire(ComputeHash(studentId, lockability));

            try
            {
                long hash = ComputeHash(studentId, capacity);

                return hashTable[hash].Any(x => x.StudentID == studentId && x.CourseID == courseId);
            }
            finally
            {
                Release(ComputeHash(studentId, lockability));
            }
        }

        public void Remove(long studentId, long courseId)
        {
            Acquire(ComputeHash(studentId, lockability));

            try
            {
                long hash = ComputeHash(studentId, capacity);

                if (hashTable[hash].Any(x => x.StudentID == studentId && x.CourseID == courseId))
                {
                    hashTable[hash].Remove(hashTable[hash].Find(x => x.StudentID == studentId && x.CourseID == courseId));
                    Interlocked.Decrement(ref elements);
                }
            }
            finally
            {
                Release(ComputeHash(studentId, lockability));
            }
        }
    }
}