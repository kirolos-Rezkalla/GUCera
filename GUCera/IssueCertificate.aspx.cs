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
    public partial class IssueCertificate : System.Web.UI.Page
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
            Int32 student_id = Int32.Parse(StudentId.Text);
            DateTime issue_date = DateTime.Parse(IssueDate.Text);
            int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));
            String session_id_string = session_id.ToString();
            //Boolean err = true;

            if (course_id.ToString() == "")
            {
                MessageBox.Show("You have to enter the Instructor Id");
                return;
            }
            if (student_id.ToString() == "")
            {
                MessageBox.Show("You have to enter the Instructor Id");
                return;
            }
            if (issue_date.ToString() == "")
            {
                MessageBox.Show("You have to enter the Instructor Id");
                return;
            }
            SqlCommand issue_certificate = new SqlCommand("InstructorIssueCertificateToStudent", conn);
            issue_certificate.CommandType = CommandType.StoredProcedure;

            issue_certificate.Parameters.Add(new SqlParameter("@cid", course_id));
            issue_certificate.Parameters.Add(new SqlParameter("@sid", student_id));
            issue_certificate.Parameters.Add(new SqlParameter("@insId", session_id));
            issue_certificate.Parameters.Add(new SqlParameter("@issueDate", issue_date));


            conn.Open();
            Boolean err = false;
            try
            {
                issue_certificate.ExecuteNonQuery();
  
            }
            catch (SqlException)
            {
                err = true;
                MessageBox.Show("Cannot Issue Certificate!");
                Response.Redirect("Instructorprofile.aspx");
            }
            if(!err)
            {
               MessageBox.Show("YOU HAVE ISSUED A CERTIFICATE SUCCESSFULLY!");
               Response.Redirect("Instructorprofile.aspx");
               
            }
            conn.Close();

        }
    }
}