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
            Course firstCourse = new Course("Coding");
            Course secondCourse = new Course("Coding");

            //Assert
            Assert.Equal(firstCourse, secondCourse);
        }

        [Fact]
        public void Test_Save_SaveCourseToDatabase()
        {
            //Arrange
            Course testCourse = new Course("Math");
            testCourse.Save();

            //Act
            List<Course> result = Course.GetAll();
            List<Course> testList = new List<Course>{testCourse};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignIdCourseObject()
        {
            // Arrange
            Course testCourse = new Course("Business");
            testCourse.Save();

            // Act
            Course savedCourse = Course.GetAll()[0];

            int result = savedCourse.GetId();
            int testId = testCourse.GetId();

            // Assert
            Assert.Equal(testId, result);
        }












        public void Dispose()
        {
            Course.DeleteAll();
            // Category.DeleteAll();
        }
    }
}
