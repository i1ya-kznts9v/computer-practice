namespace ExamSystem.StripedHashTables
{
    public class StripedNode
    {
        public long StudentID { get; private set; }
        public long CourseID { get; private set; }

        public StripedNode(long studentID, long courseID)
        {
            StudentID = studentID;
            CourseID = courseID;
        }
    }
}