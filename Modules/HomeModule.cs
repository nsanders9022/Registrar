using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Registrar
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                return View["index.cshtml"];
            };

            Get["/students"] = _ => {
                List<Student> allStudents = Student.GetAll();
                return View["students.cshtml", allStudents];
            };

            Post["/students"] = _ => {
                var newStudent = new Student(Request.Form["student"]);
                newStudent.Save();
                var allStudents = Student.GetAll();
                return View ["students.cshtml", allStudents];
            };

            Get["/student/{studentId}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Student newStudent = Student.Find(parameters.studentId);
                List<Course> studentCourses = newStudent.GetCourses();
                model.Add("student", newStudent);
                model.Add("all_courses", Course.GetAll());
                model.Add("courses", studentCourses);
                return View["student.cshtml", model];
            };

            Get["/courses"] = _ => {
                List<Course> allCourses = Course.GetAll();
                return View["courses.cshtml", allCourses];
            };

            Post["/courses"] = _ => {
                var newCourse = new Course(Request.Form["course"]);
                newCourse.Save();
                var allCourses = Course.GetAll();
                return View ["courses.cshtml", allCourses];
            };

            Get["/course/{courseId}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Course newCourse = Course.Find(parameters.courseId);
                List<Student> courseStudents = newCourse.GetStudents();
                model.Add("course", newCourse);
                model.Add("all_students", Student.GetAll());
                model.Add("students", courseStudents);
                return View["course.cshtml", model];
            };

            Get["/student/course_added"] = _ => {
                return View["course_added.cshtml"];
            };

            Post["/student/course_added"] = _ => {
                Course course = Course.Find(Request.Form["course-list"]);
                Student student = Student.Find(Request.Form["student"]);
                student.AddCourse(course);
                return View["course_added.cshtml"];
            };

            Post["/course/student_added"] = _ => {
                Course course = Course.Find(Request.Form["course-list"]);
                Student student = Student.Find(Request.Form["student"]);
                student.AddCourse(course);
                return View["student_added.cshtml"];
            };
        }
    }
}
