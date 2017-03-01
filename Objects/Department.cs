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

        public static List<Department> GetAll()
        {
            List<Department> newList = new List<Department>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM departemnts", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int departemntId = rdr.GetInt32(0);
                string departmentName = rdr.GetString(1);
                Department newDepartment = new Department(departmentName, departemntId);
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
