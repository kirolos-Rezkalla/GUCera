using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace GUCera
{
    public partial class AddFeedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["user_login"]) != "")
            {
                if (!Page.IsPostBack)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["GUCera"].ToString();
                    SqlConnection conn = new SqlConnection(connStr);

                    int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));
                    String session_id_string = session_id.ToString();

                    SqlCommand cmd = new SqlCommand("addFeedback", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlCommand cmd1 = new SqlCommand("coursesEnrolledIn", conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@sid", session_id));

                    conn.Open();
                    SqlDataReader rdr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                    //pass parameters to the stored procedure

                    while (rdr.Read())
                    {
                        string courseName = rdr.GetString(rdr.GetOrdinal("name"));

                        select_course.Items.Add(new ListItem(courseName, courseName));

                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Add(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));
            String session_id_string = session_id.ToString();

            String course_name = select_course.Value;

            SqlCommand cmd1 = new SqlCommand("courseIdUsingName", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@name", course_name));
            SqlParameter cid = cmd1.Parameters.Add("@id", SqlDbType.Int);
            cid.Direction = ParameterDirection.Output;

            conn.Open();

            cmd1.ExecuteNonQuery();

            SqlCommand cmd = new SqlCommand("addFeedback", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            string yourFeedback = your_feedback.Text;

            if (yourFeedback.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter your Feedback");
                return;
            }

            cmd.Parameters.Add(new SqlParameter("@sid", session_id));
            cmd.Parameters.Add(new SqlParameter("@comment", yourFeedback));
            cmd.Parameters.Add(new SqlParameter("@cid", cid.Value));
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("You have added your Feedback Successfully!");
        }
    }
}