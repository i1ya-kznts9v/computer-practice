namespace ExamSystem.LockFreeListHashTables
{
    public class LockFreeListWindow
    {
        public LockFreeNode Previous { get; private set; }
        public LockFreeNode Current { get; private set; }

        public LockFreeListWindow(LockFreeNode previous, LockFreeNode current)
        {
            Previous = previous;
            Current = current;
        }
    }
}