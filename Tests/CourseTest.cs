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

        [Fact]
        public void Test_Equal_ReturnsTrueForSameCourse()
        {
            //Arrange, Act
            Course firstCourse = new Course("Math");
            Course secondCourse = new Course("Math");

            //Assert
            Assert.Equal(firstCourse, secondCourse);
        }












        public void Dispose()
        {
            Course.DeleteAll();
            // Category.DeleteAll();
        }
    }
}
