using CumulativeProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace CumulativeProject.Controllers
{
    [ApiController]
    [Route("api/TeacherAPI")]
    public class TeacherAPIController : Controller
    {
        private readonly SchoolDbContext _context = new SchoolDbContext();

        // GET: api/TeacherAPI/GetAllTeachers
        [HttpGet("GetAllTeachers")]
        public IEnumerable<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();

            MySqlConnection conn = _context.AccessDatabase();
            conn.Open();
            Console.WriteLine("Connected to School database successfully!");


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

            return teachers;
        }



        // GET: api/TeacherAPI/GetTeacherById/{id}
        [HttpGet("GetTeacherById/{id}")]
        public Teacher GetTeacherByName(int id)
        {
            Teacher teacher = null;

            MySqlConnection conn = _context.AccessDatabase();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, name, course, salary FROM teachers WHERE id = @id LIMIT 1";
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

            return teacher;
        }


    }
}
