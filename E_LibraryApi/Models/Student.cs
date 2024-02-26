namespace E_LibraryApi.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public string Department { get; set; }
        public int EnrollmentNb { get; set; }
        public string StudentSemester { get; set; }
        public int StudentContact { get; set; }

    }
}
