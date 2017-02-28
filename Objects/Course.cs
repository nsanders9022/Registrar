using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Registrar
{
    public class Course
    {
        private int _id;
        private string _course;
        public Course(string course, int id = 0)
        {
            _id = id;
            _course = course;
        }

        public static List<Course> GetAll()
        {
            List<Course> newList = new List<Course>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM courses;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int courseId = rdr.GetInt32(0);
                string courseName = rdr.GetString(1);
                Course newCourse = new Course(courseName, courseId);
                newList.Add(newCourse);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return newList;
        }

        public override bool Equals(System.Object otherCourse)
        {
            return false;
        }





















        public string GetCourse()
        {
            return _course;
        }
        public void SetCourse(string newCourse)
        {
            _course = newCourse;
        }
        public int GetId()
        {
            return _id;
        }
        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM courses;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
