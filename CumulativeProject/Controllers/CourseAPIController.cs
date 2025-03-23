using CumulativeProject.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace CumulativeProject.Controllers
{
    [ApiController]
    [Route("api/CourseAPI")]
    public class CourseAPIController : Controller
    {
        private readonly SchoolDbContext _context = new SchoolDbContext();

        // GET: api/CourseAPI/GetAllCourses
        [HttpGet("GetAllCourses")]
        public IEnumerable<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();

            MySqlConnection conn = _context.AccessDatabase();
            conn.Open();
            Console.WriteLine("Connected to School database successfully!");

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT courseid, coursecode, teacherid, startdate, finishdate, coursename FROM courses";
            MySqlDataReader resultSet = cmd.ExecuteReader();

            while (resultSet.Read())
            {
                courses.Add(new Course
                {
                    CourseId = resultSet.GetInt32("courseid"),
                    CourseCode = resultSet.IsDBNull(resultSet.GetOrdinal("coursecode")) ? null : resultSet.GetString("coursecode"),
                    TeacherId = resultSet.IsDBNull(resultSet.GetOrdinal("teacherid")) ? null : resultSet.GetInt32("teacherid"),
                    StartDate = resultSet.IsDBNull(resultSet.GetOrdinal("startdate")) ? null : resultSet.GetDateTime("startdate"),
                    FinishDate = resultSet.IsDBNull(resultSet.GetOrdinal("finishdate")) ? null : resultSet.GetDateTime("finishdate"),
                    CourseName = resultSet.IsDBNull(resultSet.GetOrdinal("coursename")) ? null : resultSet.GetString("coursename")
                });
            }

            resultSet.Close();
            conn.Close();

            return courses;
        }

        // GET: api/CourseAPI/GetCourseById/{id}
        [HttpGet("GetCourseById/{id}")]
        public Course GetCourseById(int id)
        {
            Course course = null;

            MySqlConnection conn = _context.AccessDatabase();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT courseid, coursecode, teacherid, startdate, finishdate, coursename FROM courses WHERE courseid = @id LIMIT 1";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            MySqlDataReader resultSet = cmd.ExecuteReader();

            if (resultSet.Read())
            {
                course = new Course
                {
                    CourseId = resultSet.GetInt32("courseid"),
                    CourseCode = resultSet.IsDBNull(resultSet.GetOrdinal("coursecode")) ? null : resultSet.GetString("coursecode"),
                    TeacherId = resultSet.IsDBNull(resultSet.GetOrdinal("teacherid")) ? null : resultSet.GetInt32("teacherid"),
                    StartDate = resultSet.IsDBNull(resultSet.GetOrdinal("startdate")) ? null : resultSet.GetDateTime("startdate"),
                    FinishDate = resultSet.IsDBNull(resultSet.GetOrdinal("finishdate")) ? null : resultSet.GetDateTime("finishdate"),
                    CourseName = resultSet.IsDBNull(resultSet.GetOrdinal("coursename")) ? null : resultSet.GetString("coursename")
                };
            }

            resultSet.Close();
            conn.Close();

            return course;
        }
    }
}
