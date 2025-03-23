using CumulativeProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace CumulativeProject.Controllers
{
    /// <summary>
    /// Controller responsible for handling student-related web pages.
    /// </summary>
    public class StudentPageController : Controller
    {
        private readonly SchoolDbContext _context = new SchoolDbContext();
         /// <summary>
        /// Retrieves a list of all students and displays them in the view.
        /// </summary>
        // GET: /StudentPage/List
        public IActionResult List()
        {
            List<Student> students = new List<Student>();

            using (MySqlConnection conn = _context.AccessDatabase())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT studentid, studentfname, studentlname, studentnumber, enroldate FROM students";

                using (MySqlDataReader resultSet = cmd.ExecuteReader())
                {
                    while (resultSet.Read())
                    {
                        students.Add(new Student
                        {
                            StudentId = resultSet.GetInt32("studentid"),
                            StudentFName = resultSet.GetString("studentfname"),
                            StudentLName = resultSet.GetString("studentlname"),
                            StudentNumber = resultSet.GetString("studentnumber"),
                            EnrolDate = resultSet.IsDBNull(resultSet.GetOrdinal("enroldate")) ? (DateTime?)null : resultSet.GetDateTime("enroldate")
                        });
                    }
                }
            }

            return View(students);
        }
         /// <summary>
        /// Retrieves details of a specific student by ID and displays them in the view.
        /// </summary>
        // GET: /StudentPage/Show/{id}
        public IActionResult Show(int id)
        {
            Student student = null;

            using (MySqlConnection conn = _context.AccessDatabase())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT studentid, studentfname, studentlname, studentnumber, enroldate FROM students WHERE studentid = @id LIMIT 1";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();

                using (MySqlDataReader resultSet = cmd.ExecuteReader())
                {
                    if (resultSet.Read())
                    {
                        student = new Student
                        {
                            StudentId = resultSet.GetInt32("studentid"),
                            StudentFName = resultSet.GetString("studentfname"),
                            StudentLName = resultSet.GetString("studentlname"),
                            StudentNumber = resultSet.GetString("studentnumber"),
                            EnrolDate = resultSet.IsDBNull(resultSet.GetOrdinal("enroldate")) ? (DateTime?)null : resultSet.GetDateTime("enroldate")
                        };
                    }
                }
            }

            if (student == null)
                return NotFound("Student not found.");

            return View(student);
        }
    }
}
