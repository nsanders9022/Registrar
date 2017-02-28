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
            if(!(otherCourse is Course))
            {
                return false;
            }
            else
            {
                Course newCourse = (Course) otherCourse;
                bool idEquality = this.GetId() == newCourse.GetId();
                bool courseEquality = this.GetCourse() == newCourse.GetCourse();
                return (idEquality && courseEquality);
            }
        }
        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO courses (course) OUTPUT INSERTED.id VALUES (@CourseName);", conn);

            SqlParameter courseParameter = new SqlParameter("@CourseName", this.GetCourse());
            cmd.Parameters.Add(courseParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
        }

        public static Course Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE id = @CourseId;", conn);
            SqlParameter courseParameter = new SqlParameter("@CourseId", id.ToString());
            cmd.Parameters.Add(courseParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            int foundCourseId = 0;
            string foundCourseName = null;

            while(rdr.Read())
            {
                foundCourseId = rdr.GetInt32(0);
                foundCourseName = rdr.GetString(1);
            }
            Course foundCourse = new Course(foundCourseName, foundCourseId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundCourse;
        }

        public void AddStudent(Student newStudent)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO students_courses (student_id, course_id) VALUES (@StudentId, @CourseId);", conn);
            SqlParameter studentIdParameter = new SqlParameter("@StudentId", newStudent.GetId());
            cmd.Parameters.Add(studentIdParameter);

            SqlParameter courseIdParameter = new SqlParameter("@CourseId", this.GetId());
            cmd.Parameters.Add(courseIdParameter);

            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
        }

        // public List<Student> GetStudents()
        // {
        //     SqlConnection conn = DB.Connection();
        //     conn.Open();
        //
        //     SqlCommand cmd = new SqlCommand("SELECT students.* FROM courses JOIN students_courses ON (courses.id = students_courses.course_id) JOIN students ON (students_courses.student_id = students.id) WHERE courses.id = @CourseId");
        //     SqlParameter CourseIdParameter = new SqlParameter("@CourseId", this.GetId().ToString());
        //
        //     cmd.Parameters.Add(CourseIdParameter);
        //
        //     SqlDataReader rdr = cmd.ExecuteReader();
        //
        //     List<Student> newList = new List<Student>{};
        //
        //     while(rdr.Read())
        //     {
        //         int studentId = rdr.GetInt32(0);
        //         string studentName = rdr.GetString(1);
        //
        //         Student newStudent = new Student(studentName, studentId);
        //         newList.Add(newStudent);
        //     }
        //     if (rdr != null)
        //     {
        //         rdr.Close();
        //     }
        //     if (conn != null)
        //     {
        //         conn.Close();
        //     }
        //     return newList;
        // }

        public List<Student> GetStudents()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT student_id FROM students_courses WHERE course_id = @CourseId;", conn);
            SqlParameter courseIdParameter = new SqlParameter();
            courseIdParameter.ParameterName = "@CourseId";
            courseIdParameter.Value = this.GetId();
            cmd.Parameters.Add(courseIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            List<int> studentIds = new List<int> {};
            while(rdr.Read())
            {
                int studentId = rdr.GetInt32(0);
                studentIds.Add(studentId);
            }
            if (rdr != null)
            {
                rdr.Close();
            }

            List<Student> students = new List<Student> {};
            foreach (int studentId in studentIds)
            {
                SqlCommand studentQuery = new SqlCommand("SELECT * FROM students WHERE id = @StudentId;", conn);

                SqlParameter studentIdParameter = new SqlParameter();
                studentIdParameter.ParameterName = "@StudentId";
                studentIdParameter.Value = studentId;
                studentQuery.Parameters.Add(studentIdParameter);

                SqlDataReader queryReader = studentQuery.ExecuteReader();
                while(queryReader.Read())
                {
                    int thisStudentId = queryReader.GetInt32(0);
                    string studentDescription = queryReader.GetString(1);
                    Student foundStudent = new Student(studentDescription, thisStudentId);
                    students.Add(foundStudent);
                }
                if (queryReader != null)
                {
                    queryReader.Close();
                }
            }
            if (conn != null)
            {
                conn.Close();
            }
            return students;
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
