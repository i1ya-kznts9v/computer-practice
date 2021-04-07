using System;

namespace ExamSystem.LockFreeListHashTables
{
    public class LockFreeListHashTable : IExamSystem
    {
        public LockFreeList[] HashTable { get; private set; }
        uint capacity;

        public LockFreeListHashTable(uint capacity)
        {
            try
            {
                this.capacity = Check(capacity);
            }
            catch
            {
                this.capacity = 256;
            }

            HashTable = new LockFreeList[this.capacity];

            for(int i = 0; i < this.capacity; i++)
            {
                HashTable[i] = new LockFreeList();
            }
        }

        uint Check(uint capacity)
        {
            if(capacity < 256)
            {
                throw new ArgumentException("Too small capacity\n");
            }

            return capacity;
        }

        long ComputeHash(long studentID)
        {
            studentID ^= (studentID << 13);
            studentID ^= (studentID >> 17);
            studentID ^= (studentID << 5);

            return studentID % capacity;
        }

        public void Add(long studentId, long courseId)
        {
            HashTable[ComputeHash(studentId)].Add(new LockFreeNode(studentId, courseId));
        }

        public bool Contains(long studentId, long courseId)
        {
            return HashTable[ComputeHash(studentId)].Contains(new LockFreeNode(studentId, courseId));
        }

        public void Remove(long studentId, long courseId)
        {
            HashTable[ComputeHash(studentId)].Remove(new LockFreeNode(studentId, courseId));
        }
    }
}