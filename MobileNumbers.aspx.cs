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
    public partial class MobileNumbers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void done_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            int id = Int16.Parse(mobileNumberID.Text);
            String mobile_number = mobileNumber.Text;

            SqlCommand addMobileNumbers = new SqlCommand("addMobile", conn);
            addMobileNumbers.CommandType = CommandType.StoredProcedure;
            addMobileNumbers.Parameters.Add(new SqlParameter("@ID", id));
            addMobileNumbers.Parameters.Add(new SqlParameter("@mobile_number", mobile_number));

            int session_id = Int16.Parse(Convert.ToString(Session["User_register"]));
            String id_string = session_id.ToString();

            if (id.ToString() != id_string)
            {
                MessageBox.Show("This ID is incorrect");
                return;
            }

            if (mobile_number.Trim().Length != 11)
            {
                MessageBox.Show("Your mobile number has to be exactly 11 characters");
                return;
            }

            else if (mobile_number.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter your mobile number");
                return;
            }


            conn.Open();
            addMobileNumbers.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("Login.aspx");
        }

        protected void insert_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            int id = Int16.Parse(mobileNumberID.Text);
            String mobile_number = mobileNumber.Text;

            SqlCommand addMobileNumbers = new SqlCommand("addMobile", conn);
            addMobileNumbers.CommandType = CommandType.StoredProcedure;
            addMobileNumbers.Parameters.Add(new SqlParameter("@ID", id));
            addMobileNumbers.Parameters.Add(new SqlParameter("@mobile_number", mobile_number));

            int session_id = Int16.Parse(Convert.ToString(Session["User_register"]));
            String id_string = session_id.ToString();

            if (id.ToString() != id_string)
            {
                MessageBox.Show("This ID is incorrect");
                return;
            }

            if (mobile_number.Trim().Length != 11)
            {
                MessageBox.Show("Your mobile number has to be exactly 11 characters");
                return;
            }

            else if (mobile_number.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter your mobile number");
                return;
            }

            conn.Open();
            addMobileNumbers.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("MobileNumbers.aspx");
        }
    }
}