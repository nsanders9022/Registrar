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

        [Fact]
        public void Test_Find_FindsCourseId()
        {
            // Arrange
            Course testCourse = new Course("P.E.");
            testCourse.Save();

            // Act
            Course foundCourse = Course.Find(testCourse.GetId());

            // Assert
            Assert.Equal(testCourse, foundCourse);
        }

        [Fact]
        public void Test_GetStudents_ReturnsAllStudentsInACourse()
        {
            //Arrange
            Course testCourse = new Course("Coding");
            testCourse.Save();

            Student testStudent1 = new Student("Kory");
            testStudent1.Save();

            testCourse.AddStudent(testStudent1);
            List<Student> savedStudents = testCourse.GetStudents();
            List<Student> testList = new List<Student> {testStudent1};

            //Assert
            Assert.Equal(testList, savedStudents);
        }












        public void Dispose()
        {
            Course.DeleteAll();
            Student.DeleteAll();
        }
    }
}
