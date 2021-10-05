using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace GUCera
{
    public partial class GradeAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["user_login"]) != "")
            {
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);
            Int32 student_id = Int32.Parse(StudentId.Text);
            Int32 course_id = Int32.Parse(CourseId.Text);
            Int32 assignmnet_number = Int32.Parse(AssignmnetNumber.Text);
            String type = Type.Text;
            decimal grade = Decimal.Parse(Grade.Text);
            int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));
            String session_id_string = session_id.ToString();


            if (student_id.ToString() == "")
            {
                MessageBox.Show("You have to enter the Student Id");
                return;
            }
            if (course_id.ToString() == "")
            {
                MessageBox.Show("You have to enter the Course Id");
                return;
            }
            if (assignmnet_number.ToString() == "")
            {
                MessageBox.Show("You have to enter the Assignment Number");
                return;
            }
            if (type.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter the type");
                return;
            }
            if (grade.ToString() == "")
            {
                MessageBox.Show("You have to enter the grade");
                return;
            }

            Boolean found = false;
            Boolean assignment_found = false;
            SqlCommand courses = new SqlCommand("InstructorTeachThisStudentThisCourse", conn);
            courses.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader rdr = courses.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                int stored_course = rdr.GetInt32(rdr.GetOrdinal("cid"));
                int stored_instructor_id = rdr.GetInt32(rdr.GetOrdinal("insid"));
                int stored_student_id = rdr.GetInt32(rdr.GetOrdinal("sid"));



                if (course_id == stored_course && session_id == stored_instructor_id && stored_student_id == student_id)
                {
                    found = true;
                    break;
                }
            }


            conn.Close();

            SqlCommand assignments = new SqlCommand("AllAssignments", conn);
            assignments.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader rdr1 = assignments.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr1.Read())
            {
                int stored_number = rdr1.GetInt32(rdr1.GetOrdinal("assignmentNumber"));
                String stored_type = rdr1.GetString(rdr1.GetOrdinal("assignmenttype"));
                int stored_cid = rdr1.GetInt32(rdr1.GetOrdinal("cid"));


                if (assignmnet_number == stored_number && type == stored_type && course_id==stored_cid)
                {
                    assignment_found = true;
                   
                }


            }
            conn.Close();

            if (found == true && assignment_found == true)
            {
                SqlCommand instructor_grade_assignment = new SqlCommand("InstructorgradeAssignmentOfAStudent", conn);
                instructor_grade_assignment.CommandType = CommandType.StoredProcedure;

                instructor_grade_assignment.Parameters.Add(new SqlParameter("@instrId", session_id));
                instructor_grade_assignment.Parameters.Add(new SqlParameter("@sid", student_id));
                instructor_grade_assignment.Parameters.Add(new SqlParameter("@cid", course_id));
                instructor_grade_assignment.Parameters.Add(new SqlParameter("@assignmentNumber", assignmnet_number));
                instructor_grade_assignment.Parameters.Add(new SqlParameter("@type", type));
                instructor_grade_assignment.Parameters.Add(new SqlParameter("@grade", grade));


                conn.Open();
                instructor_grade_assignment.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("YOU HAVE GRADDED AN ASSIGNMENT SUCCESSFULLY!");

                Response.Redirect("Instructorprofile.aspx");
            }
            else if (found == false && assignment_found == true)
            {
                MessageBox.Show("This student  does not take this course with this instructor ");
                return;
            }
            else if (found == true && assignment_found == false)
            {
                MessageBox.Show("Assignment not found ");
                return;
            }
            else 
            {
                MessageBox.Show("This student  does not take this course with this instructor and assignment not found ");
                return;
            }




        }
    }
}