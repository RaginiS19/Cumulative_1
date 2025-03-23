namespace CumulativeProject.Models
{ 
    /// <summary>
    /// Represents a course in the system.
    /// Includes properties related to course details such as course ID, code, teacher, dates, and name.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Gets or sets the unique identifier for the course.
        /// </summary>
        public int CourseId { get; set; }
        public string? CourseCode { get; set; }
        public int? TeacherId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string? CourseName { get; set; }
    }
}
