using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GUCera
{
    public partial class Student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

            while (rdr.Read())
            {
                Label studentID = new Label();
                studentID.Text = session_id.ToString();
                p8.Controls.Add(studentID); 

                Decimal your_GPA = rdr.GetDecimal(rdr.GetOrdinal("gpa"));
                Label studentGPA = new Label();
                studentGPA.Text = your_GPA + " ";
                p7.Controls.Add(studentGPA);

                String your_firstName = rdr.GetString(rdr.GetOrdinal("firstName"));
                Label studentFirstName = new Label();
                studentFirstName.Text = your_firstName;
                p1.Controls.Add(studentFirstName);

                String your_lastName = rdr.GetString(rdr.GetOrdinal("lastName"));
                Label studentLastName = new Label();
                studentLastName.Text = your_lastName;
                p2.Controls.Add(studentLastName);

                String your_password = rdr.GetString(rdr.GetOrdinal("password"));
                Label studentPassword = new Label();
                studentPassword.Text = your_password;
                p3.Controls.Add(studentPassword);

                Byte[] your_gender = (byte[])rdr.GetSqlBinary(rdr.GetOrdinal("gender"));
                Label studentGender = new Label();
                if (your_gender[0] == 1)
                {
                    studentGender.Text = "Female" + "</br>";
                }
                else
                {
                    studentGender.Text = "Male" + "</br>";
                }
                p5.Controls.Add(studentGender);

                String your_email = rdr.GetString(rdr.GetOrdinal("email"));
                Label studentEmail = new Label();
                studentEmail.Text = your_email;
                p6.Controls.Add(studentEmail);

                String your_address = rdr.GetString(rdr.GetOrdinal("address"));
                Label studentAddress = new Label();
                studentAddress.Text = your_address;
                p4.Controls.Add(studentAddress);


            }
            conn.Close();
        }

    }
}