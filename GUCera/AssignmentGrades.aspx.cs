using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GUCera
{
    public partial class AssignmentGrades : System.Web.UI.Page
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

                SqlCommand cmd = new SqlCommand("ViewAssign", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlCommand cmd1 = new SqlCommand("coursesEnrolledIn", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@sid", session_id));

                SqlCommand cmd2 = new SqlCommand("viewAssignGrades", conn);
                cmd2.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader rdr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);


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

        protected void Btn1_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));
            String session_id_string = session_id.ToString();

            String course_name = select_course.Value;


            SqlCommand cmd = new SqlCommand("viewAssign", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd1 = new SqlCommand("courseIdUsingName", conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@name", course_name));

            SqlParameter cid = cmd1.Parameters.Add("@id", SqlDbType.Int);
            cid.Direction = ParameterDirection.Output;


            conn.Open();
            cmd1.ExecuteNonQuery();

            cmd.Parameters.Add(new SqlParameter("@Sid", session_id_string));
            cmd.Parameters.Add(new SqlParameter("@courseId", cid.Value));


            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (rdr.Read())
            {

                int number = rdr.GetInt32(rdr.GetOrdinal("number"));
                String type = rdr.GetString(rdr.GetOrdinal("type"));

                SqlConnection con = new SqlConnection(connStr);
                using (SqlCommand cmd2 = new SqlCommand("viewAssignGrades", con))
                {
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add(new SqlParameter("@assignnumber", number));
                    cmd2.Parameters.Add(new SqlParameter("@assignType", type));
                    cmd2.Parameters.Add(new SqlParameter("@cid", cid.Value));
                    cmd2.Parameters.Add(new SqlParameter("@sid", session_id));

                    SqlParameter grade = cmd2.Parameters.Add("@assignGrade", SqlDbType.Int);
                    grade.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd2.ExecuteNonQuery();

                    HtmlGenericControl tr = new HtmlGenericControl("tr");
                    HtmlGenericControl td1 = new HtmlGenericControl("td");
                    HtmlGenericControl td2 = new HtmlGenericControl("td");
                    HtmlGenericControl td3 = new HtmlGenericControl("td");


                    td1.InnerText = number + "";
                    td2.InnerText = type;
                    td3.InnerText = grade.Value + "";


                    tr.Controls.Add(td1);
                    tr.Controls.Add(td2);
                    tr.Controls.Add(td3);


                    tabs.Controls.Add(tr);
                    con.Close();
                }
            }
        }
    }
}