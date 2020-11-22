namespace ExamSystem.LockFreeListHashTables
{
    public class LockFreeNode
    {
        public long StudentID { get; private set; }
        public long CourseID { get; private set; }

        public LockFreeNode(long studentID, long courseID)
        {
            StudentID = studentID;
            CourseID = courseID;
        }

        public bool IsLogicallyRemoved { get; set; }

        LockFreeNode next;

        public ref LockFreeNode GetRefNext()
        {
            return ref next;
        }
        
        public LockFreeNode GetNext()
        {
            return next;
        }

        public void SetNext(LockFreeNode node)
        {
            next = node;
        }

        public override bool Equals(object obj)
        {
            if(obj as LockFreeNode == null)
            {
                return false;
            }

            LockFreeNode node = (LockFreeNode)obj;

            return (node.StudentID == StudentID) && (node.CourseID == CourseID);
        }
    }
}