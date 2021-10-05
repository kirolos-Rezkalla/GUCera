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

namespace GUCera
{
    public partial class StudentProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["user_login"]) != "")
            {
                string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));

            SqlCommand student_profile = new SqlCommand("viewMyProfile", conn);
            student_profile.CommandType = CommandType.StoredProcedure;
            student_profile.Parameters.Add(new SqlParameter("@id", session_id));

            conn.Open();
            SqlDataReader rdr = student_profile.ExecuteReader(CommandBehavior.CloseConnection);

            int student_id = Int16.Parse(Convert.ToString(Session["user_login"]));


            while (rdr.Read())
            {
                
                string first_name = rdr.GetString(rdr.GetOrdinal("firstName"));
                string last_name = rdr.GetString(rdr.GetOrdinal("lastName"));
                byte[] gender = (byte[])rdr.GetSqlBinary(rdr.GetOrdinal("gender"));
                string email = rdr.GetString(rdr.GetOrdinal("email"));
                string address = rdr.GetString(rdr.GetOrdinal("address"));

                string gender2 = "";
                if (gender[0] == 1)
                {
                    gender2 = "Female";
                }
                else
                {
                    gender2 = "Male";
                }


                var tr = new HtmlGenericControl("tr");
                var td1 = new HtmlGenericControl("td");
                var td2 = new HtmlGenericControl("td");
                var td3 = new HtmlGenericControl("td");
                var td4 = new HtmlGenericControl("td");
                var td5 = new HtmlGenericControl("td");
                var td6 = new HtmlGenericControl("td");

                td1.InnerText = student_id.ToString();
                td2.InnerText = first_name.ToString();
                td3.InnerText = last_name.ToString();
                td4.InnerText = gender2.ToString();
                td5.InnerText = email.ToString();
                td6.InnerText = address.ToString();

                tr.Controls.Add(td1);
                tr.Controls.Add(td2);
                tr.Controls.Add(td3);
                tr.Controls.Add(td4);
                tr.Controls.Add(td5);
                tr.Controls.Add(td6);

                tabs.Controls.Add(tr);
            }
            conn.Close();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            Response.Redirect("CoursesAcceptedByAdmin.aspx");
        }
    }
}