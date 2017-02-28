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
                model.Add("courses", studentCourses);
                return View["student.cshtml", model];
            };
         }
    }
}
