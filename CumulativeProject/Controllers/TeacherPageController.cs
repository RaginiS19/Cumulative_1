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
