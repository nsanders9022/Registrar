using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Registrar
{
    public class Department
    {
        private int _id;
        private string _department;

        public Department(string department, int id = 0)
        {
            _id = id;
            _department = department;
        }

        public override bool Equals(System.Object otherDepartment)
        {
            if(!(otherDepartment is Department))
            {
                return false;
            }
            else
            {
                Department newDepartment = (Department) otherDepartment;
                bool idEquality = this.GetId() == newDepartment.GetId();
                bool departmentEquality = this.GetDepartment() == newDepartment.GetDepartment();
                return (idEquality && departmentEquality);
            }
        }

        public static List<Department> GetAll()
        {
            List<Department> newList = new List<Department>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM departments", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int departmentId = rdr.GetInt32(0);
                string departmentName = rdr.GetString(1);
                Department newDepartment = new Department(departmentName, departmentId);
                newList.Add(newDepartment);
            }

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return newList;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO departments (department) OUTPUT INSERTED.id VALUES (@DepartmentName);", conn);

            SqlParameter departmentParameter = new SqlParameter("@DepartmentName", this.GetDepartment());
            cmd.Parameters.Add(departmentParameter);
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

        public static Department Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM departments WHERE id = @DepartmentId;", conn);
            SqlParameter departmentParameter = new SqlParameter("@DepartmentId", id.ToString());
            cmd.Parameters.Add(departmentParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            int foundDepartmentId = 0;
            string foundDepartmentName = null;

            while(rdr.Read())
            {
                foundDepartmentId = rdr.GetInt32(0);
                foundDepartmentName = rdr.GetString(1);
            }
            Department foundDepartment = new Department(foundDepartmentName, foundDepartmentId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundDepartment;
        }

        public void AddCourse(Course newCourse)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO departments_courses (department_id, course_id) VALUES (@DepartmentId, @CourseId);", conn);

            SqlParameter departmentIdParameter = new SqlParameter("@DepartmentId", this.GetId());
            cmd.Parameters.Add(departmentIdParameter);

            SqlParameter courseIdParameter = new SqlParameter("@CourseId", newCourse.GetId());
            cmd.Parameters.Add(courseIdParameter);

            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
        }

        public List<Course> GetCourses()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT departments.* FROM courses JOIN departments_courses ON (courses.id = departments_courses.course_id) JOIN departments ON (departments_courses.department_id = departments.id) WHERE departments.id = @DepartmentId;", conn);
            SqlParameter DepartmentIdParameter = new SqlParameter("@DepartmentId", this.GetId().ToString());

            cmd.Parameters.Add(DepartmentIdParameter);

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























        public string GetDepartment()
        {
            return _department;
        }
        public void SetDepar(string newDepartment)
        {
            _department = newDepartment;
        }
        public int GetId()
        {
            return _id;
        }
        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM departments;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
