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
    public partial class InstructorMobileNumbers : System.Web.UI.Page
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

        protected void add_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            String mobile_number = mobileNumber.Text;

            int session_id = Int16.Parse(Convert.ToString(Session["user_login"]));
            String id_string = session_id.ToString();

            SqlCommand userMobileNumber = new SqlCommand("mobileNumbers", conn);
            userMobileNumber.CommandType = CommandType.StoredProcedure;

            SqlCommand addMobileNumbers = new SqlCommand("addMobile", conn);
            addMobileNumbers.CommandType = CommandType.StoredProcedure;
            addMobileNumbers.Parameters.Add(new SqlParameter("@ID", id_string));
            addMobileNumbers.Parameters.Add(new SqlParameter("@mobile_number", mobile_number));


            conn.Open();
            // to read the table one by one
            SqlDataReader rdr = userMobileNumber.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String mobileNumber = rdr.GetString(rdr.GetOrdinal("mobileNumber"));
                if (mobile_number.Trim().Length != 11)
                {
                    MessageBox.Show("Your Mobile Number has to be exactly 11 characters");
                    return;
                }
                else if (mobile_number.Trim() == string.Empty)
                {
                    MessageBox.Show("You have to add your Mobile Number");
                    return;
                }
                else if (mobile_number.Trim() == mobileNumber.Trim())
                {
                    MessageBox.Show("This Mobile Number already exists!" + "\n" + "\n" + "Please add another one");
                    return;
                }
            }
            conn.Close();

            conn.Open();
            addMobileNumbers.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("You have added your Mobile Number Successfully!");
            return;

        }

    }
}