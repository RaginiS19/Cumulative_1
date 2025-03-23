namespace CumulativeProject.Models
{
    /// <summary>
    /// Represents a student in the system.
    /// Contains details such as student's ID, name, student number, and enrollment date.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Gets or sets the unique identifier for the student.
        /// </summary>
        public int StudentId { get; set; }
        public string StudentFName { get; set; }
        public string StudentLName { get; set; }
        public string StudentNumber { get; set; }
        public DateTime? EnrolDate { get; set; }
    }
}
