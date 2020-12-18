using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;


namespace AssignmentThree_N01458977.Controllers
{
    public class TeacherController : Controller
    {
        // GET: /Teacher/Index
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns list of teacher names from database.
        /// <example> /Teacher/List </example>
        /// </summary>
        /// <param></param>
        /// <return>List of all teacher names</return>
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            ViewBag.ListTeachers = controller.ListTeachers();

            return View();
        }

        /// <summary>
        /// Returns selected teacher name.
        /// <example> 6/Teacher/Show?id=2 </example>
        /// </summary>
        /// <param>?id=2</param>
        /// <return>Selected Teachers name</return>
        public ActionResult Show(int? id)
        {
            Models.Teacher NewTeacher = new Models.Teacher();

            if (id == null) NewTeacher = null;
            else
            {
                TeacherDataController controller = new TeacherDataController();
                NewTeacher = controller.ShowTeacher((int)id);
            }

            return View(NewTeacher);
        }

        /// <summary>
        /// Shows info on the teacher that can be deleted.
        /// <example> /Teacher/DeleteConfirm </example>
        /// </summary>
        /// <param></param>
        /// <return>Routes back to selected teacher if canceled.</return>
        public ActionResult DeleteConfirm(int? id)
        {
            Models.Teacher NewTeacher = new Models.Teacher();

            if (id == null) NewTeacher = null;
            else
            {
                TeacherDataController controller = new TeacherDataController();
                NewTeacher = controller.ShowTeacher((int)id);
            }

            return View(NewTeacher);
        }

        /// <summary>
        /// Deletes Selected Teacher
        /// <example> /Teacher/List </example>
        /// </summary>
        /// <param></param>
        /// <return>Routes back to Teacher list.</return>
        //POST : /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        /// <summary>
        /// Shows new teacher form.
        /// <example> /Teacher/List </example>
        /// </summary>
        /// <param></param>
        /// <return>Teacher list page</return>
        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        /// <summary>
        /// Adds new teacher to the database database.
        /// <example> /Teacher/List </example>
        /// </summary>
        /// <param></param>
        /// <return>List of all teacher names</return>
        //POST : /Teacher/Create
        public ActionResult Create(string TeacherFname, string TeacherLname, string Salary, string HireDate, string EmployeeNumber)
        {
            //Identify this method is running
            //Identify the inpiuts provided from the form

            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(Salary);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(EmployeeNumber);

            Models.Teacher NewTeacher = new Models.Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.Salary = Salary;
            NewTeacher.HireDate = HireDate;
            NewTeacher.EmployeeNumber = EmployeeNumber;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
        public ActionResult UpdateConfirm(int id)
        {
            Models.Teacher NewTeacher = new Models.Teacher();

            if (id == null) NewTeacher = null;
            else
            {
                TeacherDataController controller = new TeacherDataController();
                NewTeacher = controller.UpdateConfirmTeacher((int)id);
            }

            return View(NewTeacher);
        }



        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the Author to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="Salary">The updated salary of the teacher.</param>
        /// <param name="HireDate">The updated hire date of the teacher.</param>
        /// <param name="EmployeeNumber">The updated employee number of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Author/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":Chris
        ///	"TeacherLname":Kinsman
        ///	"Salary":1000.00
        ///	"HireDate":12,12,2020
        ///	"EmployeeNumber":T402
        /// }
        /// </example>
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string Salary, string HireDate, string EmployeeNumber)
        {
            //Identify this method is running
            //Identify the inpiuts provided from the form

            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(Salary);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(EmployeeNumber);

            Models.Teacher NewTeacher = new Models.Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.Salary = Salary;
            NewTeacher.HireDate = HireDate;
            NewTeacher.EmployeeNumber = EmployeeNumber;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, NewTeacher);

            return RedirectToAction("Show/" + id);
        }

    }
}