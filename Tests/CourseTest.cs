using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Registrar
{
    public class CourseTest : IDisposable
    {
        public CourseTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_CoursesEmptyAtFirst()
        {
            // Arrange, Act
            int result = Course.GetAll().Count;

            // Assert
            Assert.Equal(0, result);
        }














        public void Dispose()
        {
            Course.DeleteAll();
            // Category.DeleteAll();
        }
    }
}
