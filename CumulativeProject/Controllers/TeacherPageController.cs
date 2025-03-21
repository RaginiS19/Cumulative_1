using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using CumulativeProject.Models;

namespace CumulativeProject.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly SchoolDbContext _context = new SchoolDbContext();

        // GET: /Teacher/List
        public IActionResult List()
        {
            Console.WriteLine("List() method called!"); // Debug statement

            List<Teacher> teachers = new List<Teacher>();

            MySqlConnection conn = _context.AccessDatabase();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, name, course, salary FROM teachers";
            MySqlDataReader resultSet = cmd.ExecuteReader();

            while (resultSet.Read())
            {
                teachers.Add(new Teacher
                {
                    id = resultSet.GetInt32("id"),
                    name = resultSet.GetString("name"),
                    course = resultSet.GetString("course"),
                    salary = resultSet.GetDecimal("salary")
                });
            }

            resultSet.Close();
            conn.Close();

            return View(teachers);
        }

        // GET: /Teacher/Show/{id}
        public IActionResult Show(int id)
        {
            Teacher teacher = null;

            MySqlConnection conn = _context.AccessDatabase();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, name, course, salary FROM teachers WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            MySqlDataReader resultSet = cmd.ExecuteReader();

            if (resultSet.Read())
            {
                teacher = new Teacher
                {
                    id = resultSet.GetInt32("id"),
                    name = resultSet.GetString("name"),
                    course = resultSet.GetString("course"),
                    salary = resultSet.GetDecimal("salary")
                };
            }

            resultSet.Close();
            conn.Close();

            return View(teacher);
        }
    }
}
