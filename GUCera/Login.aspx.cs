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
    public partial class Login : System.Web.UI.Page
    {
        internal static string aspx;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void login(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            if (username.Text == "")
            {
                MessageBox.Show("You have to enter your ID");
                return;
            }

            int id = Int16.Parse(username.Text);
            String pass = password.Text;

            SqlCommand loginproc = new SqlCommand("userLogin", conn);
            loginproc.CommandType = CommandType.StoredProcedure;
            loginproc.Parameters.Add(new SqlParameter("@id", id));
            loginproc.Parameters.Add(new SqlParameter("@password", pass));

            SqlParameter success = loginproc.Parameters.Add("@success", System.Data.SqlDbType.Int);
            SqlParameter type = loginproc.Parameters.Add("@type", System.Data.SqlDbType.Int);

            success.Direction = ParameterDirection.Output;
            type.Direction = ParameterDirection.Output;

            conn.Open();
            loginproc.ExecuteNonQuery();
            conn.Close();


            if (pass.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter your Password");
                return;
            }

            if (success.Value.ToString() == "1")
            {
                Session["user_login"] = id;
                if (type.Value.ToString() == "2")
                {
                    Response.Redirect("StudentProfile.aspx");
                }
                else if (type.Value.ToString() == "0")
                {
                    Response.Redirect("InstructorProfile.aspx");
                }
                else if (type.Value.ToString() == "1")
                {
                    Response.Redirect("admin.aspx");
                }

            }
            else
            {
                MessageBox.Show("Incorrect ID or Password" + "\n" + "\n" + "Please try again");
                return;
            }
        }
    }
}