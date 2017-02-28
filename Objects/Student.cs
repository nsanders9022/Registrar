using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Registrar
{
    public class Student
    {
        private int _id;
        private string _name;

        public Student (string name, int id = 0)
        {
            _id = id;
            _name = name;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public static List<Student> GetAll()
        {
            List<Student> AllStudents = new List<Student>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM students", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int studentId = rdr.GetInt32(0);
                string studentName = rdr.GetString(1);
                Student newStudent = new Student(studentName, studentId);
                AllStudents.Add(newStudent);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return AllStudents;
        }

        public override bool Equals(System.Object otherStudent)
        {
            if (!(otherStudent is Student))
            {
                return false;
            }
            else
            {
                Student newStudent = (Student) otherStudent;
                bool idEquality = this.GetId()  == newStudent.GetId();
                return (idEquality);
            }
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO students(name) OUTPUT INSERTED.id VALUES(@StudentName);", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@StudentName";
            nameParameter.Value = this.GetName();

            cmd.Parameters.Add(nameParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
        }

        public static Student Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE id = @StudentId;", conn);
            SqlParameter StudentIdParameter = new SqlParameter("@StudentId", id.ToString());
            cmd.Parameters.Add(StudentIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            int foundStudentId = 0;
            string foundStudentName = null;

            while(rdr.Read())
            {
                foundStudentId = rdr.GetInt32(0);
                foundStudentName = rdr.GetString(1);
            }
            Student foundStudent = new Student(foundStudentName, foundStudentId);

            if(rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundStudent;
        }

        public void AddCourse(Course newCourse)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO students_courses (student_id, course_id) VALUES (@StudentId, @CourseId);", conn);

            SqlParameter courseIdParameter = new SqlParameter ("@CourseId", newCourse.GetId());
            cmd.Parameters.Add(courseIdParameter);

            SqlParameter studentIdParameter = new SqlParameter("@StudentId", this.GetId());
            cmd.Parameters.Add(studentIdParameter);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }

        public List<Course> GetCourses()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT courses.* FROM students JOIN students_courses ON (students.id = students_courses.student_id) JOIN courses ON (students_courses.course_id = courses.id) WHERE students.id = @StudentId;", conn);

            SqlParameter StudentIdParameter = new SqlParameter("@StudentId", this.GetId().ToString());

            cmd.Parameters.Add(StudentIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Course> newList = new List<Course>{};

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




        public static void DeleteAll()
        {
          SqlConnection conn = DB.Connection();
          conn.Open();
          SqlCommand cmd = new SqlCommand("DELETE FROM students;", conn);
          cmd.ExecuteNonQuery();
          conn.Close();
        }

    }
}
