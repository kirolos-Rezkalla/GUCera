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
    public partial class acceptcourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["user_login"]) != "")
            {

                string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
                //create a new connection
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand idcourse = new SqlCommand("courseid", conn);
                idcourse.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader rdr = idcourse.ExecuteReader(CommandBehavior.CloseConnection);
                DropDownList1.Items.Add("Select Course");


                while (rdr.Read())
                {
                    int id = rdr.GetInt32(rdr.GetOrdinal("id"));
                    String name = rdr.GetString(rdr.GetOrdinal("name"));
                    DropDownList1.Items.Add(new ListItem("courseid: " + id.ToString() + "-" + " coursename: " + name));


                }
                conn.Close();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }

        protected void AcceptCourse(object sender, EventArgs e)
        {

            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);
            if (DropDownList1.SelectedValue == "Select Course")
            {
                MessageBox.Show("You have to choose course");
                Response.Redirect(Request.RawUrl);
            }
            String[] l = DropDownList1.SelectedValue.Split('-');
            String[] l1 = l[0].Split(':');
            int id = Int16.Parse(l1[1]);
           
            int adminid = Int16.Parse(Convert.ToString(Session["user_login"]));
            SqlCommand AdminAcceptRejectCourse = new SqlCommand("AdminAcceptRejectCourse", conn);
            AdminAcceptRejectCourse.CommandType = CommandType.StoredProcedure;
            AdminAcceptRejectCourse.Parameters.Add(new SqlParameter("@courseId", id));
            AdminAcceptRejectCourse.Parameters.Add(new SqlParameter("@adminid", adminid));
            conn.Open();
            AdminAcceptRejectCourse.ExecuteNonQuery();
            conn.Close();
            System.Windows.Forms.MessageBox.Show("course accepted");
            Response.Redirect(Request.RawUrl);

        }

       
    }
}