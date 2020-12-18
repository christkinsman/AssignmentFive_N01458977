using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AssignmentThree_N01458977.Models;
using MySql.Data.MySqlClient;

namespace AssignmentThree_N01458977.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        //Get the teacher name and class by teacher id for SHOW page
        [HttpGet]
        public Teacher ShowTeacher(int? id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();

                NewTeacher.TeacherId = TeacherId;

                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Open the connection between the web server and database
            Conn.Open();

            //Create another instance of a connection
            MySqlCommand classcmd = Conn.CreateCommand();

            classcmd.CommandText = "Select * from classes where teacherid = " + id;

            MySqlDataReader ClassResultSet = classcmd.ExecuteReader();

            while (ClassResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                string TeacherClass = ClassResultSet["classname"].ToString();

                NewTeacher.TeacherClass = TeacherClass;
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of author namess
            return NewTeacher;
        }


        //Get the class by Teacher id for LIST page
        [HttpGet]
        public IEnumerable<Teacher> ListTeachers()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Author Names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();

                Teacher NewTeacher = new Teacher();

                NewTeacher.TeacherId = TeacherId;

                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;

                //Add the Author Names to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of author names
            return Teachers;
        }

        //Deletes selected Teacher from the database.
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection to the database.
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //create query to delete teacher at id
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //send query
            cmd.ExecuteNonQuery();

            //create query to delete teacher at id
            cmd.CommandText = "Update classes SET teacherid=NULL where teacherid=@id";
            cmd.Prepare();

            //send query
            cmd.ExecuteNonQuery();

            //close connection to the database.
            Conn.Close();
        }

        //Adds Teacher info to database as new teacher.
        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection to the database.
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //create query to add teacher.
            cmd.CommandText = "Insert into teachers (teacherfname, teacherlname, salary, hiredate, employeenumber) " +
                "values (@TeacherFname, @TeacherLname,@Salary, @HireDate, @EmployeeNumber)";
            //set value of input to be form values.
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Prepare();

            //send query
            cmd.ExecuteNonQuery();

            //close connection to the database
            Conn.Close();
        }



        //Updates selected Teacher in the database.
        [HttpPost]
        public Teacher UpdateConfirmTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string Salary = ResultSet["salary"].ToString();
                string HireDate = ResultSet["hiredate"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.Salary = Salary;
                NewTeacher.HireDate = HireDate;
                NewTeacher.EmployeeNumber = EmployeeNumber;
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            return NewTeacher;
        }



        //Deletes selected Teacher from the database.
        [HttpPost]
        public void UpdateTeacher(int id, [FromBody]Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection to the database.
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //create query to add teacher.
            cmd.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, salary=@Salary, hiredate=@HireDate, employeenumber=@EmployeeNumber where teacherid=@id";
            //set value of input to be form values.
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //send query
            cmd.ExecuteNonQuery();

            //close connection to the database
            Conn.Close();
        }
    }
}