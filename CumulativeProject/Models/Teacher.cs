namespace CumulativeProject.Models
{
    /// <summary>
    /// Represents a teacher in the system.
    /// Contains details such as teacher's ID, name, employee number, hire date, and salary.
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// Gets or sets the unique identifier for the teacher.
        /// </summary>
        public int TeacherId { get; set; }
        public string? TeacherFName { get; set; }
        public string? TeacherLName { get; set; }
        public string? EmployeeNumber { get; set; }
        public DateTime? HireDate { get; set; }
        public decimal? Salary { get; set; }
    }
}
