using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GUCera
{
    public partial class ViewMyCertificates : System.Web.UI.Page
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

                SqlCommand cmd = new SqlCommand("viewCertificate", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlCommand cmd1 = new SqlCommand("coursesEnrolledIn", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@sid", session_id));

                conn.Open();
                SqlDataReader rdr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                //pass parameters to the stored procedure

                while (rdr.Read())
                {

                    string course_name = rdr.GetString(rdr.GetOrdinal("name"));

                    select_course.Items.Add(new ListItem(course_name, course_name));

                }

            }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void B1_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["GUCera"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));
            String session_id_string = session_id.ToString();

            String course_name = select_course.Value;


            SqlCommand cmd = new SqlCommand("viewCertificate", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd1 = new SqlCommand("courseIdUsingName", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@name", course_name));
            SqlParameter cid = cmd1.Parameters.Add("@id", SqlDbType.Int);
            cid.Direction = ParameterDirection.Output;

            conn.Open();
            cmd1.ExecuteNonQuery();

            cmd.Parameters.Add(new SqlParameter("@sid", session_id));
            cmd.Parameters.Add(new SqlParameter("@cid", cid.Value));


            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            //pass parameters to the stored procedure

            while (rdr.Read())
            {

                int courseID = rdr.GetInt32(rdr.GetOrdinal("cid"));
                DateTime issue_date = rdr.GetDateTime(rdr.GetOrdinal("issueDate"));

                //Create a new tr for each row and add it to the HTML form
                HtmlGenericControl tr = new HtmlGenericControl("tr");
                //Create 4 new  td for each string and add it to the HTML form
                HtmlGenericControl td1 = new HtmlGenericControl("td");
                //HtmlGenericControl td2 = new HtmlGenericControl("td");
                HtmlGenericControl td2 = new HtmlGenericControl("td");

                td1.InnerText = courseID + "";
                td2.InnerText = issue_date + "";

                tr.Controls.Add(td1);
                tr.Controls.Add(td2);


                tabs.Controls.Add(tr);

            }
        }
    }
}