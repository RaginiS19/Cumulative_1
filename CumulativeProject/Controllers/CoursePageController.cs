using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using CumulativeProject.Models;
using System;
using System.Collections.Generic;

namespace CumulativeProject.Controllers
{
    /// <summary>
    /// MVC Controller for handling course-related web pages.
    /// </summary>
    public class CoursePageController : Controller
    {
        private readonly SchoolDbContext _context = new SchoolDbContext();
        /// <summary>
        /// Displays a list of all courses.
        /// </summary>
    
        // GET: /CoursePage/List
        public IActionResult List()
        {
            Console.WriteLine("List() method called!"); // Debug statement

            List<Course> courses = new List<Course>();

            using (MySqlConnection conn = _context.AccessDatabase())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT courseid, coursecode, teacherid, startdate, finishdate, coursename FROM courses";

                using (MySqlDataReader resultSet = cmd.ExecuteReader())
                {
                    while (resultSet.Read())
                    {
                        courses.Add(new Course
                        {
                            CourseId = resultSet.GetInt32("courseid"),
                            CourseCode = resultSet.GetString("coursecode"),
                            TeacherId = resultSet.IsDBNull(resultSet.GetOrdinal("teacherid")) ? (int?)null : resultSet.GetInt32("teacherid"),
                            StartDate = resultSet.IsDBNull(resultSet.GetOrdinal("startdate")) ? (DateTime?)null : resultSet.GetDateTime("startdate"),
                            FinishDate = resultSet.IsDBNull(resultSet.GetOrdinal("finishdate")) ? (DateTime?)null : resultSet.GetDateTime("finishdate"),
                            CourseName = resultSet.GetString("coursename")
                        });
                    }
                }
            }

            return View(courses);
        }

        // GET: /CoursePage/Show/{id}
        public IActionResult Show(int id)
        {
            Course course = null;

            using (MySqlConnection conn = _context.AccessDatabase())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT courseid, coursecode, teacherid, startdate, finishdate, coursename FROM courses WHERE courseid = @id LIMIT 1";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();

                using (MySqlDataReader resultSet = cmd.ExecuteReader())
                {
                    if (resultSet.Read())
                    {
                        course = new Course
                        {
                            CourseId = resultSet.GetInt32("courseid"),
                            CourseCode = resultSet.GetString("coursecode"),
                            TeacherId = resultSet.IsDBNull(resultSet.GetOrdinal("teacherid")) ? (int?)null : resultSet.GetInt32("teacherid"),
                            StartDate = resultSet.IsDBNull(resultSet.GetOrdinal("startdate")) ? (DateTime?)null : resultSet.GetDateTime("startdate"),
                            FinishDate = resultSet.IsDBNull(resultSet.GetOrdinal("finishdate")) ? (DateTime?)null : resultSet.GetDateTime("finishdate"),
                            CourseName = resultSet.GetString("coursename")
                        };
                    }
                }
            }

            if (course == null)
                return NotFound("Course not found.");

            return View(course);
        }
    }
}
