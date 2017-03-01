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
        [Fact]
        public void Test_Save_AssignIdDepartmentObject()
        {
            // Arrange
            Department testDepartment = new Department("Business");
            testDepartment.Save();

            // Act
            Department savedDepartment = Department.GetAll()[0];

            int result = savedDepartment.GetId();
            int testId = testDepartment.GetId();

            // Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindsDepartmentId()
        {
            // Arrange
            Department testDepartment = new Department("P.E.");
            testDepartment.Save();

            // Act
            Department foundDepartment = Department.Find(testDepartment.GetId());

            // Assert
            Assert.Equal(testDepartment, foundDepartment);
        }














        public void Dispose()
        {
            Department.DeleteAll();
            // Department.DeleteAll();
            // Student.DeleteAll();
        }
    }
}
