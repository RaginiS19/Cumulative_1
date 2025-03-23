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

            return teacher;
        }


    }
}
