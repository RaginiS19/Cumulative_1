using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using CumulativeProject.Models;

namespace CumulativeProject.Controllers
{
    /// <summary>
    /// MVC Controller for managing teacher-related pages.
    /// Provides functionality to display a list of teachers and details of a specific teacher.
    /// </summary>
    public class TeacherPageController : Controller
    {
        private readonly SchoolDbContext _context = new SchoolDbContext();
        /// <summary>
        /// Retrieves and displays a list of all teachers from the database.
        /// </summary>
        // GET: /Teacher/List
        public IActionResult List()
        {
            Console.WriteLine("List() method called!"); // Debug statement

            List<Teacher> teachers = new List<Teacher>();

            MySqlConnection conn = _context.AccessDatabase();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary FROM teachers";
            MySqlDataReader resultSet = cmd.ExecuteReader();

            while (resultSet.Read())
            {
                teachers.Add(new Teacher
                {
                    TeacherId = resultSet.GetInt32("teacherid"),
                    TeacherFName = resultSet.GetString("teacherfname"),
                    TeacherLName = resultSet.GetString("teacherlname"),
                    EmployeeNumber = resultSet.GetString("employeenumber"),
                    HireDate = resultSet.GetDateTime("hiredate"),
                    Salary = resultSet.GetDecimal("salary")
                });
            }

            resultSet.Close();
            conn.Close();

            return View(teachers);
        }
        /// <summary>
        /// Retrieves and displays details of a specific teacher by ID.
        /// </summary>
        // GET: /Teacher/Show/{id}
        public IActionResult Show(int id)
        {
            Teacher teacher = null;

            MySqlConnection conn = _context.AccessDatabase();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary FROM teachers WHERE teacherid = @id LIMIT 1";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            MySqlDataReader resultSet = cmd.ExecuteReader();

            if (resultSet.Read())
            {
                teacher = new Teacher
                {
                    TeacherId = resultSet.GetInt32("teacherid"),
                    TeacherFName = resultSet.GetString("teacherfname"),
                    TeacherLName = resultSet.GetString("teacherlname"),
                    EmployeeNumber = resultSet.GetString("employeenumber"),
                    HireDate = resultSet.GetDateTime("hiredate"),
                    Salary = resultSet.GetDecimal("salary")
                };
            }

            resultSet.Close();
            conn.Close();

            return View(teacher);
        }
    }
}
