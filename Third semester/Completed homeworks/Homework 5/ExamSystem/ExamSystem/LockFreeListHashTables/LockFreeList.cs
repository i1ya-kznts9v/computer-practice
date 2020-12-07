using System.Threading;

namespace ExamSystem.LockFreeListHashTables
{
    public class LockFreeList
    {
        volatile LockFreeNode head;

        LockFreeListWindow Find(LockFreeNode node)
        {
            if (head == null || node == null)
            {
                return null;
            }

            LockFreeNode previous = null;
            LockFreeNode current = head;

            while(!current.Equals(node))
            {
                if(current.GetNext() == null)
                {
                    return null;
                }

                previous = current;
                current = current.GetNext();
            }

            return new LockFreeListWindow(previous, current);
        }

        public void Add(LockFreeNode node)
        {
            if(node == null)
            {
                return;
            }

            if(node.IsLogicallyRemoved)
            {
                node.IsLogicallyRemoved = false;
            }

            while(true)
            {
                if(Contains(node))
                {
                    return;
                }

                node.SetNext(head);

                if(Interlocked.CompareExchange(ref head, node, node.GetNext()) != head)
                {
                    return;
                }
            }    
        }

        public bool Contains(LockFreeNode node)
        {
            if (node == null)
            {
                return false;
            }

            LockFreeListWindow window = Find(node);

            if(window == null)
            {
                return false;
            }

            if (window.Current.IsLogicallyRemoved)
            {
                return false;
            }

            return true;
        }

        public void Remove(LockFreeNode node)
        {
            if(node == null)
            {
                return;
            }

            while(true)
            {
                LockFreeListWindow window = Find(node);

                if(window == null)
                {
                    return;
                }

                window.Current.IsLogicallyRemoved = true;

                if(window.Previous == null)
                {
                    if(Interlocked.CompareExchange(ref head, window.Current.GetNext(), window.Current) == window.Current)
                    {
                        return;
                    }
                }
                else
                {
                    if(window.Previous.IsLogicallyRemoved)
                    {
                        continue;
                    }

                    if (Interlocked.CompareExchange(ref window.Previous.GetRefNext(), window.Current.GetNext(), window.Current) == window.Current)
                    {
                        return;
                    }
                }
            }
        }
    }
}