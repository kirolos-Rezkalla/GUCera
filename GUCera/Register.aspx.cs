using System;
using System.Text;
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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void instructor_register(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            // declaring parameters 
            String first_name = firstName.Text;
            String last_name = lastName.Text;
            String pass = password.Text;
            String user_email = email.Text;
            int user_gender = Int16.Parse(gender.SelectedItem.Value);
            String user_address = address.Text;

            // initiating sql command
            SqlCommand instructor_register = new SqlCommand("InstructorRegister", conn);
            instructor_register.CommandType = CommandType.StoredProcedure;
            // input
            instructor_register.Parameters.Add(new SqlParameter("@first_name", first_name));
            instructor_register.Parameters.Add(new SqlParameter("@last_name", last_name));
            instructor_register.Parameters.Add(new SqlParameter("@password", pass));
            instructor_register.Parameters.Add(new SqlParameter("@email", user_email));
            instructor_register.Parameters.Add(new SqlParameter("@gender", user_gender));
            instructor_register.Parameters.Add(new SqlParameter("@address", user_address));

            // proc to get the id
            SqlCommand instructor_id = new SqlCommand("userID", conn);
            instructor_id.CommandType = CommandType.StoredProcedure;
            // output
            SqlParameter user_id = instructor_id.Parameters.Add("@user_id", System.Data.SqlDbType.Int);
            // declares an output
            user_id.Direction = ParameterDirection.Output;

            SqlCommand instructor_emails = new SqlCommand("userEmail", conn);
            instructor_emails.CommandType = CommandType.StoredProcedure;

            if (first_name.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter your First Name");
                return;
            }

            else if (first_name.Trim().Length > 20)
            {
                MessageBox.Show("Your First Name has to be less than 21 characters");
                return;
            }

            if (last_name.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter your Last Name");
                return;
            }

            else if (last_name.Trim().Length > 20)
            {
                MessageBox.Show("Your Last Name has to be less than 21 characters");
                return;
            }

            if (pass.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter your Password");
                return;
            }

            else if (pass.Trim().Length > 20)
            {
                MessageBox.Show("Your Password has to be less than 21 characters");
                return;
            }

            conn.Open();
            // to read the table one by one
            SqlDataReader rdr = instructor_emails.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String email = rdr.GetString(rdr.GetOrdinal("email"));
                if (user_email.Trim().Length > 10)
                {
                    MessageBox.Show("Your email has to be less than 11 characters");
                    return;
                }
                else if (user_email.Trim() == string.Empty)
                {
                    MessageBox.Show("You have to enter your Email");
                    return;
                }
                else if (user_email.Trim() == email.Trim())
                {
                    MessageBox.Show("This email already exists!" + "\n" + "\n" + "Please enter another one");
                    return;
                }
            }
            conn.Close();

            if (user_gender == -1)
            {
                MessageBox.Show("You have to choose Male or Female");
                return;
            }

            if (user_address.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter your Address");
                return;
            }

            else if (user_address.Trim().Length > 10)
            {
                MessageBox.Show("Your Address has to be less than 11 characters");
                return;
            }

            // excecute what you have done
            conn.Open();
            instructor_register.ExecuteNonQuery();
            instructor_id.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("YOU HAVE REGISTERED SUCCESSFULLY!" + "\n" + "\n" + "Your ID is " + user_id.Value);
            Response.Redirect("Login.aspx");
        }

        protected void student_register(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["GUCera"].ToString();
            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            String first_name = firstName.Text;
            String last_name = lastName.Text;
            String pass = password.Text;
            String user_email = email.Text;
            int user_gender = Int16.Parse(gender.SelectedItem.Value);
            String user_address = address.Text;


            SqlCommand student_register = new SqlCommand("studentRegister", conn);
            student_register.CommandType = CommandType.StoredProcedure;
            student_register.Parameters.Add(new SqlParameter("@first_name", first_name));
            student_register.Parameters.Add(new SqlParameter("@last_name", last_name));
            student_register.Parameters.Add(new SqlParameter("@password", pass));
            student_register.Parameters.Add(new SqlParameter("@email", user_email));
            student_register.Parameters.Add(new SqlParameter("@gender", user_gender));
            student_register.Parameters.Add(new SqlParameter("@address", user_address));

            SqlCommand student_id = new SqlCommand("userID", conn);
            student_id.CommandType = CommandType.StoredProcedure;
            SqlParameter user_id = student_id.Parameters.Add("@user_id", System.Data.SqlDbType.Int);
            user_id.Direction = ParameterDirection.Output;

            SqlCommand student_emails = new SqlCommand("userEmail", conn);
            student_emails.CommandType = CommandType.StoredProcedure;

            if (first_name.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter your First Name");
                return;
            }

            else if (first_name.Trim().Length > 20)
            {
                MessageBox.Show("Your First Name has to be less than 21 characters");
                return;
            }

            if (last_name.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter your Last Name");
                return;
            }

            else if (last_name.Trim().Length > 20)
            {
                MessageBox.Show("Your Last Name has to be less than 21 characters");
                return;
            }

            if (pass.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter your Password");
                return;
            }

            else if (pass.Trim().Length > 20)
            {
                MessageBox.Show("Your Password has to be less than 21 characters");
                return;
            }

            conn.Open();
            SqlDataReader rdr = student_emails.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                String email = rdr.GetString(rdr.GetOrdinal("email"));
                if (user_email.Trim().Length > 10)
                {
                    MessageBox.Show("Your email has to be less than 11 characters");
                    return;
                }
                else if (user_email.Trim() == string.Empty)
                {
                    MessageBox.Show("You have to enter your Email");
                    return;
                }
                else if (user_email.Trim() == email.Trim())
                {
                    MessageBox.Show("This email already exists!" + "\n" + "\n" + "Please enter another one");
                    return;
                }
            }
            conn.Close();
            
            if (user_gender == -1)
            {
                MessageBox.Show("You have to choose Male or Female");
                return;
            }

            if (user_address.Trim() == string.Empty)
            {
                MessageBox.Show("You have to enter your Address");
                return;
            }

            else if (user_address.Trim().Length > 10)
            {
                MessageBox.Show("Your Address has to be less than 11 characters");
                return;
            }

            conn.Open();
            student_register.ExecuteNonQuery();
            student_id.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("YOU HAVE REGISTERED SUCCESSFULLY!" + "\n" + "\n" + "Your ID is " + user_id.Value);
            Response.Redirect("Login.aspx");
        }

        protected void login_directly(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}