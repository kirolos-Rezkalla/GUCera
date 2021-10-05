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
    public partial class DefineAssignment : System.Web.UI.Page
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
           
            Int32 course_id = Int32.Parse(CourseId.Text);
            Int32 number = Int32.Parse(Number.Text);
            String type = Type.Text;
            Int32 full_grade = Int32.Parse(FullGrade.Text);
            decimal weight = Decimal.Parse(Weight.Text);
            DateTime deadline = DateTime.Parse(Deadline.Text);
            String content = Content.Text;
            Boolean course_found = false;
            int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));
            String session_id_string = session_id.ToString();





            if (course_id.ToString() == "")
            {
                MessageBox.Show("You have to enter the Course Id");
                return;
            }
            if (number.ToString() == "")
            {
                MessageBox.Show("You have to enter the number ");
                return;
            }
            if (type.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter the type ");
                return;
            }
            if (full_grade.ToString() == "")
            {
                MessageBox.Show("You have to enter the full grade ");
                return;
            }
            if (weight.ToString() == "")
            {
                MessageBox.Show("You have to enter the weight ");
                return;
            }
            if (deadline.ToString() == "")
            {
                MessageBox.Show("You have to enter the deadline ");
                return;
            }
            if (content.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter the content ");
                return;
            }




            SqlCommand courses = new SqlCommand("InstructorTeachThisCourse", conn);
            courses.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader rdr = courses.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                int stored_course = rdr.GetInt32(rdr.GetOrdinal("cid"));
                int stored_instructor_id = rdr.GetInt32(rdr.GetOrdinal("insid"));



                if (course_id == stored_course && session_id== stored_instructor_id)
                {
                    course_found = true;
                    break;
                }
            }
            conn.Close();

            



            if (course_found == true)
            {
                SqlCommand submit_assignment = new SqlCommand("DefineAssignmentOfCourseOfCertianType", conn);
                submit_assignment.CommandType = CommandType.StoredProcedure;

                submit_assignment.Parameters.Add(new SqlParameter("@instId", session_id));
                submit_assignment.Parameters.Add(new SqlParameter("@cid", course_id));
                submit_assignment.Parameters.Add(new SqlParameter("@number", number));
                submit_assignment.Parameters.Add(new SqlParameter("@type", type));
                submit_assignment.Parameters.Add(new SqlParameter("@fullGrade", full_grade));
                submit_assignment.Parameters.Add(new SqlParameter("@weight", weight));
                submit_assignment.Parameters.Add(new SqlParameter("@deadline", deadline));
                submit_assignment.Parameters.Add(new SqlParameter("@content", content));

                conn.Open();
                submit_assignment.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("YOU HAVE ADDED AN ASSIGNMENT SUCCESSFULLY!");

                Response.Redirect("Instructorprofile.aspx");
            }
            else 
            {
                MessageBox.Show("This instructor does not teach this course ");
                return;
            }

        }


    }
}