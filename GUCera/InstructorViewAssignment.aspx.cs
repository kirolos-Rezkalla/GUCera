using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace GUCera
{
    public partial class ViewAssignment : System.Web.UI.Page
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

            
            int course_id = Int32.Parse(CourseId.Text);
            int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));
            String session_id_string = session_id.ToString();

            Boolean found = false;
            SqlCommand feedbacks = new SqlCommand("InstructorTeachThisCourse", conn);
            feedbacks.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader rdr1 = feedbacks.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr1.Read())
            {
                int stored_course = rdr1.GetInt32(rdr1.GetOrdinal("cid"));
                int stored_instructor_id = rdr1.GetInt32(rdr1.GetOrdinal("insid"));



                if (course_id == stored_course && session_id == stored_instructor_id)
                {
                    found = true;
                    break;
                }
            }
            conn.Close();

            if (found == true)
            {
                SqlCommand view_assignment = new SqlCommand("InstructorViewAssignmentsStudents", conn);
                view_assignment.CommandType = CommandType.StoredProcedure;
                view_assignment.Parameters.Add(new SqlParameter("@instrId", session_id));
                view_assignment.Parameters.Add(new SqlParameter("@cid", course_id));
                conn.Open();

                SqlDataReader rdr = view_assignment.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read())
                {
                    int student_id = rdr.GetInt32(rdr.GetOrdinal("sid"));
                    int cid = rdr.GetInt32(rdr.GetOrdinal("cid"));
                    int assignment_number = rdr.GetInt32(rdr.GetOrdinal("assignmentNumber"));
                    String type = rdr.GetString(rdr.GetOrdinal("assignmenttype"));
                    decimal grade = rdr.GetDecimal(rdr.GetOrdinal("grade"));

                    HtmlGenericControl tr = new HtmlGenericControl("tr");
                    HtmlGenericControl td1 = new HtmlGenericControl("td");
                    HtmlGenericControl td2 = new HtmlGenericControl("td");
                    HtmlGenericControl td3 = new HtmlGenericControl("td");
                    HtmlGenericControl td4 = new HtmlGenericControl("td");
                    HtmlGenericControl td5 = new HtmlGenericControl("td");

                    td1.InnerText = student_id + "";
                    td2.InnerText = cid + "";
                    td3.InnerText = assignment_number + "";
                    td4.InnerText = type;
                    td5.InnerText = grade + "";

                    tr.Controls.Add(td1);
                    tr.Controls.Add(td2);
                    tr.Controls.Add(td3);
                    tr.Controls.Add(td4);
                    tr.Controls.Add(td5);



                    tabs.Controls.Add(tr);
                }
            }
            else
            {
                MessageBox.Show("this instructor does not teach this course ");
                return;
            }
        }
    }
}