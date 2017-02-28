using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Registrar
{
    public class StudentTest : IDisposable
    {
        public StudentTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void StudentDatabaseEmpty()
        {
            //Arrange, act
            int result = Student.GetAll().Count;

            //Assert
            Assert.Equal(0,result);
        }

        [Fact]
        public void Test_EqualOverrideTrueForSameDescription()
        {
            //Arrange, Act
            Student firstStudent = new Student("Kory");
            Student secondStudent = new Student("Kory");

            //Assert
            Assert.Equal(firstStudent, secondStudent);
        }

        [Fact]
        public void Test_SaveToDatabase()
        {
            // Arrange
            Student newStudent = new Student("Kory");
            newStudent.Save();

            // Act
            List<Student> result = Student.GetAll();
            List<Student> testList = new List<Student>{newStudent};

            // Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_SaveAssignsIdToObject()
        {
            //Arrange
            Student testStudent = new Student("Nicole");
            testStudent.Save();

            //Act
            Student savedStudent = Student.GetAll()[0];

            int result = savedStudent.GetId();
            int testId = testStudent.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindsTaskinDatablase()
        {
            //Arrange
            Student testStudent = new Student ("John");
            testStudent.Save();

            //Act
            Student foundStudent = Student.Find(testStudent.GetId());

            //Asswert
            Assert.Equal(testStudent, foundStudent);
        }

        [Fact]
        public void Test_GetCourses_ReturnsAllCoursesForAStudent()
        {
            //Arrange
            Student testStudent = new Student("Nicole");
            testStudent.Save();

            Course testCourse = new Course("Math");
            testCourse.Save();

            testStudent.AddCourse(testCourse);
            List<Course> savedCourses = testStudent.GetCourses();
            List<Course> testList = new List<Course> {testCourse};

            //Assert
            Assert.Equal(testList, savedCourses);
        }


        public void Dispose()
        {
            Student.DeleteAll();
            // Category.DeleteAll();
        }
    }
}
