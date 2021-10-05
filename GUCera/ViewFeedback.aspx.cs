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
    public partial class ViewFeedback : System.Web.UI.Page
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

            Boolean found = false;
            SqlCommand feedbacks = new SqlCommand("InstructorTeachThisCourse", conn);
            feedbacks.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader rdr1 = feedbacks.ExecuteReader(CommandBehavior.CloseConnection);
            int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));
            String session_id_string = session_id.ToString();
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
                SqlCommand view_feedback = new SqlCommand("ViewFeedbacksAddedByStudentsOnMyCourse ", conn);
                view_feedback.CommandType = CommandType.StoredProcedure;
                view_feedback.Parameters.Add(new SqlParameter("@instrId", session_id));
                view_feedback.Parameters.Add(new SqlParameter("@cid", course_id));
                conn.Open();

                SqlDataReader rdr = view_feedback.ExecuteReader(CommandBehavior.CloseConnection);

                while (rdr.Read())
                {
                    int number = rdr.GetInt32(rdr.GetOrdinal("number"));
                    String comment = rdr.GetString(rdr.GetOrdinal("comment"));
                    int number_of_likes = rdr.GetInt32(rdr.GetOrdinal("numberOfLikes"));




                    HtmlGenericControl tr = new HtmlGenericControl("tr");
                    HtmlGenericControl td1 = new HtmlGenericControl("td");
                    HtmlGenericControl td2 = new HtmlGenericControl("td");
                    HtmlGenericControl td3 = new HtmlGenericControl("td");

                    td1.InnerText = number + "";
                    td2.InnerText = comment;
                    td3.InnerText = number_of_likes + "";

                    tr.Controls.Add(td1);
                    tr.Controls.Add(td2);
                    tr.Controls.Add(td3);



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