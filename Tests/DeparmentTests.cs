using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Registrar
{
    public class DepartmentTest : IDisposable
    {
        public DepartmentTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_DepartmentEmptyAtFirst()
        {
            // Arrange, Act
            int result = Department.GetAll().Count;

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Save()
        {
            Department testDepartment = new Department("English");
            testDepartment.Save();

            List<Department> result = Department.GetAll();
            List<Department> testList = new List<Department>{testDepartment};

            Assert.Equal(testList, result);
        }














        public void Dispose()
        {
            Department.DeleteAll();
            // Course.DeleteAll();
            // Student.DeleteAll();
        }
    }
}
