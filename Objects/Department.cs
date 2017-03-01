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
